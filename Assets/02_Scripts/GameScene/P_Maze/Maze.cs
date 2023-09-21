using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class Maze : MonoBehaviour
    {
        [SerializeField] private float moveDistance; // 움직일 거리
        [SerializeField] private float moveSpeed;    // 움직이는 속도
        [SerializeField] private GameObject buttonObject;   // 발판에 연결된 버튼 오브젝트


        private Vector3 initialPosition;  // 초기 위치
        private Vector3 targetPosition;   // 목표 위치

        public bool isRed = false;
        public bool isGreen = false;
        public bool isBlue = false;

        // Start is called before the first frame update
        void Start()
        {
            initialPosition = transform.position;
            targetPosition = initialPosition + Vector3.down * moveDistance;
        }

        // Update is called once per frame
        void Update()
        {
            if (isRed || isGreen || isBlue)
            {
                MovePlatform(buttonObject);
            }
            else
            {
                ResetPlatform(buttonObject);
            }
        }

        private void MovePlatform(GameObject button)
        {
            float step = moveSpeed * Time.deltaTime;
            button.transform.position = Vector3.MoveTowards(button.transform.position, targetPosition, step);


        }

        private void ResetPlatform(GameObject button)
        {
            float step = moveSpeed * Time.deltaTime;
            button.transform.position = Vector3.MoveTowards(button.transform.position, initialPosition, step);
        }
    }
}

