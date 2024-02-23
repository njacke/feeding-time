using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    GameObject tongue;

    TongueController tongueController;

    EnemySpawner enemySpawner;

    ScoreManager scoreManager;

    Vector3 positionDifference;
    bool gotHit = false;
    bool posDiffAssigned = false;

    void Start()
    {
        tongueController = FindObjectOfType<TongueController>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        scoreManager = FindObjectOfType<ScoreManager>();

        StartCoroutine(WaitForTongueAssignment());
    }

    IEnumerator WaitForTongueAssignment(){
        yield return new WaitUntil(() => TongueManager.Instance.GetTongue() != null);
        tongue = TongueManager.Instance.GetTongue();
    }

    void Update()
    {
        if(gotHit && tongueController.GetHasExtended() && !posDiffAssigned){
            //Debug.Log(tongue.transform.position);
            positionDifference = transform.position - tongue.transform.position;
            posDiffAssigned = true;
        }
        
        if(!gotHit){
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            if (transform.position.x < -10f) //if out of screen
            {
                ResetFlyPosition();
            } 
        }
        
        else{

            if (tongueController.GetHasExtended() && posDiffAssigned){
                transform.position = tongue.transform.position + positionDifference;
            }

            if(tongueController.GetCanMove()){
                FlyRemoval();              
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("FLY TRIGGED");        
        if (!gotHit){
            if(tongueController.GetHasExtended()){
                positionDifference = transform.position - tongue.transform.position;
                posDiffAssigned = true;
            }        
            gotHit = true;
            scoreManager.AddScore();
        }
    }

    private void FlyRemoval(){
        enemySpawner.SpawnFly();
        Destroy(gameObject);
    }
    

    private void ResetFlyPosition()
    {
        //set fly's position to the starting position
        float randomY = Random.Range(0f, 2f);
        transform.position = new Vector3(10f, randomY, 0f);
    }

}

