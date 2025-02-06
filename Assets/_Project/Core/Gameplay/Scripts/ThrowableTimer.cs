using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableTimer : MonoBehaviour
{
    [SerializeField]
    private float _lifeTime = 5.0f;

    private Coroutine _coroutine;


    private void OnEnable()
    {
        if (TryGetComponent(out HVRGrabbable grabbable))
        {
            grabbable.HandGrabbed.AddListener(StopTimer);
            grabbable.HandGrabbed.AddListener(StartTimer);

        }
    }
    private void OnDisable()
    {
        if (TryGetComponent(out HVRGrabbable grabbable))
        {
            grabbable.HandGrabbed.RemoveListener(StopTimer);
            grabbable.HandGrabbed.RemoveListener(StartTimer);

        }
    }
    public void StartTimer(HVRHandGrabber grabber, HVRGrabbable grabbable)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        StartCoroutine(TimerCoroutine());
    }
    public void StopTimer(HVRHandGrabber grabber, HVRGrabbable grabbable)
    {
        StopCoroutine(TimerCoroutine());
    }
    private IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }

}
