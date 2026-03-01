using System.Collections.Generic;
using IdleCorp.ECS.Components;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Factory;
using IdleCorp.OOP.Services.Events.Hangars;
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

        private HangarsService _hangarsService;

        private Dictionary<int, GameObject> _spawnedHangarsByPositionId;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var robotSpawnQuery = _entityManager
                .CreateEntityQuery(typeof(RobotSpawnComponent));
            _robotSpawnEntity = robotSpawnQuery.GetSingletonEntity();

            _hangarsService = ServiceLocator.GetService<HangarsService>();

            SetupSpawnDictionaries();

            AddListeners();

            SpawnWorld();
        }

        private void SetupSpawnDictionaries()
        {
            _spawnedHangarsByPositionId = new Dictionary<int, GameObject>
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
            var spawnComponent = _entityManager.GetComponentData<RobotSpawnComponent>(_robotSpawnEntity);
            var newComponent = new RobotSpawnComponent()
            {
                RobotPrefab = spawnComponent.RobotPrefab,
                SpawnPosition = spawnComponent.SpawnPosition,
                AmountToSpawn = spawnComponent.AmountToSpawn + amount
            };
            _entityManager.SetComponentData(_robotSpawnEntity, newComponent);
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
    }
}