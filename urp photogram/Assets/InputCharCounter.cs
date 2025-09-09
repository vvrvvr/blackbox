using UnityEngine;
using TMPro;

public class InputCharCounter : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField; // поле ввода
    [SerializeField] private TMP_Text counterText;      // текст с оставшимися символами
    [SerializeField] private int maxChars = 100;        // максимальное число символов

    private void Start()
    {
        if (inputField != null)
            inputField.onValueChanged.AddListener(UpdateCounter);

        UpdateCounter(inputField.text);
    }

    private void UpdateCounter(string currentText)
    {
        int remaining = Mathf.Max(0, maxChars - currentText.Length);
        counterText.text = $"Осталось: {remaining}";
    }
}