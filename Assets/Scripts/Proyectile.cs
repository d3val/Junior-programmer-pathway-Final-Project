using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    protected float lifeTime = 5.0f;
    [SerializeField] protected float speed = 10;
    public float damage = 2;

    // Update is called once per frame
    void Update()
    {
        if (lifeTime <= 0)
            Destroy(gameObject);
        lifeTime -= Time.deltaTime;
        MoveForward();
    }

    // ABSTRACTION
    protected virtual void MoveForward()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
    }
}
