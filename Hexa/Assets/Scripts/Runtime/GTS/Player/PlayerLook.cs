using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

namespace GTS.Player
{
    /// <summary>
    /// Handles controlling player camera to allow looking around
    /// </summary>
    public class PlayerLook : MonoBehaviour
    {
        [BoxGroup("Mouse Settings")]
        public float lookSensitvity = 100f;

        [BoxGroup("References")]
        public Transform playerCamera;

        // Holds value from input to be used for looking
        private Vector2 lookValue;

        // Holds values for the player xRotation
        private float xRotation = 0f;

        // Unity Methods
        private void Update()
        {
            float mouseX = lookValue.x * lookSensitvity * Time.deltaTime;
            float mouseY = lookValue.y * lookSensitvity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }

        // Input Handling Methods
        private void OnLook(InputValue value)
        {
            lookValue = value.Get<Vector2>();
        }
    }
}
