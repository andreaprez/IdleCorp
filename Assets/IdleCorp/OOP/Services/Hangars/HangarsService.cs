using IdleCorp.OOP.Persistence.Hangars;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Factory;
using IdleCorp.OOP.Services.UserData;

namespace IdleCorp.OOP.Services.Hangars
{
    public class HangarsService : IService
    {
        private readonly HangarsConfig _hangarsConfig;
        private HangarsData _data;

        public HangarsService(HangarsConfig hangarsConfig)
        {
            _hangarsConfig = hangarsConfig;
        }

        public void Init()
        {
            _data = ServiceLocator.GetService<UserDataService>().GetData<HangarsData>();
            
            var eventsService = ServiceLocator.GetService<EventsService>();
            eventsService.GetEvent<RobotProducedEvent>().AddListener(AddRobots);
        }

        public void Dispose()
        {
            _data = null;
        }

        public int GetTotalRobotCount()
        {
            return _data.TotalRobotCount;
        }

        public void AddRobots(int amount)
        {
            var totalRobotCount = _data.TotalRobotCount;
            totalRobotCount += amount;
            _data.SetTotalRobotCount(totalRobotCount);

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
            hangarRobotCount += amount;
            ModifyHangarRobotCount(hangarToAddRobots.PositionId, hangarRobotCount);
        }

        public void BuildHangar(int positionId, int hangarId)
        {
            var hangar = new HangarData();
            hangar.SetPositionId(positionId);
            hangar.SetId(hangarId);
            _data.AddHangar(hangar);
        }

        public void ModifyHangarId(int positionId, int hangarId)
        {
            _data.ModifyHangarId(positionId, hangarId);
        }

        public void ModifyHangarRobotCount(int positionId, int hangarId)
        {
            _data.ModifyHangarRobotCount(positionId, hangarId);
        }

        public void ResetHangars()
        {
            _data.SetTotalRobotCount(0);
            _data.Hangars.Clear();
            BuildHangar(0, 0);
        }
    }
}