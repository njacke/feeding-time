using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject flyPrefab;
    [SerializeField] GameObject beePrefab;
    [SerializeField] int maxFlies = 8;
    [SerializeField] int maxBees = 5;
    [SerializeField] float minFlyDelay = 0f;
    [SerializeField] float maxFlyDelay = 2f;
    [SerializeField] float minBeeDelay = 0f;
    [SerializeField] float maxBeeDelay = 4f;
    [SerializeField] float minX = 6f;
    [SerializeField] float maxX = 12f;
    [SerializeField] float minY = 0f;
    [SerializeField] float maxY = 2f;


/*     public void SpawnFly(){
        float randomY = Random.Range(0f, 2f);
        var startingPosition = new Vector3(10f, randomY, 0f);
        Instantiate(flyPrefab, startingPosition, Quaternion.identity);
    } */

    private void Start() {
        StartCoroutine(SpawnFlies());
        StartCoroutine(SpawnBees());
    }

    IEnumerator SpawnFlies(){

        int spawnedFlies = 0;

        while(spawnedFlies < maxFlies){
            float randomDelay = Random.Range(minFlyDelay, maxFlyDelay);
            yield return new WaitForSeconds(randomDelay);

            SpawnFly();
            spawnedFlies++;
        }
    }

    public void SpawnFly(){
        float randomY = Random.Range(minY, maxY);
        float randomX = Random.Range (minX, maxX);
        var startingPosition = new Vector3(randomX, randomY, 0f);

        Instantiate(flyPrefab, startingPosition, Quaternion.identity);
    }

    public void RepositionFly(GameObject fly){
        float randomY = Random.Range(minY, maxY);
        float randomX = Random.Range (minX, maxX);
        var startingPosition = new Vector3(randomX, randomY, 0f);

        fly.transform.position = startingPosition;
    }
    IEnumerator SpawnBees(){

        int spawnedBees = 0;

        while(spawnedBees < maxBees){
            float randomDelay = Random.Range(minBeeDelay, maxBeeDelay);
            yield return new WaitForSeconds(randomDelay);

            SpawnBee();
            spawnedBees++;
        }
    }

    public void SpawnBee(){
        float randomY = Random.Range(minY, maxY);
        float randomX = Random.Range (minX, maxX);
        var startingPosition = new Vector3(-randomX, randomY, 0f);

        Instantiate(beePrefab, startingPosition, Quaternion.identity);
    }

    public void RepositionBee(GameObject bee){
        float randomY = Random.Range(minY, maxY);
        float randomX = Random.Range (minX, maxX);
        var startingPosition = new Vector3(-randomX, randomY, 0f);

        bee.transform.position = startingPosition;
    }    
}
