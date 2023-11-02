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
        List<GameObject> HorizontalLineCube = new List<GameObject>();
        List<GameObject> VerticalLineCube = new List<GameObject>();

        [SerializeField] GameObject cubeLoc;
        [SerializeField] GameObject ver;
        [SerializeField] GameObject hor;
        
        [Header("Caching")]
        CubeState cubeState;
        Color selVerColor;
        Color selHorColor;
        GameObject obj;
        int v, h;
        float rotationSpeed = 90f;

        /*[Header("TestUI")]
        [SerializeField] TextMeshProUGUI vText;
        [SerializeField] TextMeshProUGUI hText;
*/
        private void Start()
        {
            InitCubeProcess();

            //선택시 색상
            selVerColor = Color.yellow;
            selHorColor = Color.green;
        }

        void InitCubeProcess() //큐브 생성
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
            PickRandomCube();
            cubeState = CubeState.None;
        }

        void PickRandomCube() //초기 큐브 랜덤 선택 
        {
            List<Cube> availableCubes = new List<Cube>(LubiksCubeScript);
            availableCubes.RemoveAll(cube => cube.cubeKey == 14); // 14번 큐브는 제외

            for (int i = 0; i < 9; i++)
            {
                if (availableCubes.Count > 0)
                {
                    int randomIndex = Random.Range(0, availableCubes.Count);
                    Cube randomCube = availableCubes[randomIndex];

                    randomCube.isKey = true;
                    randomCube.Meshrenderer = randomCube.GetComponent<MeshRenderer>();
                    randomCube.Meshrenderer.material = randomCube.answer;

                    availableCubes.RemoveAt(randomIndex);
                }
            }
        }

        void SelVertical(int val) // 수직 선택
        {
            if(cubeState.Equals(CubeState.Hor)) //수평이 이미 선택이 되어있다면
            {
                foreach (GameObject obj in HorizontalLineCube)
                {
                    /*Renderer renderer = obj.GetComponent<Renderer>();
                    renderer.material.color = Color.gray;*/
                    obj.transform.SetParent(cubeLoc.transform);
                }
                HorizontalLineCube.Clear();
            }
            cubeState = CubeState.Ver;
            RoundTransform();
            foreach (GameObject obj in LubiksCubeObj)
            {
                if (obj.transform.position.x.Equals(val))
                {
                    VerticalLineCube.Add(obj);
                    /*Renderer renderer = obj.GetComponent<Renderer>();
                    renderer.material.color = selVerColor;*/
                }
            }
        }

        void SelHorizontal(int val) //수평 선택
        {
            if (cubeState.Equals(CubeState.Ver)) //수직이 이미 선택이 되어있다면
            {
                foreach (GameObject obj in VerticalLineCube)
                {
                    /*Renderer renderer = obj.GetComponent<Renderer>();
                    renderer.material.color = Color.gray;*/
                    obj.transform.SetParent(cubeLoc.transform);
                }
                VerticalLineCube.Clear();
            }
            cubeState = CubeState.Hor;
            RoundTransform();
            foreach (GameObject obj in LubiksCubeObj)
            {
                if (obj.transform.position.z.Equals(val))
                {
                    HorizontalLineCube.Add(obj);
                    /*Renderer renderer = obj.GetComponent<Renderer>();
                    renderer.material.color = selHorColor;*/
                }
            }
        }

        IEnumerator RotateCubes(GameObject vh, Quaternion goalRot) //큐브 회전
        {
            Quaternion targetRotation = goalRot;

            Vector3 rotationAxis = cubeState switch
            {
                CubeState.Ver => Vector3.left,
                CubeState.Hor => Vector3.back,
            };

            float totalAngle = 90.0f;
            float rotatedAngle = 0.0f;

            while (rotatedAngle < totalAngle)
            {
                float step = rotationSpeed * Time.deltaTime;
                vh.transform.rotation *= Quaternion.AngleAxis(step, rotationAxis);
                rotatedAngle += step;
                yield return null;
            }
            RoundTransform();
        }

        void RoundTransform() //큐브 위치 재조정
        {
            foreach (GameObject obj in LubiksCubeObj)
            {
                Vector3 v3 = obj.transform.position;

                v3.x = Mathf.Round(v3.x);
                v3.y = Mathf.Round(v3.y);
                v3.z = Mathf.Round(v3.z);

                obj.transform.position = v3;
            }
            GameManager.gm.objController.boolCubeRotate = false;
        }


        #region Click
        public void Click_ChangeVerLine() //수직 라인 선택
        {
            if (v > 2)
            {
                v = 0;
            }
            if (VerticalLineCube.Count > 0)
            {
                foreach (GameObject obj in VerticalLineCube)
                {
                    /*Renderer renderer = obj.GetComponent<Renderer>();
                    renderer.material.color = Color.gray;*/
                    obj.transform.SetParent(cubeLoc.transform);
                }
            }
            VerticalLineCube.Clear();
            //vText.text = "V : " + v;
            SelVertical(v);
            v++;
        }

        public void Click_ChangeHorLine() //수평 라인 선택
        {
            if (h > 2)
            {
                h = 0;
            }
            if (HorizontalLineCube.Count > 0)
            {
                foreach (GameObject obj in HorizontalLineCube)
                {
                   /* Renderer renderer = obj.GetComponent<Renderer>();
                    renderer.material.color = Color.gray;*/
                    obj.transform.SetParent(cubeLoc.transform);
                }
            }
            HorizontalLineCube.Clear();
           // hText.text = "H : " + h;
            SelHorizontal(h);
            h++;
        }

        public void Click_RotateVertical() //수직 회전
        {
            if (!cubeState.Equals(CubeState.Ver)) return;
            foreach (GameObject obj in VerticalLineCube)
            {
                obj.transform.SetParent(ver.transform);
            }
            StartCoroutine(RotateCubes(ver, Quaternion.Euler(90, 0, 0)));
        }

        public void Click_RotateHorizontal() //수평 회전
        {
            if (!cubeState.Equals(CubeState.Hor)) return;
            foreach (GameObject obj in HorizontalLineCube)
            {
                obj.transform.SetParent(hor.transform);
            }
            StartCoroutine(RotateCubes(hor, Quaternion.Euler(0, 0, 90)));
        }
        public void Click_AnswerCheck()
        {
            int a = 0;
            foreach (Cube item in LubiksCubeScript)
            {
                if (item.isCurPos)
                {
                    a++;
                }
            }
            if (a >= 9) 
            {
                Debug.Log("Clear");
            }
        }
        #endregion

        private enum CubeState
        {
            None,
            Hor,
            Ver
        }
    }
}