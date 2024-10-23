using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerHandler : MonoBehaviour
{
    [SerializeField] float customerHandleTime = 3;
    [SerializeField] Image cleanFillImage;
    [SerializeField] GameObject cleanParent;    

    float time;
    public void HandleCustomer()
    {
        if (!(RoomManager.Instance.IsAllRoomNonUseable() || !CustomerSpawnManager.Instance.HasCustomersInQueue()))
        {
            cleanParent.SetActive(true);

            time += Time.deltaTime;
            cleanFillImage.fillAmount = time / customerHandleTime;
            if (time > customerHandleTime)
            {
                time = 0;
                AssignCustomerToRoom();
            }
        }
        else
        {
            cleanParent.SetActive(false);
        }
    }
    void AssignCustomerToRoom()
    {
        Customer customer = CustomerSpawnManager.Instance.GetCustomerFromQueue();
        HotelRoom room = RoomManager.Instance.GetUseableRoom();
        customer.SetTargetDestination(room.transform, DestinationType.HotelRoom);
        customer.OnRoomAsigned(room);
        room.OnAssingingCustomer(customer);
    }
}
