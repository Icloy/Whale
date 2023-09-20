using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        List<GameObject> HorinzontalLineCube = new List<GameObject>();
        List<GameObject> VerticalLineCube = new List<GameObject>();


        [Header("Caching")]
        GameObject obj;

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
                        Cube cubeScript = GetComponent<Cube>();
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

        private void Start()
        {
            InitCubeProcess();
        }

        #region Click
        public void Click_RotateVertical()
        {
            foreach (var item in VerticalLineCube)
            {

            }
        }

        public void Click_RotateHorizontal()
        {
            foreach (var item in HorinzontalLineCube)
            {

            }

        }
        #endregion
    }

}