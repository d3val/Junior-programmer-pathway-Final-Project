using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    protected Enemy target;
    public float intervalShoot = 0.5f;
    public GameObject projectile;
    protected bool isShooting = false;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    protected virtual void Shoot()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }

    private  void Update()
    {
        AimTarget();
        if (target != null && !isShooting)
        {
            StartCoroutine(ShootRepeatily());
            isShooting = true;
        }
    }

    protected void AimTarget()
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (target == other.GetComponent<Enemy>())
            {
                target = null;
            }
        }
    }

    protected virtual IEnumerator ShootRepeatily()
    {
        Shoot();
        yield return new WaitForSeconds(intervalShoot);
        if (target == null)
            isShooting = false;
        else
            StartCoroutine(ShootRepeatily());
    }
}
