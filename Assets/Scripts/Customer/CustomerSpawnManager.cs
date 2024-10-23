using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnManager : MonoBehaviourSingleton<CustomerSpawnManager>
{
    [SerializeField] Customer customerPrefab;
    [SerializeField] Transform spawnPointTransform;
    [SerializeField] float initialSpawnGap = 1f;
    [SerializeField] CustomerQueuePoint[] customerQueuePoints;

    Queue<Customer> customers = new();

    void Start()
    {
        StartCoroutine(SpawnCustomers());
    }

    IEnumerator SpawnCustomers()
    {
        foreach (var point in customerQueuePoints)
        {
            SpawnCustomer(point);
            yield return new WaitForSeconds(initialSpawnGap);
        }
    }

    Customer SpawnCustomer(CustomerQueuePoint point)
    {
        Customer customer = Instantiate(customerPrefab, spawnPointTransform.position, spawnPointTransform.rotation);
        customer.SetTargetDestination(point.GetQueueTransform(), DestinationType.Queue);
        point.SetCustomer(customer);
        return customer;
    }

    public void AddCustomerToQueue(Customer customer)
    {
        customers.Enqueue(customer);
    }

    public Customer GetCustomerFromQueue()
    {
        if (!HasCustomersInQueue()) return null;
        Customer customer = customers.Dequeue();
        DequeueCustomerQueue();
        return customer;
    }

    public bool HasCustomersInQueue() => customers.Count > 0;
    void DequeueCustomerQueue()
    {
        for (int i = 0; i < customerQueuePoints.Length - 1; i++)
        {
            var point = customerQueuePoints[i];
            point.SetCustomer(null);

            var nextPoint = customerQueuePoints[i + 1];
            var customer = nextPoint.GetCustomer();
            if (customer == null) continue;

            point.SetCustomer(customer);
            customer.SetTargetDestination(point.GetQueueTransform(), DestinationType.SortQueue);
            nextPoint.SetCustomer(null);
        }
        var lastPoint = customerQueuePoints[^1];

        SpawnCustomer(lastPoint);
    }
    public Transform GetSpawnPointTransform() => spawnPointTransform;
}
