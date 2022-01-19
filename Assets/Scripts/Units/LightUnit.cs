using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUnit : Unit
{
    public float beforeHeatTime;
    public float heat;
    public bool isCooling = false;
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
