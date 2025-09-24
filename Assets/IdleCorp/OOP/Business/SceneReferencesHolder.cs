using UnityEngine;

public class SceneReferencesHolder : MonoBehaviour
{
    [Header("Popups")]
    [SerializeField]
    public HangarsPopupView HangarsPopup;

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
