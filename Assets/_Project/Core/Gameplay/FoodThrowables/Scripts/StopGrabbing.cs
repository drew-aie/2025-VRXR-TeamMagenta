using HurricaneVR.Framework.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopGrabbing : MonoBehaviour
{
    public void LetGo()
    {
        if (TryGetComponent<HVRGrabbable>(out HVRGrabbable grab))
            grab.enabled = false;
    }
}
