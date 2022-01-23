using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class LightUnit : Unit
{
    private readonly float beforeHeatTime = 5;
    private float heat;
    private bool isCooling = false;
    [SerializeField] ParticleSystem smoke;

    private void Update()
    {
        AimTarget();
        CheckHeat();
        if (target != null && !isShooting && !isCooling)
        {
            StartCoroutine(ShootRepeatily());
            isShooting = true;
        }
    }

    // Checks if the heat level and controls isShooting state
    private void CheckHeat()
    {
        if (isShooting)
            heat += Time.deltaTime;
        else
            heat -= Time.deltaTime;

        if (heat <= 0)
        {
            isCooling = false;
            heat = 0;
            smoke.Stop();
        }

        if (heat >= beforeHeatTime)
        {
            isCooling = true;
            smoke.Play();
        }
    }

    // POLYMORPHISM
    // Diferent shoot behaviour depending on a heat level;
    protected override IEnumerator ShootRepeatily()
    {
        Shoot();
        yield return new WaitForSeconds(intervalShoot);
        if (target == null || isCooling)
            isShooting = false;
        else
            StartCoroutine(ShootRepeatily());
    }

}
