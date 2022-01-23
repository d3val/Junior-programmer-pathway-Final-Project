using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainManager : MonoBehaviour
{
    public static UIMainManager instance;

    private Vector3 worldPosition;

    [SerializeField] Text moneyCount;
    public int money;

    [SerializeField] Slider waveProgress;
    [SerializeField] Text waveCounter;
    private int waveCount = 1;

    public int enemiesInGame = 0;

    private Spawner spawner;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        spawner = GameObject.Find("Spawn Manager").GetComponent<Spawner>();
        UpdateWaveCounter();
        SetWaveProgressValues(spawner.waves);
        StartNewWave();
    }

    // Set a gameObject's position in the world game depending on the mouse position.
    public void SetPositionInWorld(GameObject gameObject)
    {

        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            worldPosition = hit.point;
        }

        if (hit.collider != null)
        {
            gameObject.transform.position = worldPosition;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }

    public void UpdateWaveProgress()
    {
        waveProgress.value++;
    }

    public void UpdateWaveProgress(int n)
    {
        waveProgress.value = n;
    }

    public void UpdateMoney(int n)
    {
        money += n;
        moneyCount.text = money.ToString();
    }

    private void UpdateWaveCounter()
    {
        waveCounter.text = waveCount.ToString();
    }

    public void SetWaveProgressValues(int nWaves)
    {
        waveProgress.maxValue = nWaves;
    }

    public void CheckFinalWave()
    {
        if (waveProgress.value == waveProgress.maxValue)
        {
            StartCoroutine(WaitForNoEnemies());
        }
    }

    IEnumerator WaitForNoEnemies()
    {
        while (enemiesInGame > 0)
        {
            yield return new WaitForSeconds(1);
        }
        StartNewWave();
    }

    private void StartNewWave()
    {
        UpdateWaveCounter();
        waveCount++;
        UpdateWaveProgress(Mathf.RoundToInt(waveProgress.minValue));
        spawner.StartSpawningEnemies();
    }
}
