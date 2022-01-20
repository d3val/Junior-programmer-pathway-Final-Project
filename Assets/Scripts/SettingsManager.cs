using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static readonly int Hard = 2;
    public static readonly int Normal = 1;
    public static readonly int Easy = 0;

    public static SettingsManager instance;

    public float volume;
    public float difficulty;

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
    }


}
