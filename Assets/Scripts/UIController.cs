using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    static UIController instance;
    public static UIController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIController>();
            }
            return instance;
        }
    }

    public Text[] scoreCounters;
    public Text[] bestScoresCounters;

    public void UpdateScore()
    {
        string scoreString = GameManager.Instance.TotalScore.ToString();
        foreach(var text in scoreCounters)
        {
            text.text = scoreString;
        }
    }

    public void UpdateBestScores()
    {
        string bestScoreString = GameManager.Instance.BestScore.ToString();
        foreach (var text in bestScoresCounters)
        {
            text.text = bestScoreString;
        }
    }
}
