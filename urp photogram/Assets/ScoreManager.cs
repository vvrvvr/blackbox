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

  public void SubmintScore()
  {
    submitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text));
  }
  
}
