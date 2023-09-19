using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace whale
{
    public class MazeButton : MonoBehaviour
    {
        [SerializeField] private float moveDistance = 2.0f; // 움직일 거리
        [SerializeField] private float moveSpeed = 1.0f;    // 움직이는 속도
        private Vector3 initialPosition;  // 초기 위치
        private Vector3 targetPosition;   // 목표 위치
        private bool isMoving = false;    // 발판이 움직이는 중인지 여부

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

            // 발판을 원래 위치로 되돌림
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
                // 발판을 아래로 움직이는 코드
                float step = moveSpeed * Time.deltaTime;
                RedBtn.transform.position = Vector3.MoveTowards(RedBtn.transform.position, targetPosition, step);

                // 목표 위치에 도달하면 움직임을 멈춤
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
                // 발판을 아래로 움직이는 코드
                float step = moveSpeed * Time.deltaTime;
                GreenBtn.transform.position = Vector3.MoveTowards(GreenBtn.transform.position, targetPosition, step);

                // 목표 위치에 도달하면 움직임을 멈춤
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
                // 발판을 아래로 움직이는 코드
                float step = moveSpeed * Time.deltaTime;
                BlueBtn.transform.position = Vector3.MoveTowards(BlueBtn.transform.position, targetPosition, step);

                // 목표 위치에 도달하면 움직임을 멈춤
                if (BlueBtn.transform.position == targetPosition)
                {
                    isMoving = false;
                }
            }
        }

        void ResetPlatformPosition()
        {
            // 발판을 초기 위치로 되돌림
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, step);

            // 초기 위치에 도달하면 움직임을 멈춤
            if (transform.position == initialPosition)
            {
                // 여기에 초기 위치에 도달했을 때 실행할 코드를 추가할 수 있습니다.
            }
        }

    }
}

