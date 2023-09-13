using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;

    [Header("Move")]
    public float speed;


    [Header("Jump")]
    public float jumpPower;
    public float gravity;
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


        Vector3 moveDirection = (cameraForward * vAxis + Camera.main.transform.right * hAxis).normalized;


        Vector3 newPosition = transform.position + moveDirection * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1000f * Time.deltaTime);
        }

        if (hAxis != 0 || vAxis != 0)
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
