using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;

    void OnEnable()
    {
        GameManager.OnMoneyValueChangeEvent += GameManager_OnMoneyValueChangeEvent;
    }

    void GameManager_OnMoneyValueChangeEvent(int money)
    {
        moneyText.text = money.ToStringPrice();
    }

    void OnDisable()
    {
        GameManager.OnMoneyValueChangeEvent -= GameManager_OnMoneyValueChangeEvent;
    }
}
