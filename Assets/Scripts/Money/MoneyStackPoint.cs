using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStackPoint : MonoBehaviour
{
    Money money;
    public void SetMoney(Money _money)
    {
        money = _money;
    }
    public bool HasMoney() => money;
}
