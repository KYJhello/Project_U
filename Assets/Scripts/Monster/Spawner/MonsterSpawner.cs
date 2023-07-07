using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    //[SerializeField] GameObject monsterPrefab;
    [SerializeField] float spawnTime;
    List<GameObject> gos;

    private void Awake()
    {
        gos = new List<GameObject>();
    }
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
            if(gos.Count < 3)
            {
                gos.Add(GameManager.Resource.Instantiate<GameObject>("Monster/Prefab/Spider", spawnPoint.position, spawnPoint.rotation, transform, true));
                gos[gos.Count-1].transform.position = spawnPoint.position;
            }
        }
    }
}
