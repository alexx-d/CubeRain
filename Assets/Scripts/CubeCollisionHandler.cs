using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollisionHandler : MonoBehaviour
{
    public event Action<Platform> PlatformTouched;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            PlatformTouched?.Invoke(platform);
        }
    }
}
