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
    float coinSpawnTime = 20;

    [SerializeField] float xBounds;
    [SerializeField] float zBounds;

    [SerializeField] GameObject coin;
    float coinSpawnLimitX = 7.5f;
    float coinSpawnLimitZ = 11f;
    readonly float coinYPos = 10.5f;

    private int spawnPosVariance = 1;

    private int stdEnemies = 2;
    private int fstEnemies = 1;
    private int hvyEnemies = 0;

    private int enemiesIncreaseRate = 1;

    private void Awake()
    {
        if (spawnManager == null)
        {
            spawnManager = this;
        }
        else
            Destroy(gameObject);

        SetDifficulty();

        InvokeRepeating("RandomSpawnCoin", 5, coinSpawnTime);
    }

    private void SetDifficulty()
    {
        if (SettingsManager.instance != null)
        {
            switch (SettingsManager.instance.difficulty)
            {
                case SettingsManager.HARD:
                    RandomSpawnCoin(1);
                    timeBetweenWaves *= 0.75f;
                    stdEnemies = 3;
                    fstEnemies = 2;
                    hvyEnemies = 0;
                    break;
                case SettingsManager.EASY:
                    RandomSpawnCoin(3);
                    timeBetweenWaves *= 1.25f;
                    coinSpawnTime *= 0.75f;
                    stdEnemies = 2;
                    fstEnemies = 0;
                    hvyEnemies = 0;
                    break;
                default:
                    RandomSpawnCoin(2);
                    break;
            }
        }
    }

    private void RandomSpawnCoin()
    {

        float randomX = Random.Range(-coinSpawnLimitX, coinSpawnLimitX);
        float randomZ = Random.Range(-coinSpawnLimitZ, coinSpawnLimitZ);
        Vector3 spawnPosition = new Vector3(randomX, coinYPos, randomZ);

        Instantiate(coin, spawnPosition, coin.transform.rotation);
    }

    private void RandomSpawnCoin(int nCoins)
    {
        float randomZ;
        float randomX;
        Vector3 spawnPosition;
        for (int i = 0; i < nCoins; i++)
        {
            randomX = Random.Range(-coinSpawnLimitX, coinSpawnLimitX);
            randomZ = Random.Range(-coinSpawnLimitZ, coinSpawnLimitZ);
            spawnPosition = new Vector3(randomX, coinYPos, randomZ);

            Instantiate(coin, spawnPosition, coin.transform.rotation);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float randomX;
        float randomZ;

        randomX = Random.Range(-xBounds, xBounds);
        randomZ = Random.Range(-zBounds, zBounds);
        if (randomX > -25 && randomX < 25)
        {
            randomZ = 25 * spawnPosVariance;
            spawnPosVariance *= -1;
        }
        else if (randomZ > -25 && randomZ < 25)
        {
            randomX = -25 * spawnPosVariance;
            spawnPosVariance *= -1;
        }
        Vector3 position = new Vector3(randomX, 0, randomZ);

        return position;
    }

    public void RandomSpawnWave()
    {
        Vector3 randomSpawnPosition;
        for (int i = 0; i < stdEnemies; i++)
        {
            randomSpawnPosition = GetRandomPosition();
            Instantiate(enemies[0], randomSpawnPosition, enemies[0].transform.rotation);
        }

        for (int i = 0; i < fstEnemies; i++)
        {
            randomSpawnPosition = GetRandomPosition();
            Instantiate(enemies[1], randomSpawnPosition, enemies[1].transform.rotation);
        }
        for (int i = 0; i < hvyEnemies; i++)
        {
            randomSpawnPosition = GetRandomPosition();
            Instantiate(enemies[2], randomSpawnPosition, enemies[2].transform.rotation);
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

    private void IncreaseEnemiesSpawn()
    {
        stdEnemies += enemiesIncreaseRate;
        fstEnemies += enemiesIncreaseRate;
        hvyEnemies += enemiesIncreaseRate;
        timeBetweenWaves *= 0.9f;
    }

    public void StartSpawningEnemies()
    {
        StartCoroutine(SpawnContinuousWaves(waves, timeBetweenWaves));
    }

    IEnumerator SpawnContinuousWaves(int wavesCantity, float time)
    {
        for (int i = 0; i < wavesCantity; i++)
        {
            RandomSpawnWave();
            yield return new WaitForSeconds(time);
            UIMainManager.instance.UpdateWaveProgress();
        }
        IncreaseEnemiesSpawn();
    }

}
