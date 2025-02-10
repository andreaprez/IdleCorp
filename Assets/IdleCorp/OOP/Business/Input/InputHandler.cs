using System;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Input;
using UnityEngine;

namespace IdleCorp.OOP.Business.Input
{
    public class InputHandler : MonoBehaviour
    {
        private const float DRAG_THRESHOLD_FOR_TAP = 2f;
        private const float RAYCAST_MAX_DISTANCE = 60;
        private const string WORLD_INTERACTABLE_LAYER_NAME = "WorldInteractable";

        private EventsService _eventsService;
        private Vector2 _firstDragPosition;
        private Vector2 _lastDragPosition;
        private bool _isDragging;
        private bool _isTapping;
        private int _worldInteractableLayerMask;

        private void Start()
        {
            _eventsService = ServiceLocator.GetService<EventsService>();
            _worldInteractableLayerMask = 1 << LayerMask.NameToLayer(WORLD_INTERACTABLE_LAYER_NAME);
        }

        private void Update()
        {
            UpdateInputState();
            if (_isDragging)
                HandleDrag();
            else if (_isTapping)
                HandleTap();
        }

        private void UpdateInputState()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                _firstDragPosition = UnityEngine.Input.mousePosition;
                _lastDragPosition = UnityEngine.Input.mousePosition;
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
                _eventsService.GetEvent<InputDragStoppedEvent>().Trigger();
                if (Vector2.Distance(_firstDragPosition, UnityEngine.Input.mousePosition) <= DRAG_THRESHOLD_FOR_TAP)
                    _isTapping = true;
            }
        }

        private void HandleDrag()
        {
            var dragDirection = _lastDragPosition - (Vector2)UnityEngine.Input.mousePosition;
            _eventsService.GetEvent<InputDraggedEvent>().Trigger(dragDirection);
            _lastDragPosition = UnityEngine.Input.mousePosition;
        }

        private void HandleTap()
        {
            _isTapping = false;

            var ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, RAYCAST_MAX_DISTANCE, _worldInteractableLayerMask))
                return;
            
            var hitObjectTag = hit.transform.gameObject.tag;
            if (Enum.TryParse(typeof(WorldInteractableTag), hitObjectTag, out var worldInteractableTag));
            {
                var worldInteractable = (WorldInteractableTag)worldInteractableTag;
                _eventsService.GetEvent<InputTappedOnWorldInteractableEvent>().Trigger(worldInteractable);
            }
        }
    }
}