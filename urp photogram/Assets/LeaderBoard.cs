using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class LeaderBoard : MonoBehaviour
{
   [SerializeField] private List<TextMeshProUGUI> names;
   [SerializeField] private List<TextMeshProUGUI> scores;
   private string publicLeaderboardKey = 
         "8a1a45fd3e13c3224df5b97efb3bb7e6b25815ba748a3e2a4a84dd55b0ca42f1";

   private void Start()
   {
        GetLeaderBoard();
   }

   public void GetLeaderBoard()
   {
      LeaderboardCreator.GetLeaderboard(publicLeaderboardKey,((msg) =>
      {
          int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
          for (int i = 0; i < loopLength; i++)
          {
              names[i].text = msg[i].Username;
              scores[i].text = msg[i].Score.ToString();
          }
      }) );
   }

   public void SetLeaderboardEntry(string username, int score)
   {
       LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username,  score, ((msg) =>
       {
          // username.Substring(0, 140);
           GetLeaderBoard();
       }));
   }
   
   public void ResetPlayer()
   {
       LeaderboardCreator.ResetPlayer();
   }
}
