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
        private bool isP1 = false;
        public void ItemOk(Vector3 position)
        {
            GameManager.gm.soundManager.Play(SoundManager.AudioType.Ghost, true);
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
            else if (num == 3)
            {
                itemList[6].SetActive(false);
                itemList[7].SetActive(false);
            }
        }

        IEnumerator ItemEnd()
        {
            yield return new WaitForSeconds(1f);
            if (itemBool[0] && itemBool[1] && itemBool[2] && itemBool[3] && !isP1)
            {
                Debug.Log("Ŭ����");
                isP1 = true;
                DoorL.SetActive(false);
                DoorR.SetActive(false);
                GameManager.gm.soundManager.Play(SoundManager.AudioType.MetalDoor, true);
            }
        }

    }
}

