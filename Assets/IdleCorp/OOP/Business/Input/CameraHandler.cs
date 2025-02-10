using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Input;
using UnityEngine;

namespace IdleCorp.OOP.Business.Input
{
    public class CameraHandler : MonoBehaviour
    {
        private const float MOVEMENT_SPEED = 1.2f;
        private const float REPOSITION_SPEED = 3f;
        private const float OUT_OF_LIMIT_SPEED_MULTIPLIER = 0.25f;
        private const float LIMIT_DISTANCE_TO_CENTER = 4f;

        private Vector3 _centerPosition;
        private bool _isRepositioning;

        private void Start()
        {
            _centerPosition = transform.position;
            ServiceLocator.GetService<EventsService>().GetEvent<InputDraggedEvent>().AddListener(OnInputDragged);
            ServiceLocator.GetService<EventsService>().GetEvent<InputDragStoppedEvent>().AddListener(OnInputDragStopped);
        }

        private void Update()
        {
            if (_isRepositioning)
                HandleRepositioning();
        }

        private void HandleRepositioning()
        {
            var moveDirection = _centerPosition - transform.position;
            moveDirection.y = 0f;
            
            transform.position += moveDirection * (REPOSITION_SPEED * Time.deltaTime);

            if (GetDistanceToCenter() < LIMIT_DISTANCE_TO_CENTER)
                _isRepositioning = false;
        }

        private void OnInputDragged(Vector2 dragDirection)
        {
            Vector3 moveDirection = transform.forward * dragDirection.y
                                    + transform.right * dragDirection.x;

            var moveSpeed = MOVEMENT_SPEED;
            var distanceToCenter = GetDistanceToCenter();
            if (distanceToCenter > LIMIT_DISTANCE_TO_CENTER)
                moveSpeed /= distanceToCenter * OUT_OF_LIMIT_SPEED_MULTIPLIER;

            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
        }

        private void OnInputDragStopped()
        {
            if (GetDistanceToCenter() > LIMIT_DISTANCE_TO_CENTER)
                _isRepositioning = true;
        }

        private float GetDistanceToCenter()
        {
            var distanceVector = transform.position - _centerPosition;
            distanceVector.y = 0f;
            return Vector3.Magnitude(distanceVector);
        }
    }
}