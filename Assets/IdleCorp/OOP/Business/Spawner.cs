using System.Collections.Generic;
using System.Linq;
using IdleCorp.ECS.Components;
using IdleCorp.OOP.Business.Hangars;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Factory;
using IdleCorp.OOP.Services.Events.Hangars;
using IdleCorp.OOP.Services.Factory;
using IdleCorp.OOP.Services.Hangars;
using Unity.Entities;
using UnityEngine;

namespace IdleCorp.OOP.Business
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> hangarSpawnPoints;

        private EntityManager _entityManager;
        private Entity _robotSpawnEntity;

        private FactoryService _factoryService;
        private HangarsService _hangarsService;

        private Dictionary<int, HangarWorldObject> _spawnedHangarsByPositionId;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var robotSpawnQuery = _entityManager
                .CreateEntityQuery(typeof(RobotSpawnComponent));
            _robotSpawnEntity = robotSpawnQuery.GetSingletonEntity();

            _factoryService = ServiceLocator.GetService<FactoryService>();
            _hangarsService = ServiceLocator.GetService<HangarsService>();

            SetupSpawnDictionaries();

            AddListeners();

            SpawnWorld();
        }

        private void SetupSpawnDictionaries()
        {
            _spawnedHangarsByPositionId = new Dictionary<int, HangarWorldObject>
            {
                { 0, null },
                { 1, null },
                { 2, null },
                { 3, null }
            };
        }

        private void AddListeners()
        {
            var eventsService = ServiceLocator.GetService<EventsService>();

            //TODO: listen to events to spawn elements:
            eventsService.GetEvent<RobotProducedEvent>().AddListener(SpawnRobots);
            eventsService.GetEvent<HangarBuiltEvent>().AddListener(SpawnHangar);
            // eventsService.GetEvent<VehicleDeployedEvent>().AddListener(SpawnVehicle);
            // eventsService.GetEvent<ShootingStarGeneratedEvent>().AddListener(SpawnShootingStar);
        }

        private void SpawnWorld()
        {
            var builtHangarsInfo = _hangarsService.GetBuiltHangarsInfo();
            foreach (var builtHangarKvp in builtHangarsInfo)
            {
                SpawnHangar(builtHangarKvp.Key, builtHangarKvp.Value);
            }

            //TODO: spawn Mine
        }

        private void SpawnRobots(int amount)
        {
            var defaultSpawnComponent = _entityManager.GetComponentData<RobotSpawnComponent>(_robotSpawnEntity);
            var newSpawnComponent = new RobotSpawnComponent
            {
                RobotPrefab = defaultSpawnComponent.RobotPrefab,
                AmountToSpawn = amount,
                SpawnPosition = _factoryService.GetSpawnPoint().position,
                TargetPosition = GetRandomHangarPosition(),
                TargetReachedThreshold = _factoryService.GetTargetReachedThresholdForRobots()
            };
            _entityManager.SetComponentData(_robotSpawnEntity, newSpawnComponent);
        }

        private void SpawnHangar(int positionId, int hangarId)
        {
            var currentlySpawnedHangar = _spawnedHangarsByPositionId[positionId];
            if (currentlySpawnedHangar != null)
                Destroy(currentlySpawnedHangar);

            var hangarToSpawn = _hangarsService.GetHangarPrefab(hangarId);
            var spawnPosition = hangarSpawnPoints[positionId];
            _spawnedHangarsByPositionId[positionId] = Instantiate(hangarToSpawn, spawnPosition);
        }

        private void SpawnVehicle()
        {
            //TODO
        }

        private void SpawnShootingStar()
        {
            //TODO
        }

        private Vector3 GetRandomHangarPosition()
        {
            var builtHangarsInfo = _hangarsService.GetBuiltHangarsInfo();
            var randomValue = Random.Range(0, builtHangarsInfo.Count);
            var randomHangarPositionId = builtHangarsInfo.ElementAt(randomValue).Key;
            return _spawnedHangarsByPositionId[randomHangarPositionId].RobotTargetPosition.position;
        }
    }
}