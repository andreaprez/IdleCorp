using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.EventsService;
using IdleCorp.OOP.Services.EventsService.Events;
using UnityEngine;

namespace IdleCorp.OOP.Business
{
    public class InputHandler : MonoBehaviour
    {
        private EventsService _eventsService;
        private Vector2 _lastDragPosition;
        private bool _isDragging;

        private void Start()
        {
            _eventsService = ServiceLocator.GetService<EventsService>();
        }

        private void Update()
        {
            UpdateDraggingState();
            if (_isDragging)
                HandleDragging();
        }

        private void UpdateDraggingState()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                _lastDragPosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                _eventsService.GetEvent<CameraDragStoppedEvent>().Trigger();
            }
        }

        private void HandleDragging()
        {
            var dragDirection = _lastDragPosition - (Vector2)Input.mousePosition;
            _eventsService.GetEvent<InputDraggedEvent>().Trigger(dragDirection);
            _lastDragPosition = Input.mousePosition;
        }
    }
}