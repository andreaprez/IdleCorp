using IdleCorp.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace IdleCorp.ECS.Authoring
{
    public class VehicleAuthoring : MonoBehaviour
    {
    }
    
    public class VehicleBaker : Baker<VehicleAuthoring>
    {
        public override void Bake(VehicleAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            AddComponent(entity, new MovementComponent());
        }
    }
}