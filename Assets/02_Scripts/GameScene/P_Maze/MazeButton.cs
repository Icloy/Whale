using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace whale
{
    public class MazeButton : MonoBehaviour
    {
        [SerializeField] private float moveDistance = 2.0f; // ������ �Ÿ�
        [SerializeField] private float moveSpeed = 1.0f;    // �����̴� �ӵ�
        private Vector3 initialPosition;  // �ʱ� ��ġ
        private Vector3 targetPosition;   // ��ǥ ��ġ
        private bool isMoving = false;    // ������ �����̴� ������ ����

        [SerializeField] private GameObject RedBtn;
        [SerializeField] private GameObject GreenBtn;
        [SerializeField] private GameObject BlueBtn;

        bool isRed = false;
        bool isGreen = false;
        bool isBlue = false;

        private void Start()
        {
            initialPosition = transform.position;
            targetPosition = initialPosition + Vector3.down * moveDistance;
        }

        private void Update()
        {
            if (isRed)
            {
                RedMove();
            }
            else if(isGreen)
            {
                GreenMove();
            }
            else if (isBlue)
            {
                BlueMove();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isMoving = true;
                switch (gameObject.name)
                {
                    case "RedCol":
                        isRed = true;
                        break;
                    case "GreenCol":
                        isGreen = true;
                        break;
                    case "BlueCol":
                        isBlue = true;
                        break;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            isRed = false;
            isGreen = false;
            isBlue = false;

            // ������ ���� ��ġ�� �ǵ���
            if (isMoving)
            {
                isMoving = false;
                ResetPlatformPosition();
            }
        }

        void RedMove()
        {
            if (isMoving)
            {
                // ������ �Ʒ��� �����̴� �ڵ�
                float step = moveSpeed * Time.deltaTime;
                RedBtn.transform.position = Vector3.MoveTowards(RedBtn.transform.position, targetPosition, step);

                // ��ǥ ��ġ�� �����ϸ� �������� ����
                if (RedBtn.transform.position == targetPosition)
                {
                    isMoving = false;
                }
            }
        }

        void GreenMove()
        {
            if (isMoving)
            {
                // ������ �Ʒ��� �����̴� �ڵ�
                float step = moveSpeed * Time.deltaTime;
                GreenBtn.transform.position = Vector3.MoveTowards(GreenBtn.transform.position, targetPosition, step);

                // ��ǥ ��ġ�� �����ϸ� �������� ����
                if (GreenBtn.transform.position == targetPosition)
                {
                    isMoving = false;
                }
            }
        }

        void BlueMove()
        {
            if (isMoving)
            {
                // ������ �Ʒ��� �����̴� �ڵ�
                float step = moveSpeed * Time.deltaTime;
                BlueBtn.transform.position = Vector3.MoveTowards(BlueBtn.transform.position, targetPosition, step);

                // ��ǥ ��ġ�� �����ϸ� �������� ����
                if (BlueBtn.transform.position == targetPosition)
                {
                    isMoving = false;
                }
            }
        }

        void ResetPlatformPosition()
        {
            // ������ �ʱ� ��ġ�� �ǵ���
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, step);

            // �ʱ� ��ġ�� �����ϸ� �������� ����
            if (transform.position == initialPosition)
            {
                // ���⿡ �ʱ� ��ġ�� �������� �� ������ �ڵ带 �߰��� �� �ֽ��ϴ�.
            }
        }

    }
}

