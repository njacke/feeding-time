using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;
    
    int currentScore = 0;

    private void Update() {
        scoreDisplay.text = currentScore.ToString();
    }


    public void AddScore(){
        currentScore++;
    }

    public int GetScore(){
        return currentScore;
    }

}
