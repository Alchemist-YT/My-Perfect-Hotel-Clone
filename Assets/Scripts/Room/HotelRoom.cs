using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HotelRoom : MonoBehaviour
{
    public event Action<HotelRoom> OnRoomCleaned_Event;
    [field: SerializeField] public bool IsUnLocked { get; private set; }

    [ReadOnly, SerializeField] RoomState roomState;
    [ReadOnly, SerializeField] int moneyNeeded;

    [SerializeField] RoomData roomData;
    [SerializeField] RoomItem[] roomItems;

    [Header("Spend Money Trigger")]
    [SerializeField] TMP_Text moneyNeededText;
    [SerializeField] GameObject spendCashTrigger;

    float time;

    void OnEnable()
    {
        foreach (RoomItem roomItem in roomItems)
        {
            roomItem.OnStateChange += RoomItem_OnStateChange;
        }
    }
    public int GetRoomRent() => roomData.RoomRent;

    void Start()
    {
        spendCashTrigger.SetActive(!IsUnLocked);
        if (!IsUnLocked)
        {
            moneyNeeded = roomData.RoomUnlockCost;
            moneyNeededText.text = $"{moneyNeeded}";
        }
    }

    public bool IsUsable() => roomState == RoomState.Cleanded && IsUnLocked;
    public bool HasOccupied() => roomState == RoomState.Occupied;
    public bool IsDirty() => roomState == RoomState.Dirty;

    void RoomItem_OnStateChange()
    {
        bool isCleaned = roomItems.All((item) => item.IsUsable());
        if (isCleaned)
        {
            OnRoomCleaned();
        }
    }

    public void AfterRoomUsed()
    {
        foreach (RoomItem roomItem in roomItems)
        {
            roomItem.MakeDirty();
        }
        roomState = RoomState.Dirty;
    }

    public void OnRoomCleaned()
    {
        roomState = RoomState.Cleanded;
        OnRoomCleaned_Event?.Invoke(this);
    }
    public void OnAssingingCustomer(Customer customer)
    {
        roomState = RoomState.Occupied;
    }
    void OnDisable()
    {
        foreach (RoomItem roomItem in roomItems)
        {
            roomItem.OnStateChange -= RoomItem_OnStateChange;
        }
    }
    public void SpendCashOnRoom()
    {
        time += Time.deltaTime;
        if (!IsUnLocked)
        {
            if (GameManager.Instance.Money > 0 && time > .15f)
            {
                time = 0;
                GameManager.Instance.RemoveMoney(1);
                moneyNeeded--;
                moneyNeededText.text = $"{moneyNeeded}";
                if (moneyNeeded <= 0)
                {
                    OnUnlockingRoom();
                }
            }
        }
    }

    void OnUnlockingRoom()
    {
        IsUnLocked = true;
        spendCashTrigger.SetActive(false);
    }
}

public enum RoomState
{
    None,
    Occupied,
    Cleanded,
    Dirty,
}
