using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] Slider healthBar;
    private bool gameOver = false;
    [SerializeField] ParticleSystem gameOverExplosion;
    private MeshRenderer render;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
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
        if (health <= 0 && !gameOver)
        {
            gameOver = true;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        gameOverExplosion.Play();
        render.enabled = false;
        yield return new WaitForSeconds(2);
        UIMainManager.instance.GameOver();
    }
}
