using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyProjectile : Proyectile
{
    private SphereCollider sphereCollider;
    [SerializeField] float explodeTime;
    private MeshRenderer meshRenderer;
    private bool isExploding = false;

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

    protected override void MoveForward()
    {
        if (!isExploding)
            base.MoveForward();
    }

    IEnumerator Explode()
    {
        isExploding = true;
        meshRenderer.enabled = false;
        while (explodeTime > 0)
        {
            explodeTime -= Time.deltaTime;
            sphereCollider.radius *= 1.01f;
            yield return null;
        }
        Destroy(gameObject);
    }
}
