using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whale
{
    public class CheckItem : MonoBehaviour
    {
        public GameObject effect;
        public List<GameObject> itemList = new List<GameObject>();
        public List<bool> itemBool = new List<bool>();

        public GameObject DoorL;
        public GameObject DoorR;
        public void ItemOk(Vector3 position)
        {
            Vector3 rotation = new Vector3(-90, 0, 0);
            Quaternion desiredRotation = Quaternion.Euler(rotation);
            GameObject effectInstance = Instantiate(effect, position, desiredRotation);
            Destroy(effectInstance, 5.0f);
            StartCoroutine(ItemEnd());
        }

        public void ItemDel(int num)
        {
            if(num == 0)
            {
                itemList[0].SetActive(false);
                itemList[1].SetActive(false);
            }
            else if(num == 1)
            {
                itemList[2].SetActive(false);
                itemList[3].SetActive(false);
            }
            else if (num == 2)
            {
                itemList[4].SetActive(false);
                itemList[5].SetActive(false);
            }
        }

        IEnumerator ItemEnd()
        {
            yield return new WaitForSeconds(1f);
            if (itemBool[0] && itemBool[1] && itemBool[2])
            {
                Debug.Log("Å¬¸®¾î");
                DoorL.SetActive(false);
                DoorR.SetActive(false);
            }
        }
    }
}

