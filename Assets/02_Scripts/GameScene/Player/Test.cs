using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    public float jumpPower;
    public float gravity;

    private Rigidbody rigid;
    bool isJump;
    Vector3 moveVec;

    // Start is called before the first frame update
    void Start()
    {
        Lock();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    void PlayerMove()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 moveInput = new Vector3(hAxis, 0, vAxis).normalized;
        Vector3 moveDirection = cameraForward * moveInput.z + Camera.main.transform.right * moveInput.x;

        transform.position += moveDirection * (speed * 0.1f);

        if (CheckHitWall(moveInput))
            moveInput = Vector3.zero;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }


    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
        }

        if (isJump)
        {
            rigid.AddForce(Vector3.down * gravity * Time.deltaTime, ForceMode.Impulse);
            PlayerAnim.Instance.ChangeState(PlayerAnim.PlayerState.Jump);
        }
    }

    bool CheckHitWall(Vector3 movement)
    {
        // 움직임에 대한 로컬 벡터를 월드 벡터로 변환해준다.
        movement = transform.TransformDirection(movement);
        // scope로 ray 충돌을 확인할 범위를 지정할 수 있다.
        float scope = 1f;

        // 플레이어의 머리, 가슴, 발 총 3군데에서 ray를 쏜다.
        List<Vector3> rayPositions = new List<Vector3>();
        rayPositions.Add(transform.position + Vector3.up * 0.1f);

        // 디버깅을 위해 ray를 화면에 그린다.
        foreach (Vector3 pos in rayPositions)
        {
            Debug.DrawRay(pos, movement * scope, Color.red);
        }

        // ray와 벽의 충돌을 확인한다.
        foreach (Vector3 pos in rayPositions)
        {
            if (Physics.Raycast(pos, movement, out RaycastHit hit, scope))
            {
                if (hit.collider.CompareTag("Wall"))
                    return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
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
