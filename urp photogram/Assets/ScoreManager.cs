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
    private const int MaxExtraLength = 50;

    public void SubmintScore()
    {
        string fullText = inputName.text;
        string username;
        string extra = " ";

        if (fullText.Length <= MaxNameLength)
        {
            username = fullText;
        }
        else
        {
            username = fullText.Substring(0, MaxNameLength);
            Debug.Log("username: "+username);

            // сколько символов остаётся после 50 для extra
            int remainingLength = fullText.Length - MaxNameLength;
            Debug.Log("remainingLength: "+remainingLength);

            // если больше MaxExtraLength, обрезаем и добавляем троеточие
            if (remainingLength > MaxExtraLength)
            {
                extra = fullText.Substring(MaxNameLength, MaxExtraLength) + "...";
                Debug.Log("extra: "+extra);
                Debug.Log("...");
            }
            else
            {
                extra = fullText.Substring(MaxNameLength, remainingLength);
                Debug.Log("else");
            }
        }

        submitScoreEvent.Invoke(username, extra);
    }
}