using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public const int HARD = 2;
    public const int NORMAL = 1;
    public const int EASY = 0;

    public static SettingsManager instance;

    public float volume;
    public int difficulty;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        difficulty = NORMAL;
    }


}
