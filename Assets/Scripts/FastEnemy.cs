using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    public float stamina = 10;
    public float restTime = 3;
    private float currentStamina = 10;
    private bool isResting = false;

    private new void Awake()
    {
        base.Awake();
        currentStamina = stamina;
    }

    private void ReduceStamina()
    {
        if (currentStamina <= 0 && !isResting)
            StartCoroutine(Rest());
        else
            currentStamina -= Time.deltaTime;
    }

    private void Update()
    {
        Die();
        ObjectiveInRange();
        ReduceStamina();
    }

    IEnumerator Rest()
    {
        isResting = true;
        Stop();
        yield return new WaitForSeconds(restTime);
        currentStamina = stamina;
        GoTo(m_tarjet);
        isResting = false;
    }
}
