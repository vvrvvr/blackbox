using UnityEngine;

public class EscapeText : MonoBehaviour
{
    public GameObject textObject; // Ссылка на объект с текстом
    public float time = 2f;       // Время, на которое включается текст

    private float timer = 0f;
    private bool isShowing = false;

    void Update()
    {
        // Если Escape нажата или удерживается
        if (Input.GetKey(KeyCode.Escape))
        {
            ShowText();
        }

        // Если таймер запущен — уменьшаем его
        if (isShowing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                HideText();
            }
        }
    }

    void ShowText()
    {
        textObject.SetActive(true);
        timer = time;       // Обновляем таймер при каждом нажатии/удержании
        isShowing = true;
    }

    void HideText()
    {
        textObject.SetActive(false);
        isShowing = false;
    }
}