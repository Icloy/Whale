using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace whale
{
    public class MazeButton : MonoBehaviour
    {
        public Maze maze;

        [SerializeField] private float moveDistance = 2.0f; // ������ �Ÿ�
        [SerializeField] private float moveSpeed = 1.0f;    // �����̴� �ӵ�
        [SerializeField] private GameObject buttonObject;   // ���ǿ� ����� ��ư ������Ʈ

        private Vector3 initialPosition;  // �ʱ� ��ġ
        private Vector3 targetPosition;   // ��ǥ ��ġ
        private bool isMoving = false;    // ������ �����̴� ������ ����

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
                name = button.name;
                switch (name)
                {
                    case "Red":
                        maze.isRed = true;
                        break;
                    case "Green":
                        maze.isGreen = true;
                        break;
                    case "Blue":
                        maze.isBlue = true;
                        break;
                }
            }
        }

        private void ResetPlatform(GameObject button)
        {
            float step = moveSpeed * Time.deltaTime;
            button.transform.position = Vector3.MoveTowards(button.transform.position, initialPosition, step);

            if (button.transform.position == initialPosition)
            {
                name = button.name;
                switch (name)
                {
                    case "Red":
                        maze.isRed = false;
                        break;
                    case "Green":
                        maze.isGreen = false;
                        break;
                    case "Blue":
                        maze.isBlue = false;
                        break;
                }
            }
        }

    }
}
