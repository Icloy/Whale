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


    public GameObject item = null;  // �ٶ󺸰� �ִ� ������
    public GameObject obj = null;   // �ٶ󺸰� �ִ� ������Ʈ ex) ��,����
    public GameObject heldItem = null;  // ��� �ִ� ������
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
        if (pickupActivated) // true�� ���� �۵�
        {
            if (hitInfo.transform != null)
            {
                if (Input.GetKeyDown(pickupKey) && heldItem == null)
                {
                    Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
                    foreach (Collider collider in colliders)
                    {

                        Destroy(hitInfo.transform.gameObject); //���� �� �������� �����´� CheckItem���� Ȯ�� ����
                        Debug.Log("������ �Ⱦ�");
                        heldItem = Instantiate(item, hand);
                        heldItem.transform.localPosition = Vector3.zero;
                        heldItem.transform.localRotation = Quaternion.identity;
                        //heldItem.transform.localScale = new Vector3(1f, 1f, 1f);
                        heldItem.GetComponent<Rigidbody>().isKinematic = true;
                        break;

                    }
                }
                InfoDisappear(); //pickupActivated �ʱ�ȭ
            }
        }
    }

    void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask)) //�þ߿� ���� �� �ľ�
        {
            //����� ���
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
            InfoDisappear(); //�۾� ����
        }
    }

    void ItemInfoAppear()
    {
        obj = null;
        pickupActivated = true; //false ���� true�� ����
        //item = hitInfo.transform.GetComponent<ItemPickUp>().item.itemPrefab; // ��κ� �߿� ** itempickup��ũ��Ʈ�� �����ص� �������� ���� �������� ����

    }

    void ObjectInfoApper()
    {
        item = null;
        pickupActivated = false;
        //obj = hitInfo.transform.GetComponent<ItemPickUp>().item.itemPrefab; // ��κ� �߿� ** itempickup��ũ��Ʈ�� �����ص� �������� ���� �������� ����

        if (heldItem != null)
        {
            if (Input.GetKeyDown(dropKey))
            {
                Debug.Log("������ ��������");

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
                Debug.Log("������ ��������");

                heldItem.GetComponent<Rigidbody>().isKinematic = false;
                heldItem.transform.parent = null;
                heldItem = null;
            }

            pickupActivated = false;
        }

    }


}
