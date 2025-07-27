using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KonamiCodeListener : MonoBehaviour
{
    public FreeFlyCamera camera;
    public GameObject hintObject;
    public float hintTime = 3f;
    
    private List<KeyCode> konamiCode = new List<KeyCode> {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow
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
            OnKonamiCodeEntered();
            inputBuffer.Clear();
        }
    }

    void OnKonamiCodeEntered()
    {
        Debug.Log("Konami Code activated!");
        camera.enabled = true;
        if (hintObject != null)
        {
            StartCoroutine(ShowHintTemporarily());
        }
    }
    
    IEnumerator ShowHintTemporarily()
    {
        hintObject.SetActive(true);
        yield return new WaitForSeconds(hintTime);
        hintObject.SetActive(false);
    }
}
