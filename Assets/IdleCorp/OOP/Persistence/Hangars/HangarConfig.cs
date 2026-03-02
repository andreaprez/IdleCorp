using System;
using IdleCorp.OOP.Business.Hangars;
using UnityEngine;

namespace IdleCorp.OOP.Persistence.Hangars
{
    [Serializable]
    public class HangarConfig
    {
        public int Id;
        public string Name;
        public int Capacity;
        public Sprite Image; 
        public HangarWorldObject Prefab; 
        public int Cost;
    }
}