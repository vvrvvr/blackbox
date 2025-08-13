using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI inputScore;
    public TMP_InputField inputName;
    public UnityEvent<string, string> submitScoreEvent;

    private const int MaxNameLength = 50;
    private const int MaxExtraLength = 90;

    public void SubmintScore()
    {
        string fullText = inputName.text;
        string username;
        string extra = " ";

        if (fullText.Length <= MaxNameLength)
        {
            username = fullText; // полностью помещается
        }
        else
        {
            // первые 50 символов в username
            username = fullText.Substring(0, MaxNameLength);

            // оставшиеся символы в extra, максимум 90 символов
            int remainingLength = fullText.Length - MaxNameLength;
            int extraLength = Mathf.Min(remainingLength, MaxExtraLength);

            extra = fullText.Substring(MaxNameLength, extraLength);

            // добавляем троеточие, если что-то обрезалось
            if (extraLength < remainingLength)
            {
                extra += "...";
            }
        }

        submitScoreEvent.Invoke(username, extra);
    }
}