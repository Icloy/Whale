using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class MazeCamera : MonoBehaviour
    {
        [SerializeField] private Camera cam;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                cam.cullingMask = ~(1 << 10);
            }
        }
    }
}

