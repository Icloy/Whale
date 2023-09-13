using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class TitleCameraManager : MonoBehaviour
    {
        [Header("CameraPos")] 
        [SerializeField] float radius;  // 원의 반지름
        [SerializeField] float speed;   // 이동 속도
        [SerializeField] float yPos;    // 카메라 높이
        [SerializeField] float zPos;    // 중심점과의 거리

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
            angle += speed * Time.deltaTime;  // 시간에 따른 각도 증가

            x = Mathf.Cos(angle) * radius;
            z = Mathf.Sin(angle) * radius;

            transform.position = new Vector3(x, transform.position.y, z);
            transform.LookAt(target);
        }
    }
}
