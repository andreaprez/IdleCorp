using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace IdleCorp.ECS.Authoring
{
    public class VehicleSpawnAuthoring : MonoBehaviour
    {
        public List<GameObject> Prefabs;
    }

    public struct PrefabPoolElement : IBufferElementData
    {
        public Entity Value;
    }

    public class VehicleSpawnBaker : Baker<VehicleSpawnAuthoring>
    {
        public override void Bake(VehicleSpawnAuthoring authoring)
        {
            if (authoring.Prefabs == null || authoring.Prefabs.Count == 0)
                return;

            var entity = GetEntity(TransformUsageFlags.None);
            var buffer = AddBuffer<PrefabPoolElement>(entity);
            foreach (GameObject go in authoring.Prefabs)
            {
                if (go != null)
                {
                    var prefabEntity = GetEntity(go, TransformUsageFlags.Dynamic);
                    buffer.Add(new PrefabPoolElement { Value = prefabEntity });
                }
            }
        }
    }
}