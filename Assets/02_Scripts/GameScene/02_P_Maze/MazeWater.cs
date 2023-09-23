using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace whale
{
    public class MazeWater : MonoBehaviour
    {
        [SerializeField] private float moveDistance; // ������ �Ÿ�
        [SerializeField] private float moveSpeed;    // �����̴� �ӵ�

        private Vector3 initialPosition;  // �ʱ� ��ġ
        private Vector3 targetPosition;   // ��ǥ ��ġ

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
