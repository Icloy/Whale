using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public Transform pos;
    [SerializeField] private float moveSpeed;
    private Vector3 initialPosition;  // �ʱ� ��ġ


    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pos.position, moveSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.transform.position = initialPosition;
    }
}
