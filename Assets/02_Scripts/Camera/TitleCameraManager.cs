using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class TitleCameraManager : MonoBehaviour
    {
        [Header("CameraPos")] 
        [SerializeField] float radius;  // ���� ������
        [SerializeField] float speed;   // �̵� �ӵ�
        [SerializeField] float yPos;    // ī�޶� ����
        [SerializeField] float zPos;    // �߽������� �Ÿ�

        [Header("OT&Caching")]
        private float angle;
        Vector3 target;
        float x;
        float z;

        private void Start()
        {
            target = transform.position;
            transform.position = new Vector3(0f, yPos, zPos);
            transform.LookAt(target);
        }

        void FixedUpdate()
        {
            angle += speed * Time.deltaTime;  // �ð��� ���� ���� ����

            x = Mathf.Cos(angle) * radius;
            z = Mathf.Sin(angle) * radius;

            transform.position = new Vector3(x, transform.position.y, z);
            transform.LookAt(target);
        }
    }
}
