using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class Player : MonoBehaviour
    {
        public CharacterController controller;
        public Transform cam;

        [Header("Move")]
        [SerializeField] private float speed;

        private float turnSmoothTime = 0.1f;
        float turnSmoothVelocity;

        [Header("Jump")]
        [SerializeField] private float jumpPower;
        [SerializeField] private float gravity;
        private Vector3 playerVelocity;
        private bool groundedPlayer;


        // Start is called before the first frame update
        void Start()
        {
            Lock();
            controller = GetComponent<CharacterController>();
            if (controller == null)
            {
                controller = gameObject.AddComponent<CharacterController>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            PlayerMove();
            PlayerJump();
        }

        void PlayerMove()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            if (horizontal != 0 || vertical != 0)
            {
                PlayerAnim.Instance.ChangeState(PlayerAnim.PlayerState.Walk);
            }
            else
            {
                PlayerAnim.Instance.ChangeState(PlayerAnim.PlayerState.Idle);
            }

        }

        void PlayerJump()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpPower * -3.0f * gravity);
            }

            playerVelocity.y += gravity * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }

        public void unLock() //마우스커서 보이게
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        public void Lock() //마우스 커서 안보이게
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}