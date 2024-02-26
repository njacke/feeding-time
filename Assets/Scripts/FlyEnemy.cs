using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float triggerPosX = -6f;

    GameObject tongue;

    TongueController tongueController;

    EnemySpawner enemySpawner;

    ScoreKeeper scoreKeeper;

    Vector3 positionDifference;
    bool gotHit = false;
    bool posDiffAssigned = false;

    bool posReported = false;

    void Start()
    {
        tongueController = FindObjectOfType<TongueController>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        StartCoroutine(WaitForTongueAssignment());
    }

    IEnumerator WaitForTongueAssignment(){
        yield return new WaitUntil(() => TongueManager.Instance.GetTongue() != null);
        tongue = TongueManager.Instance.GetTongue();
    }

void Update()
{
    if (gotHit && tongueController.GetHasExtended() && !posDiffAssigned)
    {
        positionDifference = transform.position - tongue.transform.position;
        Debug.Log("First position diff: " + positionDifference);
        posDiffAssigned = true;
    }

    if (GameManager.Instance.GetGameActive())
    {
        if (!gotHit)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            if (transform.position.x < triggerPosX) //out of screen
            {
                enemySpawner.RepositionFly(gameObject); //change position to start
            }
        }
        else
        {
            if (posDiffAssigned)
            {
                if (!posReported)
                {
                    Debug.Log(positionDifference);
                    posReported = true;
                    Debug.Log("Position was reported");
                }
                transform.position = tongue.transform.position + positionDifference;
            }

            if (tongueController.GetCanMove())
            {
                FlyReset();
            }
        }
    }
}

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("FLY TRIGGED");        
        if (!gotHit && other.tag == "Tongue"){
            gotHit = true;
            }
    }

    private void FlyReset(){
        enemySpawner.RepositionFly(gameObject);
        scoreKeeper.AddScore();
        gotHit = false;        
    }
}

