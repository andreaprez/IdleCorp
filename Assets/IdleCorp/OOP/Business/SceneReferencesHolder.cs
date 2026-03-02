using IdleCorp.OOP.Business.Hangars;
using UnityEngine;

namespace IdleCorp.OOP.Business
{
    public class SceneReferencesHolder : MonoBehaviour
    {
        [Header("World")] 
        [SerializeField] 
        public Transform FactorySpawnPoint;

        [Header("Popups")] 
        [SerializeField] 
        public HangarsPopupView HangarsPopup;
        [SerializeField] 
        public HangarStorePopupView HangarStorePopup;

        private static SceneReferencesHolder _instance;
        public static SceneReferencesHolder Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(this);
        }
    }
}