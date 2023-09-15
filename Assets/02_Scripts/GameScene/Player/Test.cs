using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private LayerMask _fieldLayer;

    private void Start()
    {
        Lock();
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
        }
    }

    void Update()
    {
        if (controller == null)
        {
            return; // controller�� ���� �ʱ�ȭ���� ���� ��� Update�� ����
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }



    public void unLock() //���콺Ŀ�� ���̰�
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Lock() //���콺 Ŀ�� �Ⱥ��̰�
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private bool IsCheckGrounded()
    {
        // CharacterController.IsGrounded�� true��� Raycast�� ������� �ʰ� ���� ����
        if (controller.isGrounded) return true;
        // �߻��ϴ� ������ �ʱ� ��ġ�� ����
        // �ణ ��ü�� ���� �ִ� ��ġ�κ��� �߻����� ������ ����� ������ �� ���� ���� �ִ�.
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        // Ž�� �Ÿ�
        var maxDistance = 1.5f;
        // ���� ����� �뵵
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.red);
        // Raycast�� hit ���η� ����
        // ���󿡸� �浹�� ���̾ ����
        return Physics.Raycast(ray, maxDistance, _fieldLayer);
    }
}
