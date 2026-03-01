using System;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Input;
using IdleCorp.OOP.Services.Events.UI;
using UnityEngine;

namespace IdleCorp.OOP.Business.UI
{
    public class PopupsHandler : MonoBehaviour
    {
        private EventsService _eventsService;
        private int _openPopupsCount;

        private void Start()
        {
            _eventsService = ServiceLocator.GetService<EventsService>();

            _eventsService.GetEvent<PopupOpenedEvent>().AddListener(OnPopupOpened);
            _eventsService.GetEvent<PopupClosedEvent>().AddListener(OnPopupClosed);
        }

        private void OnPopupOpened()
        {
            _openPopupsCount++;
            if (_openPopupsCount > 0)
                _eventsService.GetEvent<SetWorldInputEnabledEvent>().Trigger(false);
        }

        private void OnPopupClosed()
        {
            _openPopupsCount--;
            _openPopupsCount = Math.Max(_openPopupsCount, 0);
            if (_openPopupsCount == 0)
                _eventsService.GetEvent<SetWorldInputEnabledEvent>().Trigger(true);
        }
    }
}