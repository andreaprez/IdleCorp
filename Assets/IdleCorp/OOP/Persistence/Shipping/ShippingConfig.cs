using System.Collections.Generic;
using UnityEngine;

namespace IdleCorp.OOP.Persistence.Shipping
{
    [CreateAssetMenu(menuName = "IdleCorp/Config/Shipping")]
    public class ShippingConfig : ScriptableObject
    {
        public List<VehicleConfig> Vehicles;
        public int VehicleSpawnSecondsInterval;
    }
}