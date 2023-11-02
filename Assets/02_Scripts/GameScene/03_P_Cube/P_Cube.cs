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

        [SerializeField] List<GameObject> LubiksCubeObj = new List<GameObject>();
        [SerializeField] List<Cube> LubiksCubeScript = new List<Cube>();
        [SerializeField] List<GameObject> HorizontalLineCube = new List<GameObject>();
        [SerializeField] List<GameObject> VerticalLineCube = new List<GameObject>();

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

        [Header("TestUI")]
        [SerializeField] TextMeshProUGUI vText;
        [SerializeField] TextMeshProUGUI hText;

        private void Start()
        {
            InitCubeProcess();

            //���ý� ����
            selVerColor = Color.yellow;
            selHorColor = Color.green;
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
            PickRandomCube();
            cubeState = CubeState.None;
        }

        public void PickRandomCube() 
        {
            List<Cube> availableCubes = new List<Cube>(LubiksCubeScript);
            availableCubes.RemoveAll(cube => cube.cubeKey == 14);
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

        void SelVertical(int val)
        {
            cubeState = CubeState.Ver;
            foreach (GameObject obj in LubiksCubeObj)
            {
                if (Mathf.Round(obj.transform.position.x) == val)
                {
                    VerticalLineCube.Add(obj);
                }
            }
        }

        void SelHorizontal(int val) 
        {
            cubeState = CubeState.Hor;
            foreach (GameObject obj in LubiksCubeObj)
            {
                if (Mathf.Round(obj.transform.position.z) == val)
                {
                    HorizontalLineCube.Add(obj);
                }
            }
        }

        IEnumerator RotateCubes(GameObject vh, Quaternion goalRot) //ť�� ȸ��
        {
            Quaternion targetRotation = goalRot;
            /*
            Vector3 rotationAxis = cubeState switch
            {
                CubeState.Ver => Vector3.left,
                CubeState.Hor => Vector3.back,
            };

            float totalAngle = 90.0f;
            float rotatedAngle = 0.0f;*/
            float t = 0;
            Vector3 vec1 = vh.transform.localEulerAngles;
            Vector3 vec2 = new Vector3(vec1.x + 90, 0, 0);
            Vector3 vec3 = new Vector3(0, 0, vec1.z+90);

            //while (rotatedAngle <= totalAngle)
            //{
            //    float step = rotationSpeed * Time.deltaTime;
            //    vh.transform.rotation *= Quaternion.AngleAxis(step, rotationAxis);
            //    rotatedAngle += step;

            //    yield return null;
            //}
            Vector3 originalScale = vh.transform.parent.localScale;
            while (t<=1)
            {
                t += Time.deltaTime * 5f;
                if(cubeState == CubeState.Ver)
                {
                    vh.transform.localEulerAngles = Vector3.Lerp(vec1, vec2, t);
                }
                else
                {
                    vh.transform.localEulerAngles = Vector3.Lerp(vec1, vec3, t);
                }
                yield return null;
            }
            vh.transform.parent.localScale = originalScale;
            SetCube();
            ListClear();

            vh.transform.localEulerAngles = Vector3.zero;
            //GameManager.gm.objController.boolCubeRotate = false;
        }

        private void SetCube()
        {
            foreach (GameObject obj in LubiksCubeObj)
            {
                Vector3 v3 = obj.transform.position;

                v3.x = Mathf.Round(v3.x);
                v3.y = Mathf.Round(v3.y);
                v3.z = Mathf.Round(v3.z);
                obj.transform.position = v3;
            }
        }

        void ListClear()
        {
            if(VerticalLineCube.Count > 0)
            {
                foreach (GameObject obj in VerticalLineCube)
                {
                    obj.transform.SetParent(cubeLoc.transform);
                }
                VerticalLineCube.Clear();

            }
            if (HorizontalLineCube.Count > 0)
            {
                foreach (GameObject obj in HorizontalLineCube)
                {
                    obj.transform.SetParent(cubeLoc.transform);
                }
                HorizontalLineCube.Clear();
            }
        }

        #region Click
        public void Click_ChangeVerLine() 
        {
            if (v > 2)
            {
                v = 0;
            }
            if (VerticalLineCube.Count > 0)
            {
                foreach (GameObject obj in VerticalLineCube)
                {
                    obj.transform.SetParent(cubeLoc.transform);
                }
                VerticalLineCube.Clear();
            }
            vText.text = "V : " + v;
            SetCube();
            SelVertical(v);
            v++;
        }

        public void Click_ChangeHorLine() 
        {
            if (h > 2)
            {
                h = 0;
            }
            if (HorizontalLineCube.Count > 0)
            {
                foreach (GameObject obj in HorizontalLineCube)
                {
                    obj.transform.SetParent(cubeLoc.transform);
                }
                HorizontalLineCube.Clear();
            }
            hText.text = "H : " + h;
            SetCube();
            SelHorizontal(h);
            h++;
        }

        public void Click_RotateVertical()
        {
            if (!cubeState.Equals(CubeState.Ver)) return;
            foreach (GameObject obj in VerticalLineCube)
            {
                obj.transform.SetParent(ver.transform);
            }
            StartCoroutine(RotateCubes(ver, Quaternion.Euler(90, 0, 0)));
        }

        public void Click_RotateHorizontal() 
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