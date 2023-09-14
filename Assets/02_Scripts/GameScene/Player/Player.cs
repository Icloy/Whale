using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class Player : MonoBehaviour
    {
        private Rigidbody rigid;
        public CharacterController controller;
        public Transform cam;

        [Header("Move")]
        [SerializeField] private float speed;

        public float turnSmoothTime = 0.1f;
        float turnSmoothVelocity;

        [Header("Jump")]
        [SerializeField] private float jumpPower;
        [SerializeField] private float gravity;
        bool isJump;



        // Start is called before the first frame update
        void Start()
        {
            Lock();
            rigid = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            PlayerJump();
        }

        private void LateUpdate()
        {
            PlayerMove();
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
            // 플레이어가 땅에 있고, 점프 키를 눌렀을 때 점프
            if (Input.GetKeyDown(KeyCode.Space) && !isJump)
            {
                // 점프 힘을 적용합니다.
                Vector3 jumpVector = new Vector3(0f, jumpPower, 0f);
                controller.Move(jumpVector * Time.deltaTime);

                // 점프 중임을 표시합니다.
                isJump = true;

                // 점프 애니메이션을 재생하거나 상태를 변경할 수 있습니다.
                PlayerAnim.Instance.ChangeState(PlayerAnim.PlayerState.Jump);
            }

            // 중력을 적용합니다.
            Vector3 gravityVector = new Vector3(0f, -gravity, 0f);
            controller.Move(gravityVector * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                // 땅과 충돌했을 때 점프 상태를 초기화합니다.
                isJump = false;

                // 땅과 충돌한 경우에만 Idle 상태로 변경할 수 있습니다.
                PlayerAnim.Instance.ChangeState(PlayerAnim.PlayerState.Idle);
            }
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