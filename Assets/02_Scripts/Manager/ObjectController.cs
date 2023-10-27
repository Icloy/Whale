using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class ObjectController : MonoBehaviour
    {
        [SerializeField] GameObject testCube;






        public void TestCubeColor()
        {
            MeshRenderer mesh = testCube.GetComponent<MeshRenderer>();
            mesh.material.color = Color.red;
        }
    }
}