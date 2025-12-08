using UnityEngine;

public class ScoreboardActivator : MonoBehaviour
{
    [SerializeField] private KonamiCodeListener konamiCodeListener;
    public GameObject scoreboard;

    private void OnEnable()
    {
        ScoreboardActive();
        scoreboard.SetActive(true);
    }

    private void OnDisable()
    {
       ScoreBoardClosed();
    }

    private void ScoreboardActive()
    {
        if (konamiCodeListener != null)
        {
            konamiCodeListener.isScoreBoardActive = true;
            konamiCodeListener.LockCamera();
        }
        else
        {
            Debug.LogWarning("KonamiCodeListener не назначен!", this);
        }
    }

     void ScoreBoardClosed()
    {
        if (konamiCodeListener != null)
        {
            konamiCodeListener.isScoreBoardActive = false;
            konamiCodeListener.UnlockCamera();
            scoreboard.SetActive(false);
        }
    }
}


