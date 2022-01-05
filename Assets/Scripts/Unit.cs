using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Enemy target;
    public float intervalShoot = 0.5f;
    public GameObject projectile;
    private bool shooting = false;

    private void Start()
    {
    }

    protected void Shoot()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }

    private void Update()
    {
        if (target != null && !shooting)
            StartCoroutine(ShootRepeatily());
    }

    private void OnTriggerStay(Collider other)
    {
        // Establecer el objetivo en cuanto entre al area de vision

        Debug.Log(other.tag);
        if (other.CompareTag("Enemy"))
        {
            if (target == null)
            {
                target = other.GetComponent<Enemy>();
                shooting = true;
            }
        }
    }

    IEnumerator ShootRepeatily()
    {
        Debug.Log("Fuego");
        Shoot();
        yield return new WaitForSeconds(3);
        if (target == null)
            shooting = false;
        else
            StartCoroutine(ShootRepeatily());
    }
}
