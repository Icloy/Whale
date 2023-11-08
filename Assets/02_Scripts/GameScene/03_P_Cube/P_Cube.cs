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
        [SerializeField] List<RectTransform> LubiksCubeRect = new List<RectTransform>();

        [SerializeField] List<GameObject> HorizontalLineCube = new List<GameObject>();
        [SerializeField] List<GameObject> VerticalLineCube = new List<GameObject>();

        [SerializeField] GameObject cubeLoc;
        [SerializeField] GameObject ver;
        [SerializeField] GameObject hor;

        [Header("Caching")]
        CubeState cubeState;
        GameObject obj;
        int v, h;

        [Header("TestUI")]
        [SerializeField] TextMesh lText;
        [SerializeField] TextMesh state;
        [SerializeField] TextMesh answer;

        private Animator anim;

        private void Start()
        {
            cubeState = CubeState.None;

            //게임씬에서는 실행 x (랜덤 요소)
            //InitCubeProcess();
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
                        RectTransform rt = obj.GetComponent<RectTransform>();
                        cubeScript.cubeKey = cubeKey;
                        cubeKey++;
                        obj.transform.position = new Vector3(x, y, i);
                        obj.SetActive(true);
                        obj.transform.SetParent(cube.transform.parent, false);
                        LubiksCubeRect.Add(rt);
                        LubiksCubeObj.Add(obj);
                        LubiksCubeScript.Add(cubeScript);
                    }
                }
            }
            PickRandomCube();
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
            ListClear();
            foreach (RectTransform rt in LubiksCubeRect)
            {
                if (Mathf.Round(rt.anchoredPosition3D.x) == val)
                {
                    VerticalLineCube.Add(rt.gameObject);
                }
            }
        }

        void SelHorizontal(int val) 
        {
            ListClear();
            foreach (RectTransform rt in LubiksCubeRect)
            {
                if (Mathf.Round(rt.anchoredPosition3D.z) == val)
                {
                    HorizontalLineCube.Add(rt.gameObject);
                }
            }
        }

        IEnumerator RotateCubes(GameObject vh)
        {
            GameManager.gm.soundManager.Play(SoundManager.AudioType.Cube, true);
            answer.text = "Rotating";
            float t = 0;
            Vector3 vec1 = vh.transform.localEulerAngles;
            Vector3 vec2 = new Vector3(vec1.x + 90, 0, 0);
            Vector3 vec3 = new Vector3(0, 0, vec1.z+90);

            while (t<=1)
            {
                t += Time.deltaTime * 3f;
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
            ParentClear();
            SetCube();

            vh.transform.localEulerAngles = Vector3.zero;
            answer.text = "";
            GameManager.gm.objController.boolCubeRotate = false;
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
        private void ListClear()
        {
            if (VerticalLineCube.Count > 0)
            {
                VerticalLineCube.Clear();
            }
            if (HorizontalLineCube.Count > 0)
            {
                HorizontalLineCube.Clear();
            }
        }

        void ParentClear()
        {
            if(VerticalLineCube.Count > 0)
            {
                foreach (GameObject obj in VerticalLineCube)
                {
                    obj.transform.SetParent(cubeLoc.transform);
                }
            }
            if (HorizontalLineCube.Count > 0)
            {
                foreach (GameObject obj in HorizontalLineCube)
                {
                    obj.transform.SetParent(cubeLoc.transform);
                }
            }
        }

        #region Click
        public void BtnClick_ChooseState()
        {
            if(cubeState == CubeState.None) cubeState = CubeState.Ver;
            switch (cubeState)
            {
                case CubeState.Hor:
                    cubeState = CubeState.CA;
                    state.text = "Answer";
                    break;
                case CubeState.CA:
                    cubeState = CubeState.Ver;
                    state.text = "Vertical";
                    break;
                case CubeState.Ver:
                    cubeState = CubeState.Hor;
                    state.text = "Horizontal";
                    break;
            }
            lText.text = "";
            answer.text = "";
        }

        public void BtnClick_LineChoose()
        {
            switch (cubeState)
            {
                case CubeState.Hor:
                    if (h > 2)
                    {
                        h = 0;
                    }
                    lText.text = (h+1) + " 번째 줄";
                    SetCube();
                    SelHorizontal(h);
                    h++;
                    break;
                case CubeState.CA:
                    //None
                    break;
                case CubeState.Ver:
                    if (v > 2)
                    {
                        v = 0;
                    }
                    lText.text = (v+1) + " 번째 줄";
                    SetCube();
                    SelVertical(v);
                    v++;
                    break;
            }
        }

        public void BtnClick_Rotate_CheckAnswer()
        {
            switch (cubeState)
            {
                case CubeState.Hor:
                    foreach (GameObject obj in HorizontalLineCube)
                    {
                        obj.transform.SetParent(hor.transform);
                    }
                    StartCoroutine(RotateCubes(hor));
                    break;
                case CubeState.CA:
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
                        //큐브 정답 처리
                        Debug.Log("Clear");
                        answer.text = "Correct";
                        GameManager.gm.EndEffect.SetActive(true);
                        anim = GameManager.gm.EndDoor.GetComponent<Animator>();
                        anim.SetBool("Clear", true);
                        Invoke("HideEffect", 3f);
                    }
                    else
                    {
                        answer.text = "Wrong";
                    }
                    GameManager.gm.objController.boolCubeRotate = false;
                    break;
                case CubeState.Ver:
                    foreach (GameObject obj in VerticalLineCube)
                    {
                        obj.transform.SetParent(ver.transform);
                    }
                    StartCoroutine(RotateCubes(ver));
                    break;
            }
        }
        #endregion

        private enum CubeState
        {
            None,
            Hor,
            Ver,
            CA
        }

        void HideEffect()
        {
            GameManager.gm.EndEffect.SetActive(false);
        }
    }
}