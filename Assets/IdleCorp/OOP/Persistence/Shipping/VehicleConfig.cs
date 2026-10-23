using System;
using UnityEngine;

namespace IdleCorp.OOP.Persistence.Shipping
{
    [Serializable]
    public class VehicleConfig
    {
        public int Id;
        public string Name;
        public int ShippingRate;
        public Sprite Image; 
        public GameObject Prefab; 
        public int Cost;
    }
}