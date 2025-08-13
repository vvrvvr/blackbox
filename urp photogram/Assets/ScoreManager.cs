using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
  public TextMeshProUGUI inputScore;
  public TMP_InputField inputName;
  public UnityEvent<string, int> submitScoreEvent;
  

  private const int MaxNameLength = 50;

  public void SubmintScore()
  {
    string username = inputName.text;

    if (username.Length > MaxNameLength)
    {
      username = username.Substring(0, MaxNameLength) + "...";
      
    }

    submitScoreEvent.Invoke(username, 0);
  }
}
