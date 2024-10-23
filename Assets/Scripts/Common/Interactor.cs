using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    [SerializeField] UnityEvent OnStandingOnTrigger;
    void OnTriggerEnter(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
    }
    void OnTriggerStay(Collider other)
    {
        OnStandingOnTrigger?.Invoke();
    }
}
