using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] Dropdown diffifultyDropdown;

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        SetGameDifficulty();
        SceneManager.LoadScene("Main");
    }

    public void SetGameDifficulty()
    {
        SettingsManager.instance.difficulty = diffifultyDropdown.value;
        Debug.Log($"La dificultad esta en: {SettingsManager.instance.difficulty}");
    }

}
