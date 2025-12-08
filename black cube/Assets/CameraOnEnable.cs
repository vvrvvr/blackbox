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
            konamiCodeListener.isCameraEnabled = true;
            konamiCodeListener.UnlockCamera();
        }
        else
        {
            Debug.LogWarning("KonamiCodeListener не назначен!", this);
        }
    }
}
