using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace whale
{
    public class MazeButton : MonoBehaviour
    {
        [SerializeField] private float moveDistance = 2.0f; // 움직일 거리
        [SerializeField] private float moveSpeed = 1.0f;    // 움직이는 속도
        [SerializeField] private GameObject buttonObject;   // 발판에 연결된 버튼 오브젝트

        private Vector3 initialPosition;  // 초기 위치
        private Vector3 targetPosition;   // 목표 위치
        [SerializeField] private bool isMoving = false;    // 발판이 움직이는 중인지 여부

        private void Start()
        {
            initialPosition = transform.position;
            targetPosition = initialPosition + Vector3.down * moveDistance;
        }

        private void Update()
        {
            if (isMoving)
            {
                MovePlatform(buttonObject);
            }
            else
            {
                ResetPlatform(buttonObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isMoving = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            isMoving = false;
        }

        private void MovePlatform(GameObject button)
        {
            float step = moveSpeed * Time.deltaTime;
            button.transform.position = Vector3.MoveTowards(button.transform.position, targetPosition, step);

            if (button.transform.position == targetPosition)
            {
                //눌렀을 때 코드
            }
        }

        private void ResetPlatform(GameObject button)
        {
            float step = moveSpeed * Time.deltaTime;
            button.transform.position = Vector3.MoveTowards(button.transform.position, initialPosition, step);

            if (button.transform.position == initialPosition)
            {
                // 다시 돌아왔을때 코드
            }
        }

    }
}

