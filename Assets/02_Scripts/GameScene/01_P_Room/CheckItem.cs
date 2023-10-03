using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class CheckItem : MonoBehaviour
    {
        [SerializeField] private GameObject Cube;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Item"))
            {
                switch (collision.gameObject.name)
                {
                    case "Cube(Clone)":
                        Destroy(collision.gameObject);
                        Cube.SetActive(true);
                        break;
                }
            }
        }
    }
}

