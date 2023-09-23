using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace whale
{
    public class MazeWater : MonoBehaviour
    {
        [SerializeField] private float moveDistance; // 움직일 거리
        [SerializeField] private float moveSpeed;    // 움직이는 속도

        private Vector3 initialPosition;  // 초기 위치
        private Vector3 targetPosition;   // 목표 위치

        [SerializeField] bool isMaze;

        // Start is called before the first frame update
        void Start()
        {
            targetPosition = initialPosition + Vector3.up * moveDistance;
        }

        // Update is called once per frame
        void Update()
        {
            if (isMaze)
            {
                MovePlatform();
            }
        }

        private void MovePlatform()
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("11");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("2");
            }
        }
    }
}
