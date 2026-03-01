using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Hangars;
using IdleCorp.OOP.Services.Hangars;
using TMPro;
using UnityEngine;

namespace IdleCorp.OOP.Business.Hangars
{
    public class RobotCountDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI robotCountText;

        private HangarsService _hangarsService;

        private void Start()
        {
            _hangarsService = ServiceLocator.GetService<HangarsService>();

            UpdateRobotCountText(_hangarsService.GetTotalRobotCount());

            var eventsService = ServiceLocator.GetService<EventsService>();
            eventsService.GetEvent<RobotCountChangedEvent>().AddListener(UpdateRobotCountText);
        }

        void UpdateRobotCountText(int newCount)
        {
            robotCountText.SetText(newCount.ToString());
        }
    }
}