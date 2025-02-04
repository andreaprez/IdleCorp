using IdleCorp.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace IdleCorp.ECS.Authoring
{
    public class RobotSpawnAuthoring : MonoBehaviour
    {
        public GameObject RobotPrefab;
    }

    public class RobotSpawnBaker : Baker<RobotSpawnAuthoring>
    {
        public override void Bake(RobotSpawnAuthoring authoring)
        {
            var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            AddComponent(entity, new RobotSpawnComponent
            {
                RobotPrefab = GetEntity(authoring.RobotPrefab, TransformUsageFlags.Dynamic),
                SpawnPosition = new float3(-1.2f, 0, 2.8f) //TODO: get from service
            });
        }
    }
}