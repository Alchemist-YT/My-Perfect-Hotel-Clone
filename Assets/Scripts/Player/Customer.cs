using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Customer : MonoBehaviour
{
    [SerializeField] float customerStayTime = 5;
    [ReadOnly, SerializeField] CustomerState customerState;
    [ReadOnly, SerializeField] DestinationType destinationType;

    [SerializeField] Money moneyPrefab;

    NavMeshAgent navMeshAgent;
    Animator animator;
    Transform target;

    HotelRoom occupiedRoom;

    float stayTime;
    bool hasStayStarted;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        OnReachingDestination();
        HandleStay();
    }


    public void SetTargetDestination(Transform _target, DestinationType _destinationType)
    {
        target = _target;
        destinationType = _destinationType;
        navMeshAgent.SetDestination(_target.position);
        Walk();
    }
    public void OnRoomAsigned(HotelRoom _room)
    {
        occupiedRoom = _room;

        MoneyStackPoint point = MoneyStacker.Instance.GetEmptyMoneyStackPoint();
        Money money = Instantiate(moneyPrefab, point.transform.position, point.transform.rotation);
        money.SetMoneyValue(occupiedRoom.GetRoomRent());
        point.SetMoney(money);
    }

    void Walk()
    {
        animator.SetBool("walk", true);
        customerState = CustomerState.Walking;
    }
    void Stop()
    {
        animator.SetBool("walk", false);
        customerState = CustomerState.Stoped;
    }

    void OnReachingDestination()
    {
        if (customerState == CustomerState.Walking && Vector3.Distance(transform.position, target.position) <= navMeshAgent.stoppingDistance)
        {
            Stop();
            transform.forward = target.forward;

            switch (destinationType)
            {
                case DestinationType.None:
                    break;
                case DestinationType.Queue:
                    OnReachingQueue();
                    break;
                case DestinationType.SortQueue:
                    OnSortingQueue();
                    break;
                case DestinationType.HotelRoom:
                    OnReachingRoom();
                    break;
                case DestinationType.OutHotel:
                    OnReacherOutside();
                    break;
                default:
                    break;
            }
        }
    }



    void HandleStay()
    {
        if (hasStayStarted)
        {
            stayTime += Time.deltaTime;

            if (stayTime >= customerStayTime)
            {
                stayTime = 0;
                VacateRoom();
            }
        }
    }

    void OnReacherOutside()
    {

    }
    void OnSortingQueue()
    {

    }

    void OnReachingRoom()
    {
        hasStayStarted = true;

    }

    void OnReachingQueue()
    {
        CustomerSpawnManager.Instance.AddCustomerToQueue(this);
    }


    void VacateRoom()
    {
        hasStayStarted = false;
        occupiedRoom.AfterRoomUsed();
        occupiedRoom = null;
        SetTargetDestination(CustomerSpawnManager.Instance.GetSpawnPointTransform(), DestinationType.OutHotel);
    }
}
public enum CustomerState
{
    None,
    Walking,
    Stoped
}

public enum DestinationType
{
    None,
    Queue,
    SortQueue,
    HotelRoom,
    OutHotel
}