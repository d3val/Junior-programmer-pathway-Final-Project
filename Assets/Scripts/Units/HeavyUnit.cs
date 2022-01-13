using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyUnit : Unit
{
    private int charges = 1;

    protected override void Shoot()
    {
        if (charges > 3)
            charges = 1;
        StartCoroutine(MultipleShoot());
    }

    IEnumerator MultipleShoot()
    {
        for (int i = 0; i < charges; i++)
        {
            base.Shoot();
            yield return new WaitForSeconds(0.5f);
        }
        charges++;
    }
}
