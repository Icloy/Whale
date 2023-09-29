using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class Cube : MonoBehaviour
    {
        //[HideInInspector] public int cubeKey;
        public int cubeKey;

        [Header("Cube_Material")]
        [SerializeField] Material normal;
        public Material answer;
        public MeshRenderer Meshrenderer;
    }
}