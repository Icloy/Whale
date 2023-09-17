using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Cube : MonoBehaviour
{
    [Header("All")]
    public GameObject allP_Cube;

    [Header("Cube")]
    [SerializeField] GameObject cube;
    int cubeNum = 3;
    List<GameObject> LubiksCube = new List<GameObject>();
    List<GameObject> HorinzontalLineCube = new List<GameObject>();
    List<GameObject> VerticalLineCube = new List<GameObject>();

    [Header("Caching")]
    GameObject obj;
    void InitCubeProcess()
    {
        for (int i = 0; i < cubeNum; i++)
        {
            for (int x = 0; x < cubeNum; x++)
            {
                for (int y = 0; y < cubeNum; y++)
                {
                    obj = Instantiate(cube);
                    obj.transform.position = new Vector3(x, y, i);
                    obj.SetActive(true);
                    obj.transform.SetParent(cube.transform.parent, false);
                    LubiksCube.Add(obj);
                }
            }
        }
    }

    private void Start()
    {
        InitCubeProcess();
    }
}
