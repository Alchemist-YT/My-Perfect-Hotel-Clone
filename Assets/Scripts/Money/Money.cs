using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] int moneyValue = 10;
    void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.AddMoney(moneyValue);
        Destroy(gameObject);
    }
    public void SetMoneyValue(int value)
    {
        moneyValue = value;
    }
}
