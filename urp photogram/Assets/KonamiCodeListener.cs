using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KonamiCodeListener : MonoBehaviour
{
    public FreeFlyCamera camera;
    public GameObject hintObject;
    public float hintTime = 3f;
    private bool isCameraActive = false;
    public GameObject inputCanvas;
    public int launchState;

    private const string LaunchStateKey = "LaunchState"; // ключ для PlayerPrefs

    private List<KeyCode> konamiCode = new List<KeyCode> {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow
    };

    private List<KeyCode> inputBuffer = new List<KeyCode>();

    void Awake()
    {
       
        // Загружаем сохранённый launchState (по умолчанию 0)
       // ResetLaunchState();
        launchState = PlayerPrefs.GetInt(LaunchStateKey, 0);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) AddInput(KeyCode.UpArrow);
            if (Input.GetKeyDown(KeyCode.DownArrow)) AddInput(KeyCode.DownArrow);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) AddInput(KeyCode.LeftArrow);
            if (Input.GetKeyDown(KeyCode.RightArrow)) AddInput(KeyCode.RightArrow);
        }
    }

    void AddInput(KeyCode key)
    {
        inputBuffer.Add(key);

        // Сохраняем длину буфера в пределах длины кода
        if (inputBuffer.Count > konamiCode.Count)
            inputBuffer.RemoveAt(0);

        // Проверка на совпадение
        if (inputBuffer.Count == konamiCode.Count)
        {
            for (int i = 0; i < konamiCode.Count; i++)
            {
                if (inputBuffer[i] != konamiCode[i])
                    return;
            }

            // Совпадение — активируем код
            OnCameraUnlocked();
            inputBuffer.Clear();
        }
    }

    public void OnCameraUnlocked()
    {
        Debug.Log("Konami Code activated!");
        if (isCameraActive)
            return;

        switch (launchState)
        {
            case 0:
                launchState = 1;
                SaveLaunchState();
                inputCanvas.SetActive(true);
                return;

            case 1:
                launchState = 2;
                SaveLaunchState();
                inputCanvas.SetActive(false);
                camera.enabled = true;
                isCameraActive = true;
                if (hintObject != null)
                    StartCoroutine(ShowHintTemporarily());
                return;

            case 2:
                inputCanvas.SetActive(false);
                camera.enabled = true;
                isCameraActive = true;
                if (hintObject != null)
                    StartCoroutine(ShowHintTemporarily());
                return;

            default:
                break;
        }
    }

    private void SaveLaunchState()
    {
        PlayerPrefs.SetInt(LaunchStateKey, launchState);
        PlayerPrefs.Save(); // Принудительно сохраняем на диск
    }

    IEnumerator ShowHintTemporarily()
    {
        hintObject.SetActive(true);
        yield return new WaitForSeconds(hintTime);
        hintObject.SetActive(false);
    }
    public void ResetLaunchState()
    {
        launchState = 0; // сброс к начальному значению
        PlayerPrefs.SetInt(LaunchStateKey, launchState);
        PlayerPrefs.Save();
        Debug.Log("LaunchState сброшен к начальному значению (0)");
    }
   
}
