using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class CheckItem : MonoBehaviour
    {
        [SerializeField] private GameObject Cube;

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

