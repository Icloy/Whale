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
        [SerializeField] GameObject cubeLoc;
        [SerializeField] GameObject ver;
        [SerializeField] GameObject hor;


        [Header("Caching")]
        Coroutine rotateCoroutine = null;
        Color selColor;
        GameObject obj;
        bool isRotate;
        int v, h;
        [SerializeField] TextMeshProUGUI vText;
        [SerializeField] TextMeshProUGUI hText;
        float rotationSpeed = 90f;
        int rotationCount;


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
        IEnumerator RotateCubes(GameObject vh, Quaternion goalRot)
        {
            Quaternion startRotation = vh.transform.rotation;
            Quaternion targetRotation = goalRot;
            Vector3 rotationAxis = Vector3.right;

            float totalAngle = 90.0f;
            float rotatedAngle = 0.0f;

            while (rotatedAngle < totalAngle)
            {
                float step = rotationSpeed * Time.deltaTime;
                vh.transform.rotation *= Quaternion.AngleAxis(step, rotationAxis);
                rotatedAngle += step;
                yield return null;
            }

            vh.transform.rotation = targetRotation;
            rotationCount++;

            if (rotationCount >= 4)
            {
                rotationCount = 0;
            }
        }

        void ResetCubeRotations()
        {
            foreach (var obj in LubiksCubeObj)
            {
                obj.transform.rotation = Quaternion.identity;
            }
        }

        #region Click
        public void Click_RotateVertical()
        {
            foreach (GameObject obj in VerticalLineCube)
            {
                obj.transform.SetParent(ver.transform);
            }
            StartCoroutine(RotateCubes(ver, Quaternion.Euler(90, 0, 0)));
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
            if(VerticalLineCube.Count > 0)
            {
                foreach (GameObject obj in VerticalLineCube)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    renderer.material.color = Color.gray;
                    obj.transform.SetParent(cubeLoc.transform);
                }
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