using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.UserData;
using TMPro;
using UnityEngine;

public class RobotCountDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro robotCountText;

    /*private HangarsData _hangarsData;

    private void Start()
    {
        _hangarsData = ServiceLocator.GetService<UserDataService>().GetData<HangarsData>();
    }

    void Update()
    {
        robotCountText.SetText(_hangarsData.TotalRobotCount.ToString());
    }*/
}
