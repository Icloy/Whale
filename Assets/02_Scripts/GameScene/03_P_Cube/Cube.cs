using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class Cube : MonoBehaviour
    {
        [Header("CubeKey")]
        [HideInInspector] public int cubeKey;
        [HideInInspector] public bool isSel;
        [HideInInspector] public bool isKey;

        [Header("Cube_Material")]
        [SerializeField] Material normal;
        public Material answer;
        public MeshRenderer Meshrenderer;
    }
}