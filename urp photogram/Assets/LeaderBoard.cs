using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class LeaderBoard : MonoBehaviour
{
    [Header("UI Prefab")]
    [SerializeField] private GameObject nameBasePrefab; // Шаблон одного элемента
    [SerializeField] private Canvas parentCanvas;       // Канвас, на который будем спавнить

    [Header("Colors")]
    [SerializeField] private Color[] textColors; // Массив цветов

    private string publicLeaderboardKey =
        "8a1a45fd3e13c3224df5b97efb3bb7e6b25815ba748a3e2a4a84dd55b0ca42f1";

    private void Start()
    {
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
    {
        // Удаляем старые элементы
        foreach (Transform child in parentCanvas.transform)
        {
            Destroy(child.gameObject);
        }

        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, (msg) =>
        {
            for (int i = 0; i < msg.Length; i++)
            {
                // Создаём новый элемент на канвасе
                GameObject entry = Instantiate(nameBasePrefab, parentCanvas.transform);

                // Ставим его в (0,0) относительно канваса
                RectTransform rt = entry.GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.anchoredPosition = Vector2.zero;
                    rt.localRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-20f, 20f));
                }

                // Заполняем текст и цвет
                TextMeshProUGUI textComp = entry.GetComponentInChildren<TextMeshProUGUI>();
                if (textComp != null)
                {
                    textComp.text = msg[i].Username;

                    if (textColors != null && textColors.Length > 0)
                    {
                        Color c = textColors[UnityEngine.Random.Range(0, textColors.Length)];
                        c.a = 1f;
                        textComp.color = c;
                    }
                }
            }
        });
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, (msg) =>
        {
            GetLeaderBoard();
        });
    }

    public void ResetPlayer()
    {
        LeaderboardCreator.ResetPlayer();
    }
}
