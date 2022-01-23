using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    protected Enemy target;
    [SerializeField] protected float intervalShoot = 0.5f;
    [SerializeField] GameObject projectile;
    protected bool isShooting = false;
    [SerializeField] protected ParticleSystem shootSpark;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    // ABSTRACTION
    // Shoot an specified projectile (gameObject)
    protected virtual void Shoot()
    {
        shootSpark.Play();
        Instantiate(projectile, transform.position, transform.rotation);
    }

    private void Update()
    {
        AimTarget();
        if (target != null && !isShooting)
        {
            StartCoroutine(ShootRepeatily());
            isShooting = true;
        }
    }

    // ABSTRACTION
    // Look to the tarjet's position
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

    // While there is a tarjet, shoot it
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
