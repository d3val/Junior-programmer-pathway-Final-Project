using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;

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
        gameOverPanel.SetActive(true);
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

    public void PuseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
