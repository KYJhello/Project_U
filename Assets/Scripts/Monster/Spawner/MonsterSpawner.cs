using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] float spawnTime;


    private void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }


    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
