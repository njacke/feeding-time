using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    

    private bool gameActive = false;


    private int finalScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }    

    public void StartGame(){
        SceneManager.LoadScene(1);
        gameActive = true;
        finalScore = 0;
    }

    public void GameOver(){
        gameActive = false;
        SceneManager.LoadScene(3);
    }

    public void GameWon(){
        gameActive = false;
        finalScore = FindObjectOfType<ScoreKeeper>().GetScore();
        SceneManager.LoadScene(2);
    }

    public void SetGameActive(bool state){
        gameActive = state;
    }

    public bool GetGameActive(){
        return gameActive;
    }

    public int GetFinalScore(){
        return finalScore;
    }
}
