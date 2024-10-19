using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events;
using UnityEngine;

namespace IdleCorp.OOP.Business
{
    public class Spawner : MonoBehaviour
    {
        private void Start()
        {
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

        private void SpawnRobot()
        {
            //TODO
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