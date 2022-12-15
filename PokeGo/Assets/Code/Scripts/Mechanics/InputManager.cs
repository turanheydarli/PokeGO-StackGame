using UnityEngine;

namespace Code.Scripts.Mechanics
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick floatingJoystick;
        public static float Horizontal { get; private set; }
        public static float Vertical { get; private set; }
        public static bool IsMoving { get; private set; } = true;

        private void Update()
        {
            if (IsMoving)
            {
                // Horizontal = floatingJoystick.Horizontal;
                // Vertical = floatingJoystick.Vertical;
                Horizontal = Input.GetAxis("Horizontal");
                Vertical = Input.GetAxis("Vertical");
            }
        }
    }
}