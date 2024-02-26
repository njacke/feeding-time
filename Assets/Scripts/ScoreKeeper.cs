using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;
    
    private int currentScore = 0;

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

