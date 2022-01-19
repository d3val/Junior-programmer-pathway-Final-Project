using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] Slider healthBar;

    private void Awake()
    {
        healthBar.maxValue = health;
        healthBar.value = health;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    private void Update()
    {
        CheckHealth();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
    }

    private void CheckHealth()
    {
        if (health <= 0)
            UIMainManager.instance.GameOver();
    }
}
