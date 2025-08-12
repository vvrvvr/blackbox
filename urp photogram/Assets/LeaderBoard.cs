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

    [Header("Coordinate Range")]
    [SerializeField] private int minX = 0;
    [SerializeField] private int maxX = 1500;
    [SerializeField] private int minY = -1500;
    [SerializeField] private int maxY = 0;

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
                    Vector2 coords = DecodeCoordinates(msg[i].Extra);
                    rt.anchoredPosition = coords;
                    rt.localRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-20f, 20f));
                }

                // Заполняем текст и цвет
                TextMeshProUGUI textComp = entry.GetComponentInChildren<TextMeshProUGUI>();
                if (textComp != null)
                {
                    string dateString = FormatEntryDate(msg[i].Date);
                    textComp.text = $"{msg[i].Username} ({dateString})";

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

    private Vector2 DecodeCoordinates(string extra)
    {
        if (string.IsNullOrEmpty(extra))
            return Vector2.zero;

        string[] parts = extra.Split(';');
        if (parts.Length != 2)
            return Vector2.zero;

        if (int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y))
        {
            return new Vector2(x, y);
        }

        return Vector2.zero;
    }
    
    private string FormatEntryDate(ulong dateValue)
    {
        DateTime dateTime;
        if (dateValue > 1000000000000) // миллисекунды
            dateTime = DateTimeOffset.FromUnixTimeMilliseconds((long)dateValue).DateTime;
        else // секунды
            dateTime = DateTimeOffset.FromUnixTimeSeconds((long)dateValue).DateTime;

        return dateTime.ToString("dd.MM.yyyy");
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        // Генерируем случайные координаты в заданном диапазоне
        int x = UnityEngine.Random.Range(minX, maxX + 1);
        int y = UnityEngine.Random.Range(minY, maxY + 1);

        // Формируем строку для extra
        string extra = $"{x};{y}";

        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, extra, (success) =>
        {
            if (success)
                GetLeaderBoard();
        },
        (error) =>
        {
            Debug.LogError("Ошибка загрузки записи: " + error);
        });
    }

    public void ResetPlayer()
    {
        LeaderboardCreator.ResetPlayer();
    }
}
