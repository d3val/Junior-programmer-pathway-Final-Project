using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : Enemy
{
    private bool isAngry = false;
    private float maxHealth;

    private new void Awake()
    {
        base.Awake();
        maxHealth = health;
    }

    private void Update()
    {
        Die();
        ObjectiveInRange();
        if (CheckHealthIsDown() && !isAngry)
        {
            Stop();
        }
    }

    protected override void Stop()
    {
        base.Stop();
        StartCoroutine(GetAngry());
    }

    IEnumerator GetAngry()
    {
        isAngry = true;
        Vector3 angryScale = transform.localScale * 1.5f;
        while (transform.localScale.x < angryScale.x && transform.localScale.x < angryScale.x && transform.localScale.x < angryScale.x)
        {
            transform.localScale *= 1.0005f;
            yield return null;
        }
        speed *= 2.5f;
        GoTo(m_tarjet);
    }

    private bool CheckHealthIsDown()
    {
        if (health <= maxHealth / 2)
            return true;
        else
            return false;

    }
}
