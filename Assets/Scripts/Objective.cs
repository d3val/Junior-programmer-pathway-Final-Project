using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public int health = 30;

    private void Update()
    {
        if (health <= 0)
            Debug.Log("F");
    }
}
