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
       public void ItemOk(Vector3 position)
        {
            GameObject effectInstance = Instantiate(effect, position, Quaternion.identity);
            Destroy(effectInstance, 1.0f);
            if(itemBool[0] && itemBool[1] && itemBool[2])
            {
                Debug.Log("Ŭ����");
            }
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
    }
}

