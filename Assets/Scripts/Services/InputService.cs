using UnityEngine;

namespace Assets.Scripts.Services
{
    public class InputService : IInputService
    {
        public float AllowedMovement = 8;

        private IClickable _clickedObject;
        private Vector3 _startMousePos;

        public void Update()
        {
            IClickable clickable;
            if (CheckButtonClick(0, out clickable))
            {
                clickable.InputToFlip();
            }

            if (CheckButtonClick(1, out clickable))
            {
                clickable.InputToMark();
            }
        }

        private bool CheckButtonClick(int buttonId, out IClickable clickable)
        {
            if (Input.GetMouseButtonDown(buttonId))
            {
                if (_clickedObject != null)
                {
                    clickable = null;
                    return false;
                }

                var origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(origin, Vector2.zero, 200);
                //_position = hit.point;
                if (hit.collider != null &&
                    hit.collider.gameObject.GetComponent<IClickable>() != null)
                {
                    _startMousePos = Input.mousePosition;

                    _clickedObject = hit.collider.gameObject.GetComponent<IClickable>();
                }
            }

            //double check to null because unity mess up with Missings in interfaces
            if (_clickedObject == null || _clickedObject.Equals(null))
            {
                _clickedObject = null;
            }
            else if (Input.GetMouseButtonUp(buttonId))
            {
                var mouseDelta = Mathf.Abs(Input.mousePosition.magnitude - _startMousePos.magnitude);
                if (mouseDelta < AllowedMovement)
                {
                    clickable = _clickedObject;
                    return true;
                }

                clickable = _clickedObject;
                return false;
            }

            clickable = null;
            return false;
        }
    }
}
