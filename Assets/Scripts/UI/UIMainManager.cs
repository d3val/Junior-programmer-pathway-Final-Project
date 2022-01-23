using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainManager : MonoBehaviour
{
    // ENCAPSULATION
    public static UIMainManager Instance { get; private set; }

    private Vector3 worldPosition;

    [SerializeField] Text moneyCount;
    public int Money { get; private set; }

    [SerializeField] Slider waveProgress;
    [SerializeField] Text waveCounter;
    private int waveCount = 1;

    public int enemiesInGame = 0;

    private Spawner spawner;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
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

    // ABSTRACTION
    // Shows the game over panel
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
        Money += n;
        moneyCount.text = Money.ToString();
    }

    private void UpdateWaveCounter()
    {
        waveCounter.text = waveCount.ToString();
    }

    public void SetWaveProgressValues(int nWaves)
    {
        waveProgress.maxValue = nWaves;
    }

    // Before start a new wave, wait until there is no 
    // more enemies.
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

    // Starts a new wave of enemies
    private void StartNewWave()
    {
        UpdateWaveCounter();
        waveCount++;
        UpdateWaveProgress(Mathf.RoundToInt(waveProgress.minValue));
        spawner.StartSpawningEnemies();
    }

    // ABSTRACTION
    public void PuseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    // ABSTRACTION
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // ABSTRACTION
    public void BackToMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    // ABSTRACTION
    public void ReloadScene()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
