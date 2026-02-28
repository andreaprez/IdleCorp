using System.Collections.Generic;
using UnityEngine;

namespace IdleCorp.OOP.Persistence.Hangars
{
    [CreateAssetMenu(menuName = "IdleCorp/Config/Hangars")]
    public class HangarsConfig : ScriptableObject
    {
        public List<HangarConfig> Hangars;
    }
}