using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RaceInfoTablo : MonoBehaviour
{
    [SerializeField] TMP_Text TimerText, bestTimeText, currentTimeText;
    [SerializeField] GameObject ScoreCanvas, resultCanvas;
    [SerializeField] Race race;

    private void Start()
    {
        TimerText.text = "00:00";
    }
    public void StartTablo()
    {
        StartCoroutine(countTime());
    }
    IEnumerator countTime()
    {
        resultCanvas.SetActive(false);
        ScoreCanvas.SetActive(true);
        string strSec = "00";
        string strMin = "00";
        int seconds = 0;
        int minutes = 0;
        while (!race.isFinished)
        {
            seconds = (int)race.timeAfterStart % 60;
            minutes = (int)race.timeAfterStart / 60;
            strSec = seconds.ToString();
            strMin = minutes.ToString();
            if (seconds < 10)
            {
                strSec = "0" + strSec;
            }
            if (minutes < 10)
            {
                strMin = "0" + strMin;
            }
            TimerText.text = strMin + ":" + strSec;
            yield return new WaitForSeconds(0.1f);
        }
        ScoreCanvas.SetActive(false);
        currentTimeText.text = "Your time: " + strMin + ":" + strSec;
        race.CheckAndSaveBestTime(seconds, minutes);
        string btText = "Best time: ";
        btText += (((int)race.bestTime / 60)<10)? "0"+((int)race.bestTime/60).ToString(): ((int)race.bestTime/60);
        btText += ":";
        btText += (((int)race.bestTime % 60)<10)? "0"+((int)race.bestTime%60).ToString(): ((int)race.bestTime%60);
        bestTimeText.text = btText;
        resultCanvas.SetActive(true);
        
    }
}
