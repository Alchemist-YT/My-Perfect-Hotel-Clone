using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public static event Action<int> OnMoneyValueChangeEvent;
    [field: SerializeField] public int Money { get; private set; }

    public void AddMoney(int _money)
    {
        Money += _money;
        OnMoneyValueChangeEvent?.Invoke(Money);
    }

    public void RemoveMoney(int _money)
    {
        Money -= _money;
        OnMoneyValueChangeEvent?.Invoke(Money);
    }
}
