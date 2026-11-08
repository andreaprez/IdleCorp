using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IdleCorp.ECS.Authoring;
using IdleCorp.ECS.Components;
using IdleCorp.OOP.Business.Hangars;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Factory;
using IdleCorp.OOP.Services.Events.Hangars;
using IdleCorp.OOP.Services.Events.Shipping;
using IdleCorp.OOP.Services.Factory;
using IdleCorp.OOP.Services.Hangars;
using IdleCorp.OOP.Services.Shipping;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace IdleCorp.OOP.Business
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> hangarSpawnPoints;
        [SerializeField]
        private Transform vehicleSpawnPoint;

        private EntityManager _entityManager;
        private Entity _robotSpawnEntity;
        private Entity _vehicleSpawnEntity;

        private FactoryService _factoryService;
        private HangarsService _hangarsService;
        private ShippingService _shippingService;

        private Dictionary<int, HangarWorldObject> _spawnedHangarsByPositionId;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var robotSpawnQuery = _entityManager.CreateEntityQuery(typeof(RobotSpawnComponent));
            _robotSpawnEntity = robotSpawnQuery.GetSingletonEntity();
            var vehicleSpawnQuery = _entityManager.CreateEntityQuery(typeof(PrefabPoolElement));
            _vehicleSpawnEntity = vehicleSpawnQuery.GetSingletonEntity();

            _factoryService = ServiceLocator.GetService<FactoryService>();
            _hangarsService = ServiceLocator.GetService<HangarsService>();
            _shippingService = ServiceLocator.GetService<ShippingService>();

            SetupSpawnDictionaries();

            AddListeners();

            SpawnWorld();
            StartCoroutine(StartVehicleSpawnRoutine());
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
            eventsService.GetEvent<VehicleDeployedEvent>().AddListener(SpawnVehicle);
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

        private void SpawnVehicle(int vehicleId)
        {
            var vehiclePrefabBuffer = _entityManager.GetBuffer<PrefabPoolElement>(_vehicleSpawnEntity);

            var randomIndex = Random.Range(0, vehiclePrefabBuffer.Length);
            var randomPrefabEntity = vehiclePrefabBuffer[randomIndex].Value;

            var newEntity = _entityManager.Instantiate(randomPrefabEntity);
            _entityManager.SetComponentData(newEntity, new LocalTransform
            {
                Position = vehicleSpawnPoint.position,
                Rotation =  vehicleSpawnPoint.rotation,
                Scale = 1
            });
        }

        private void SpawnShootingStar()
        {
            //TODO
        }

        private IEnumerator StartVehicleSpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_shippingService.GetVehicleSpawnSecondsInterval());

                var availableVehicles = _shippingService.GetPurchasedVehiclesInfo();
                if (availableVehicles.Count <= 0)
                {
                    yield break;
                }

                var randomIndex = Random.Range(0, availableVehicles.Count);
                var selectedVehicleId = availableVehicles.ElementAt(randomIndex).Value;
                SpawnVehicle(selectedVehicleId);
            }
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