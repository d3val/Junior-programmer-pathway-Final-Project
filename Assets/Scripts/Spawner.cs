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

    private void Awake()
    {
        if (spawnManager == null)
        {
            spawnManager = this;
        }
        else
            Destroy(gameObject);

        StartCoroutine(SpawnContinuousWaves(waves, timeBetweenWaves));
    }

  /*  private Vector3 GetRandomPosition()
    {
        Vector3 position;

        return position;
    }*/

    public void SpawnWave()
    {
        foreach (GameObject enemy in enemies)
        {
            Instantiate(enemy);
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
            SpawnWave();
            yield return new WaitForSeconds(time);
        }
    }

}
