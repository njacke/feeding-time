using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update

[SerializeField] TextMeshProUGUI scoreDisplay;

private void Start() {
    scoreDisplay.text = GameManager.Instance.GetFinalScore().ToString();
}




}
