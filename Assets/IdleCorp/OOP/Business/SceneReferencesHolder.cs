using IdleCorp.OOP.Business.Hangars;
using IdleCorp.OOP.Business.Shipping;
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
        [SerializeField] 
        public ShippingPopupView ShippingPopup;
        [SerializeField] 
        public VehicleStorePopupView VehicleStorePopup;

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