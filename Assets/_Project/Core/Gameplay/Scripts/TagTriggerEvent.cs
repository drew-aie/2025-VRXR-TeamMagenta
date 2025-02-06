using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TagTriggerEvent : MonoBehaviour
{
    public string TagToCompare;
    public UnityEvent OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagToCompare) || TagToCompare.Length < 1)
            OnTrigger.Invoke();
    }
}
