using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class ObjectController : MonoBehaviour
    {
        [SerializeField] GameObject testCube;


        [SerializeField] Material redMeterial;



        public void TestCubeColor()
        {
            MeshRenderer renderer = testCube.GetComponent<MeshRenderer>();
            renderer.material = redMeterial;
        }
    }
}