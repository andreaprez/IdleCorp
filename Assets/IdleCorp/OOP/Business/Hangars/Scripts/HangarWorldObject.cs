using UnityEngine;

namespace IdleCorp.OOP.Business.Hangars
{
    public class HangarWorldObject : MonoBehaviour
    {
        [SerializeField]
        private Transform robotTargetPosition;

        public Transform RobotTargetPosition => robotTargetPosition;
    }
}