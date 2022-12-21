using UnityEngine;

namespace Code.Scripts.Mechanics
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick floatingJoystick;
        [SerializeField] private float dragSpeed;
        public static float Horizontal { get; private set; }
        public static float Vertical { get; private set; }
        public static bool IsMoving { get; private set; } = true;

        private Touch _touch;

        private void Update()
        {
            if (IsMoving)
            {
                if (Input.touchCount > 0)
                {
                    _touch = Input.GetTouch(0);
                    if (_touch.phase == TouchPhase.Moved)
                    {
                        Horizontal = Vector3.Lerp(new Vector3(Horizontal, 0), _touch.deltaPosition * dragSpeed, .5f * Time.deltaTime).x;
                        Vertical = Vector3.Lerp(new Vector3(0, Vertical), _touch.deltaPosition * dragSpeed, .5f * Time.deltaTime).y;
                    }
                }
            }
        }
    }
}