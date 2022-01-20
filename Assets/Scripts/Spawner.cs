using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;

    public static Spawner spawnManager;

    private Vector3 spawnPosition;

    public int waves;
    [SerializeField] float timeBetweenWaves;

    [SerializeField] float xBounds;
    [SerializeField] float zBounds;

    [SerializeField] GameObject coin;
    float coinSpawnLimitX = 7.5f;
    float coinSpawnLimitZ = 11f;
    readonly float coinYPos = 10.5f;

    private int variance = 1;

    private void Awake()
    {
        if (spawnManager == null)
        {
            spawnManager = this;
        }
        else
            Destroy(gameObject);

        StartCoroutine(SpawnContinuousWaves(waves, timeBetweenWaves));
        InvokeRepeating("RandomSpawnCoin", 3, 6);
    }

    private void RandomSpawnCoin()
    {

        float randomX = Random.Range(-coinSpawnLimitX, coinSpawnLimitX);
        float randomZ = Random.Range(-coinSpawnLimitZ, coinSpawnLimitZ);
        Vector3 spawnPosition = new Vector3(randomX, coinYPos, randomZ);

        Instantiate(coin, spawnPosition, coin.transform.rotation);

    }

    private Vector3 GetRandomPosition()
    {
        float randomX;
        float randomZ;

        randomX = Random.Range(-xBounds, xBounds);
        randomZ = Random.Range(-zBounds, zBounds);
        if (randomX > -25 && randomX < 25)
        {
            randomZ = 25 * variance;
            variance *= -1;
        }
        else if (randomZ > -25 && randomZ < 25)
        {
            randomX = -25 * variance;
            variance *= -1;
        }
        Vector3 position = new Vector3(randomX, 0, randomZ);

        return position;
    }

    public void RandomSpawnWave()
    {
        foreach (GameObject enemy in enemies)
        {
            Vector3 randomSpawnPosition = GetRandomPosition();
            Instantiate(enemy, randomSpawnPosition, enemy.transform.rotation);
        }
    }

    // Spawns a GameObject using a raycast that starts from the main camera and aims to the mouse position.
    public void SpawnWithRaycast(GameObject prefab)
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            spawnPosition = hit.point;
        }

        if (hit.collider != null)
            Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    IEnumerator SpawnContinuousWaves(int wavesCantity, float time)
    {
        for (int i = 0; i < wavesCantity; i++)
        {
            RandomSpawnWave();
            yield return new WaitForSeconds(time);
        }
    }

}
