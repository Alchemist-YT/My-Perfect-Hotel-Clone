using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public event Action OnStateChange;

    [SerializeField] RoomItemState roomItemState = RoomItemState.Cleaned;
    [SerializeField] float cleanTime = 3;
    [SerializeField] Image cleanFillImage;
    [SerializeField] GameObject cleanParent;

    float time;

    void Start()
    {
        OnCleaned();
    }
    public bool IsUsable() => roomItemState == RoomItemState.Cleaned;
    public void Clean()
    {
        if (roomItemState == RoomItemState.Cleaned) return;
        time += Time.deltaTime;
        cleanFillImage.fillAmount = time / cleanTime;
        if (time > cleanTime)
        {
            OnCleaned();
            time = 0;
        }
    }

    void OnCleaned()
    {
        cleanParent.SetActive(false);
        roomItemState = RoomItemState.Cleaned;
        OnStateChange?.Invoke();
    }

    public void MakeDirty()
    {
        roomItemState = RoomItemState.Dirty;
        OnStateChange?.Invoke();
        cleanParent.SetActive(true);
    }
}
public enum RoomItemState
{
    None,
    Dirty,
    Cleaned
}
