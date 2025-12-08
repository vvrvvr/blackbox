using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KonamiCodeListener : MonoBehaviour
{
    public FreeFlyCamera camera;
    public GameObject hintObject;
    public GameObject canvasControls;
    public float hintTime = 3f;
    private bool isCameraActive = false;
    public GameObject inputCanvas;

    [HideInInspector] public bool isCameraEnabled = false;
    [HideInInspector] public bool isScoreBoardActive = false;

    // Классический Konami-код
    private readonly List<KeyCode> konamiCode = new List<KeyCode> {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow
    };

    // Новый чит-код: V V V V ↓ ↓ ↓ ↓
    private readonly List<KeyCode> vDownCode = new List<KeyCode> {
        KeyCode.V, KeyCode.V, KeyCode.V, KeyCode.V,
        KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.DownArrow
    };

    private List<KeyCode> inputBuffer = new List<KeyCode>();

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) AddInput(KeyCode.UpArrow);
            if (Input.GetKeyDown(KeyCode.DownArrow)) AddInput(KeyCode.DownArrow);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) AddInput(KeyCode.LeftArrow);
            if (Input.GetKeyDown(KeyCode.RightArrow)) AddInput(KeyCode.RightArrow);
            if (Input.GetKeyDown(KeyCode.V)) AddInput(KeyCode.V);
        }
    }

    void AddInput(KeyCode key)
    {
        inputBuffer.Add(key);

        // Ограничиваем длину буфера по максимальной длине кодов
        int maxLength = Mathf.Max(konamiCode.Count, vDownCode.Count);
        if (inputBuffer.Count > maxLength)
            inputBuffer.RemoveAt(0);

        // Проверяем оба кода
        if (IsSequenceMatch(konamiCode))
        {
            inputBuffer.Clear();
            isCameraEnabled = true;
            UnlockCamera();
            return;
        }

        if (IsSequenceMatch(vDownCode))
        {
            inputBuffer.Clear();
            OnVDownCodeActivated();
            return;
        }
    }

    bool IsSequenceMatch(List<KeyCode> code)
    {
        if (inputBuffer.Count < code.Count)
            return false;

        for (int i = 0; i < code.Count; i++)
        {
            if (inputBuffer[inputBuffer.Count - code.Count + i] != code[i])
                return false;
        }
        return true;
    }

    public void UnlockCamera()
    {
        if(!isCameraEnabled)
            return;
        // Проверяем, активна ли уже камера и активно ли окно scoreboard
        if (isCameraActive || isScoreBoardActive)
            return;

        // Включаем камеру
        camera.enabled = true;
        isCameraActive = true;
        if (hintObject != null)
            StartCoroutine(ShowHintTemporarily());
    }

    public void LockCamera()
    {
        Debug.Log("Camera locked!");
        
        camera.enabled = false;
        isCameraActive = false;
    }

    IEnumerator ShowHintTemporarily()
    {
        hintObject.SetActive(true);
        canvasControls.SetActive(true);
        yield return new WaitForSeconds(hintTime);
        hintObject.SetActive(false);
    }

    // Новый метод для кода VVVV ↓↓↓↓
    private void OnVDownCodeActivated()
    {
        LockCamera();
        Debug.Log("v code");
    }
}
