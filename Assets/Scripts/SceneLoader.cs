using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void StartGame(){
        GameManager.Instance.StartGame();
    }
}
