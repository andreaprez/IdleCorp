using System;
using UnityEngine;

namespace IdleCorp.OOP.Persistence.Hangars
{
    [Serializable]
    public class HangarConfig
    {
        public int Id;
        public string Name;
        public int Capacity;
        public GameObject Prefab; 
        public Sprite Image; 
    }
}