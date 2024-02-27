using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Countdown : MonoBehaviour
{
    [SerializeField] float timeRemaining = 60;

    [SerializeField] TextMeshProUGUI countdownText;
    bool countdownFinished = false;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (timeRemaining < 0)
        {
            timeRemaining = 0;
            countdownFinished = true;
        }

        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        if (countdownFinished == true)
        {
            StartCoroutine(GameWonWithDelay());
        }
    }

    IEnumerator GameWonWithDelay(){
        GameManager.Instance.SetGameActive(false);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.GameWon();
    }
}