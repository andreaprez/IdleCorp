using IdleCorp.ECS.Components;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events;
using Unity.Entities;
using UnityEngine;

namespace IdleCorp.OOP.Business
{
    public class Spawner : MonoBehaviour
    {
        private EntityManager _entityManager;
        private Entity _robotSpawnEntity;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var robotSpawnQuery = World.DefaultGameObjectInjectionWorld.EntityManager
                .CreateEntityQuery(ComponentType.ReadOnly<RobotSpawnComponent>());
            _robotSpawnEntity = robotSpawnQuery.GetSingletonEntity();

            var eventsService = ServiceLocator.GetService<EventsService>();
            //TODO: listen to events to spawn elements:
            // eventsService.GetEvent<RobotProducedEvent>().AddListener(SpawnRobot);
            // eventsService.GetEvent<HangarPurchasedEvent>().AddListener(SpawnHangar);
            // eventsService.GetEvent<VehicleDeployedEvent>().AddListener(SpawnVehicle);
            // eventsService.GetEvent<ShootingStarGeneratedEvent>().AddListener(SpawnShootingStar);
        }

        public void SpawnWorld()
        {
            //TODO: spawn Mine
            //TODO: spawn Hangars
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

        private void SpawnHangar(int hangarId, int positionId)
        {
            //TODO
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