using UnityEngine;

namespace IdleCorp.OOP.Persistence.Robots
{
    [CreateAssetMenu(menuName = "IdleCorp/Config/Robots")]
    public class RobotsConfig : ScriptableObject
    {
        public float TargetReachedThresholdForRobots;
    }
}