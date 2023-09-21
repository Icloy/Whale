using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class Maze : MonoBehaviour
    {
        [SerializeField] private float moveDistance; // ������ �Ÿ�
        [SerializeField] private float moveSpeed;    // �����̴� �ӵ�
        [SerializeField] private GameObject buttonObject;   // ���ǿ� ����� ��ư ������Ʈ


        private Vector3 initialPosition;  // �ʱ� ��ġ
        private Vector3 targetPosition;   // ��ǥ ��ġ

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

