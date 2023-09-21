using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private float range;

    private bool pickupActivated = false;

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask;


    public GameObject item = null;  // 바라보고 있는 아이템
    public GameObject obj = null;   // 바라보고 있는 오브젝트 ex) 문,서랍
    public GameObject heldItem = null;  // 들고 있는 아이템
    public Transform hand;
    private KeyCode pickupKey = KeyCode.E;
    private KeyCode dropKey = KeyCode.Q;

    // Update is called once per frame
    void Update()
    {
        CheckItem();
        TryAction();
    }





    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }

    }

    void CanPickUp()
    {
        if (pickupActivated) // true일 때만 작동
        {
            if (hitInfo.transform != null)
            {
                if (Input.GetKeyDown(pickupKey) && heldItem == null)
                {
                    Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
                    foreach (Collider collider in colliders)
                    {

                        Destroy(hitInfo.transform.gameObject); //삭제 후 프리펩을 가져온다 CheckItem에서 확인 가능
                        Debug.Log("아이템 픽업");
                        heldItem = Instantiate(item, hand);
                        heldItem.transform.localPosition = Vector3.zero;
                        heldItem.transform.localRotation = Quaternion.identity;
                        //heldItem.transform.localScale = new Vector3(1f, 1f, 1f);
                        heldItem.GetComponent<Rigidbody>().isKinematic = true;
                        break;

                    }
                }
                InfoDisappear(); //pickupActivated 초기화
            }
        }
    }

    void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask)) //시야에 닿은 것 파악
        {
            //기즈모 써라
            Debug.Log("2");

            if (hitInfo.transform.tag == "Item" && heldItem == null)
            {
                Debug.Log("!");
                ItemInfoAppear();
            }
            else
            {
                item = null;
                obj = null;
            }

            if (hitInfo.transform.tag == "Object")
            {
                ObjectInfoApper();
            }

        }
        else
        {
            InfoDisappear(); //글씨 없음
        }
    }

    void ItemInfoAppear()
    {
        obj = null;
        pickupActivated = true; //false 에서 true로 변경
        //item = hitInfo.transform.GetComponent<ItemPickUp>().item.itemPrefab; // 요부분 중요 ** itempickup스크립트에 저장해둔 프리펩을 고대로 가져오는 구문

    }

    void ObjectInfoApper()
    {
        item = null;
        pickupActivated = false;
        //obj = hitInfo.transform.GetComponent<ItemPickUp>().item.itemPrefab; // 요부분 중요 ** itempickup스크립트에 저장해둔 프리펩을 고대로 가져오는 구문

        if (heldItem != null)
        {
            if (Input.GetKeyDown(dropKey))
            {
                Debug.Log("아이템 내려놓기");

                heldItem.GetComponent<Rigidbody>().isKinematic = false;
                heldItem.transform.parent = null;
                heldItem = null;
            }

            pickupActivated = false;
        }
    }

    void InfoDisappear()
    {
        item = null;
        obj = null;
        pickupActivated = false;
        if (heldItem != null)
        {
            if (Input.GetKeyDown(dropKey))
            {
                Debug.Log("아이템 내려놓기");

                heldItem.GetComponent<Rigidbody>().isKinematic = false;
                heldItem.transform.parent = null;
                heldItem = null;
            }

            pickupActivated = false;
        }

    }


}
