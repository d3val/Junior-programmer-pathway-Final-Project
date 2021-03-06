using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class HeavyProjectile : Proyectile
{
    private SphereCollider sphereCollider;
    [SerializeField] float explosionTime;
    private MeshRenderer meshRenderer;
    private bool isExploding = false;
    public ParticleSystem explosion;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Ground") || other.CompareTag("Enemy")) && !isExploding)
        {
            StartCoroutine(Explode());
        }
    }

    // POLYMORPHISM
    protected override void MoveForward()
    {
        if (!isExploding)
            base.MoveForward();
    }

    // Explodes and increses size collider before being destroyed
    IEnumerator Explode()
    {
        explosion.Play();
        isExploding = true;
        meshRenderer.enabled = false;
        sphereCollider.radius *= 5f;
        yield return new WaitForSeconds(explosionTime);
        Destroy(gameObject);
    }
}
