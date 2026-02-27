using System.Collections.Generic;
using IdleCorp.OOP.Business.UI.Scripts;
using UnityEngine;

namespace IdleCorp.OOP.Persistence.Hangars
{
    [CreateAssetMenu(menuName = "IdleCorp/Config/Hangars")]
    public class HangarsConfig : ScriptableObject
    {
        public List<HangarConfig> Hangars;
    }
}