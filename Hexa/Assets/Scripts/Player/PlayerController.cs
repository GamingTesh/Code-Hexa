using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

namespace GTS.Player
{
    /// <summary>
    /// Handles moving the player
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        private float currentSpeed;
        [BoxGroup("Movement")]
        public float speed = 12f;
        [BoxGroup("Movement")]
        public float sprintSpeed = 20f;
        [BoxGroup("Movement")]
        public float jumpHeight = 3f;

        [BoxGroup("Ground")]
        public float gravity = -30f;
        [BoxGroup("Ground")]
        public float groundDistance = 0.4f;
        [BoxGroup("Ground")]
        public Transform groundCheck;
        [BoxGroup("Ground")]
        public LayerMask groundMask;

        // Vectors to control hold input and movement values
        private Vector3 velocity;
        private Vector2 moveValue;

        // Booleans to hold current states
        private bool isGrounded;
        private bool isSprinting;

        // Reference to required behaviours
        private CharacterController controller;

        // Unity Methods
        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (isSprinting && isGrounded)
            {
                currentSpeed = sprintSpeed;
            }
            else
            {
                currentSpeed = speed;
            }

            Vector3 move = transform.right * moveValue.x + transform.forward * moveValue.y;

            controller.Move(move * currentSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

        // Input Handling Methods
        private void OnJump(InputValue value)
        {
            if (value.Get<float>() == 1 && isGrounded)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        private void OnMove(InputValue value)
        {
            moveValue = value.Get<Vector2>();
        }

        private void OnSprint(InputValue value)
        {
            if (value.Get<float>() == 1)
                isSprinting = true;
            else
                isSprinting = false;
        }
    }
}
