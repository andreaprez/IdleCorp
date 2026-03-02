using System.Collections.Generic;
using IdleCorp.OOP.Business.Hangars;
using IdleCorp.OOP.Persistence.Hangars;
using IdleCorp.OOP.Services.Currencies;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Factory;
using IdleCorp.OOP.Services.Events.Hangars;
using IdleCorp.OOP.Services.UserData;
using UnityEngine;

namespace IdleCorp.OOP.Services.Hangars
{
    public class HangarsService : IService
    {
        private readonly HangarsConfig _hangarsConfig;
        private HangarsData _data;

        private HangarsPopupPresenter _mainPopupPresenter;
        private HangarStorePopupPresenter _storePopupPresenter;

        private EventsService _eventsService;
        private CurrenciesService _currenciesService;

        public HangarsService(HangarsConfig hangarsConfig)
        {
            _hangarsConfig = hangarsConfig;
        }

        public void Init()
        {
            _data = ServiceLocator.GetService<UserDataService>().GetData<HangarsData>();

            _mainPopupPresenter = new HangarsPopupPresenter();
            _mainPopupPresenter.Initialize();
            _storePopupPresenter = new HangarStorePopupPresenter();
            _storePopupPresenter.Initialize();

            _eventsService = ServiceLocator.GetService<EventsService>();
            _currenciesService = ServiceLocator.GetService<CurrenciesService>();
            
            _eventsService.GetEvent<RobotProducedEvent>().AddListener(AddRobots);
            _eventsService.GetEvent<HangarBuiltEvent>().AddListener(HandleHangarBuilt);
        }

        public void Dispose()
        {
            _data = null;
        }

        public int GetMaxCapacity()
        {
            var maxCapacity = 0;
            foreach (var hangar in _data.Hangars)
            {
                var hangarConfig = _hangarsConfig.Hangars.Find(h => h.Id == hangar.Id);
                maxCapacity += hangarConfig.Capacity;
            }
            return maxCapacity;
        }

        public int GetTotalRobotCount()
        {
            return _data.TotalRobotCount;
        }

        public int GetFreeSpace()
        {
            return GetMaxCapacity() - GetTotalRobotCount();
        }

        public bool IsHangarBuilt(int positionId)
        {
            return GetHangarByPositionId(positionId) != null;
        }

        public int GetHangarId(int positionId)
        {
            if (!IsHangarBuilt(positionId))
                return -1;

            return GetHangarByPositionId(positionId).Id;
        }

        public Sprite GetHangarImage(int positionId)
        {
            if (!IsHangarBuilt(positionId))
                return null;

            var hangar = GetHangarByPositionId(positionId);
            var hangarConfig = _hangarsConfig.Hangars.Find(h => h.Id == hangar.Id);
            return hangarConfig.Image;
        }

        public int GetHangarMaxCapacity(int positionId)
        {
            if (!IsHangarBuilt(positionId))
                return 0;

            var hangar = GetHangarByPositionId(positionId);
            var hangarConfig = _hangarsConfig.Hangars.Find(h => h.Id == hangar.Id);
            return hangarConfig.Capacity;
        }

        public int GetHangarUsedCapacity(int positionId)
        {
            if (!IsHangarBuilt(positionId))
                return 0;

            var hangar = GetHangarByPositionId(positionId);
            return hangar.CurrentRobotCount;
        }

        public HangarWorldObject GetHangarPrefab(int hangarId)
        {
            return _hangarsConfig.Hangars.Find(h => h.Id == hangarId).Prefab;
        }

        public Dictionary<int, int> GetBuiltHangarsInfo()
        {
            var builtHangarsByPositionId = new Dictionary<int, int>();
            foreach (var hangarData in _data.Hangars)
                builtHangarsByPositionId.Add(hangarData.PositionId, hangarData.Id);
            return builtHangarsByPositionId;
        }

        public void ResetHangars()
        {
            _data.SetTotalRobotCount(0);
            _eventsService.GetEvent<RobotCountChangedEvent>().Trigger(0);

            _data.Hangars.Clear();
        }

        public List<HangarConfig> GetHangarConfigList()
        {
            return _hangarsConfig.Hangars;
        }

        private void AddRobots(int amount)
        {
            var totalRobotCount = _data.TotalRobotCount;
            totalRobotCount += amount;
            _data.SetTotalRobotCount(totalRobotCount);

            while (amount > 0)
            {
                var freeSpaceInHangar = 0;
                var hangarToAddRobots = _data.Hangars[0];
                foreach (var hangar in _data.Hangars)
                {
                    var hangarMaxCapacity = _hangarsConfig.Hangars.Find(h => h.Id == hangar.Id).Capacity;
                    var hangarFreeSpace = hangarMaxCapacity - hangar.CurrentRobotCount;
                    if (hangarFreeSpace > freeSpaceInHangar)
                    {
                        freeSpaceInHangar = hangarFreeSpace;
                        hangarToAddRobots = hangar;
                    }
                }

                var hangarRobotCount = hangarToAddRobots.CurrentRobotCount;
                var amountToAddToThisHangar = Mathf.Min(amount, freeSpaceInHangar);
                hangarRobotCount += amountToAddToThisHangar;
                amount -= amountToAddToThisHangar;
                ModifyHangarRobotCount(hangarToAddRobots.PositionId, hangarRobotCount);
            }
            
            _eventsService.GetEvent<RobotCountChangedEvent>().Trigger(totalRobotCount);
        }

        private void HandleHangarBuilt(int positionId, int hangarId)
        {
            var hangarConfig = _hangarsConfig.Hangars.Find(h => h.Id == hangarId);

            _currenciesService.SubtractFunds(hangarConfig.Cost);

            if (IsHangarBuilt(positionId))
                ModifyHangarId(positionId, hangarId);
            else
                BuildHangar(positionId, hangarId);
        }

        private void BuildHangar(int positionId, int hangarId)
        {
            if (IsHangarBuilt(positionId))
                return;

            var hangar = new HangarData();
            hangar.SetPositionId(positionId);
            hangar.SetId(hangarId);
            _data.AddHangar(hangar);
        }

        private void ModifyHangarId(int positionId, int hangarId)
        {
            _data.ModifyHangarId(positionId, hangarId);
        }

        private void ModifyHangarRobotCount(int positionId, int count)
        {
            _data.ModifyHangarRobotCount(positionId, count);
        }

        private HangarData GetHangarByPositionId(int positionId)
        {
            return _data.Hangars.Find(h => h.PositionId == positionId);
        }
    }
}