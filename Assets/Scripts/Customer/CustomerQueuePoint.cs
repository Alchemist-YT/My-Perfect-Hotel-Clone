using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueuePoint : MonoBehaviour
{
    [SerializeField] Customer currentCustomer;

    public bool IsOccupied() => currentCustomer != null;
    public Vector3 GetQueuePos() => transform.position;
    public Transform GetQueueTransform() => transform;
    public Customer GetCustomer() => currentCustomer;
    public void SetCustomer(Customer _customer) => currentCustomer = _customer;
}
