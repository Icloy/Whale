using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class Cube : MonoBehaviour
    {
        [Header("CubeKey")]
         public int cubeKey;
         public bool isCurPos;
         public bool isKey;

        [Header("Cube_Material")]
        [SerializeField] Material normal;
        public Material answer;
        public MeshRenderer Meshrenderer;

        #region Trigger
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("AnswerCheck"))
            {
                if (isKey)
                {
                    isCurPos = true;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("AnswerCheck"))
            {
                isCurPos = false;
            }
        }
        #endregion
    }
}