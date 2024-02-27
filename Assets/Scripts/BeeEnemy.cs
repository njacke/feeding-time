using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float triggerPosX = 6f;
    [SerializeField] AudioClip hitSound;
    SpriteRenderer spriteRenderer;
    EnemySpawner enemySpawner;

    bool gotHit = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {       
        if(!gotHit && GameManager.Instance.GetGameActive()){
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            if (transform.position.x > triggerPosX) //out of screen
            {
                enemySpawner.RepositionBee(gameObject); //change position to start
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("BEE TRIGGED");        
        if (!gotHit && other.tag == "Tongue"){
            AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);    
            gotHit = true;
            GameManager.Instance.SetGameActive(false);            
            StartCoroutine(HitVisualEffect());
            StartCoroutine(GameOverWithDelay());
        }
    }

    IEnumerator GameOverWithDelay(){
        yield return new WaitForSeconds (2f);
        GameManager.Instance.GameOver();
    }

    IEnumerator HitVisualEffect(){                
        while (true){
            spriteRenderer.color = new Color32 (200, 60, 30, 255);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = new Color32 (255, 255, 255, 255);
            yield return new WaitForSeconds(0.2f);
        }
    }

}
