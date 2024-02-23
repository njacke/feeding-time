using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject flyPrefab;

    public void SpawnFly(){
        float randomY = Random.Range(0f, 2f);
        var startingPosition = new Vector3(10f, randomY, 0f);
        Instantiate(flyPrefab, startingPosition, Quaternion.identity);
    }
}
