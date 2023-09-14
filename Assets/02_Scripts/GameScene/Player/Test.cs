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
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * (speed * 0.1f);

        transform.LookAt(transform.position + moveVec);

        PlayerJump();
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
}
