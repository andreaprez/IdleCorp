using IdleCorp.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace IdleCorp.ECS.Authoring
{
    public class VehicleAuthoring : MonoBehaviour
    {
        public float Speed;
        public float LifeTimeSeconds;
    }
    
    public class VehicleBaker : Baker<VehicleAuthoring>
    {
        public override void Bake(VehicleAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            AddComponent(entity, new SimpleMovementComponent
            {
                Speed = authoring.Speed
            });
            AddComponent(entity, new LifeTimeComponent
            {
                RemainingSeconds = authoring.LifeTimeSeconds
            });
        }
    }
}