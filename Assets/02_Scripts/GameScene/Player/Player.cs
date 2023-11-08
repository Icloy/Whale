using UnityEngine;
using MNF;

namespace whale
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform cam;
        public Animator anim;
        public GameObject walk;

        [Header("Move")]
        [SerializeField] private float speed;

        NetVector3 prevTransform0 = new NetVector3(0, 0, 0);
        NetVector3 prevTransform1 = new NetVector3(0, 0, 0);

        private float turnSmoothTime = 0.1f;
        float turnSmoothVelocity;

        [Header("Jump")]
        [SerializeField] private float jumpPower;
        [SerializeField] private float gravity;
        private Vector3 playerVelocity;
        private bool groundedPlayer;

        void Start()
        {
            Lock();
            controller = GetComponent<CharacterController>();
            cam = GameObject.Find("Main Camera").transform;
            if (controller == null)
            {
                controller = gameObject.AddComponent<CharacterController>();
            }
        }

        private void Update()
        {
            if (gameObject.name != MainManager.Instance.netGameManager.m_userHandle.m_szUserID)
                return;

            PlayerMove();
            PlayerJump();

            UserSession userSession = MainManager.Instance.netGameManager.GetRoomUserSession(
                MainManager.Instance.netGameManager.m_userHandle.m_szUserID);
            if (userSession == null) return;

            if (prevTransform0.Equals(userSession.m_userTransform[0]) && prevTransform1.Equals(userSession.m_userTransform[1]))
                return;

            userSession.m_userTransform[0] = prevTransform0;
            userSession.m_userTransform[1] = prevTransform1;

            MainManager.Instance.networkManager.Send_ROOM_USER_MOVE_DIRECT(userSession);
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
                anim.SetBool("Run", true);
                MainManager.Instance.titleManager.ObjectInteraction(MainManager.Instance.statusContainer.userNum, "Player", 0);
                walk.SetActive(true);

            }
            else
            {
                anim.SetBool("Run", false);
                walk.SetActive(false);
            }

            prevTransform0 = new NetVector3(transform.position);
            prevTransform1 = new NetVector3(transform.rotation.eulerAngles);

        }

        void PlayerJump()
        {
            RaycastHit hit;
            groundedPlayer = Physics.Raycast(transform.position, Vector3.down, out hit, 1.3f) && hit.collider.CompareTag("Ground");

            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
                anim.SetBool("Jump", false);
            }

            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpPower * -3.0f * gravity);
                anim.SetBool("Jump", true);
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

        public void Init(UserSession user)
        {
            gameObject.name = user.m_szUserID;
        }
    }
}