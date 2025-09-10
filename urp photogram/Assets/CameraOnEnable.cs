using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnEnable : MonoBehaviour
{
    [SerializeField] private KonamiCodeListener konamiCodeListener;

    private void OnEnable()
    {
        if (konamiCodeListener != null)
        {
            konamiCodeListener.OnCameraUnlocked();
        }
        else
        {
            Debug.LogWarning("KonamiCodeListener не назначен!", this);
        }
    }
}
