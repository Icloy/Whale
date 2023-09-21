using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace whale
{

    public class P_Cube : MonoBehaviour
    {
        [Header("All")]
        public GameObject allP_Cube;

        [Header("Cube")]
        [SerializeField] GameObject cube;
        int cubeNum = 3;
        List<GameObject> LubiksCubeObj = new List<GameObject>();
        List<Cube> LubiksCubeScript = new List<Cube>();

        [SerializeField] List<GameObject> HorinzontalLineCube = new List<GameObject>();
        [SerializeField] List<GameObject> VerticalLineCube = new List<GameObject>();
        [SerializeField] GameObject ver;
        [SerializeField] GameObject hor;


        [Header("Caching")]
        Color selColor;
        GameObject obj;
        bool isRotate;
        int v, h;
        [SerializeField] TextMeshProUGUI vText;
        [SerializeField] TextMeshProUGUI hText;
        float rotationSpeed = 5f;


        private void Start()
        {
            InitCubeProcess();
            selColor = Color.yellow;
        }

        void InitCubeProcess()
        {
            int cubeKey = 1;
            for (int i = 0; i < cubeNum; i++)
            {
                for (int x = 0; x < cubeNum; x++)
                {
                    for (int y = 0; y < cubeNum; y++)
                    {
                        obj = Instantiate(cube);
                        Cube cubeScript = obj.GetComponent<Cube>();
                        cubeScript.cubeKey = cubeKey;
                        cubeKey++;
                        obj.transform.position = new Vector3(x, y, i);
                        obj.SetActive(true);
                        obj.transform.SetParent(cube.transform.parent, false);
                        LubiksCubeObj.Add(obj);
                        LubiksCubeScript.Add(cubeScript);
                    }
                }
            }
        }


        public void SelVertical(int val)
        {
            foreach (GameObject obj in LubiksCubeObj)
            {
                if (obj.transform.position.x.Equals(val))
                {
                    VerticalLineCube.Add(obj);
                    Renderer renderer = obj.GetComponent<Renderer>();
                    renderer.material.color = selColor;
                }
            }
        }

        public void SelHorizontal(int val)
        {
            foreach (GameObject obj in LubiksCubeObj)
            {
                if (obj.transform.position.z.Equals(val))
                {
                    HorinzontalLineCube.Add(obj);
                }
            }
        }
        IEnumerator RotateCubes()
        {
            Quaternion startRotation = transform.rotation;

            // 목표 회전값 (90도 회전)
            Quaternion targetRotation = Quaternion.Euler(90, 0, 0);

            // 회전 속도
            float rotationSpeed = 90.0f; // 90도/초

            // 회전 방향
            Vector3 rotationAxis = Vector3.right;

            while (true)
            {
                // 시간당 회전 각도를 계산합니다.
                float step = rotationSpeed * Time.deltaTime;

                // 현재 회전값을 step 만큼 회전합니다.
                ver.transform.rotation *= Quaternion.AngleAxis(step, rotationAxis);

                // 회전이 목표치에 도달했는지 확인
                if (Quaternion.Angle(ver.transform.rotation, targetRotation) < 1.0f)
                {
                    ver.transform.rotation = targetRotation; // 정확히 목표 회전값으로 설정
                    yield break; // 코루틴 종료
                }

                yield return null; // 다음 프레임까지 대기
            }
        }

        #region Click
        public void Click_RotateVertical()
        {
            foreach (GameObject obj in VerticalLineCube)
            {
                obj.transform.SetParent(ver.transform);
            }
            StartCoroutine(RotateCubes());
        }

        public void Click_RotateHorizontal()
        {
            foreach (var item in HorinzontalLineCube)
            {

            }

        }

        public void Click_ChangeVerLine()
        {
            if(v > 2)
            {
                v = 0;
            }
            foreach (GameObject obj in VerticalLineCube)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                renderer.material.color = Color.gray;
            }
            VerticalLineCube.Clear();
            vText.text = "V : " + v;
            SelVertical(v);
            v++;
        }
        public void Click_ChangeHorLine()
        {

        }
        #endregion
    }

}