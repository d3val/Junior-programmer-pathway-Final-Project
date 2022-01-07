using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Enemy target;
    public float intervalShoot = 0.5f;
    public GameObject projectile;
    private bool shooting = false;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    protected void Shoot()
    {
        AimTarget();

        Instantiate(projectile, transform.position, transform.rotation);
    }

    private void Update()
    {
        if (target != null && !shooting)
        {
            StartCoroutine(ShootRepeatily());
            shooting = true;
        }
    }

    private void AimTarget()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            if (target == null)
            {
                target = other.GetComponent<Enemy>();
            }
        }
    }

    IEnumerator ShootRepeatily()
    {
        Shoot();
        yield return new WaitForSeconds(intervalShoot);
        if (target == null)
            shooting = false;
        else
            StartCoroutine(ShootRepeatily());
    }
}
