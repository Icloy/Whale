using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using whale;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private float range;

    private bool pickupActivated = false;

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask;


    public GameObject item = null;  // 바라보고 있는 아이템
    public GameObject heldItem = null;  // 들고 있는 아이템
    public GameObject obj = null;
    public Transform hand;
    private KeyCode pickupKey = KeyCode.E;
    private KeyCode dropKey = KeyCode.Q;

    private bool isObj;

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
            CheckObj();
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

    void OnDrawGizmos()
    {
        Vector3 rayStart = transform.position;
        Vector3 rayDirection = transform.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(rayStart, rayDirection * range);
    }

    void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask)) //시야에 닿은 것 파악
        {
            if (hitInfo.transform.tag == "Item" && heldItem == null)
            {
                ItemInfoAppear();
            }
            else
            {
                item = null;
                obj = null;
            }

        }
        else
        {
            InfoDisappear(); //글씨 없음
        }
    }

    void CheckObj()
    {
        if (hitInfo.transform != null)
        {
            if (hitInfo.transform.tag == "Object")
            {
                ObjectInfoApper();
            }
        }
    }

    void ItemInfoAppear()
    {
        pickupActivated = true; //false 에서 true로 변경
        item = hitInfo.transform.GetComponent<Item>().item.itemPrefab; // 요부분 중요 ** itempickup스크립트에 저장해둔 프리펩을 고대로 가져오는 구문

    }

    void ObjectInfoApper()
    {
        item = null;
        pickupActivated = false;
        obj = hitInfo.transform.GetComponent<Item>().item.itemPrefab; // 요부분 중요 ** itempickup스크립트에 저장해둔 프리펩을 고대로 가져오는 구문
        if (obj != null && Input.GetKeyDown(pickupKey))
        {
            switch (obj.name)
            {
                #region Cube
                case "Cube_HorizontalH":
                    MainManager.Instance.titleManager.ObjectInteraction(MainManager.Instance.statusContainer.userNum, "Cube", 0);
                    break;
                case "Cube_VerticalH":
                    MainManager.Instance.titleManager.ObjectInteraction(MainManager.Instance.statusContainer.userNum, "Cube", 1);
                    break;
                case "Cube_Answer":
                    MainManager.Instance.titleManager.ObjectInteraction(MainManager.Instance.statusContainer.userNum, "Cube", 2);
                    break;
                case "Cube_HorizontalC":
                    MainManager.Instance.titleManager.ObjectInteraction(MainManager.Instance.statusContainer.userNum, "Cube", 3);
                    break;
                case "Cube_VerticalC":
                    MainManager.Instance.titleManager.ObjectInteraction(MainManager.Instance.statusContainer.userNum, "Cube", 4);
                    break;
                case "Cube_Random":
                    MainManager.Instance.titleManager.ObjectInteraction(MainManager.Instance.statusContainer.userNum, "Cube", 4);
                    break;
                #endregion
                #region Maze
                case "BlueBtn":
                    MainManager.Instance.titleManager.ObjectInteraction(MainManager.Instance.statusContainer.userNum, "Maze", 0);
                    break;
                case "GreenBtn":
                    MainManager.Instance.titleManager.ObjectInteraction(MainManager.Instance.statusContainer.userNum, "Maze", 1);
                    break;
                case "RedBtn":
                    MainManager.Instance.titleManager.ObjectInteraction(MainManager.Instance.statusContainer.userNum, "Maze", 2);
                    break;
                    #endregion
                #region Room

                #endregion
            }
        }
    }

    void InfoDisappear()
    {
        item = null;
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
