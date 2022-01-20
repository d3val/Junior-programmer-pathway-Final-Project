using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 10;
    public float damage = 10;
    public float speed = 2;
    private NavMeshAgent m_agent;
    private bool attacking = false;
    protected Objective m_tarjet;
    private float distanceAttack = 1f;

    protected void Awake()
    {
        m_tarjet = GameObject.Find("Objective").GetComponent<Objective>();
        m_agent = GetComponent<NavMeshAgent>();
        CheckDifficulty();

        SetNavMeshAgentValues();
        GoTo(m_tarjet);
    }

    private void Update()
    {
        Die();
        ObjectiveInRange();
    }

    private void CheckDifficulty()
    {
        if (SettingsManager.instance != null)
        {
            if (SettingsManager.instance.difficulty == SettingsManager.Hard)
                health *= 1.25f;
            else if (SettingsManager.instance.difficulty == SettingsManager.Easy)
                health /= 1.25f;
        }
    }

    protected void Attack()
    {
        if (m_tarjet != null)
        {
            m_tarjet.TakeDamage(damage);
        }
    }

    // Check if the enemy is near to the objective
    protected void ObjectiveInRange()
    {
        float distance = Vector3.Distance(m_tarjet.transform.position, transform.position);
        if (distance < distanceAttack && !attacking)
        {
            attacking = true;
            StartCoroutine(AttackRepeatily());
        }
    }

    protected void SetNavMeshAgentValues()
    {
        m_agent.speed = speed;
    }

    public virtual void GoTo(Vector3 destination)
    {
        m_agent.SetDestination(destination);
    }

    public virtual void GoTo(Objective objective)
    {
        m_agent.SetDestination(objective.transform.position);
        m_agent.speed = speed;
    }

    // Check the current enemy life.
    protected void Die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Stop()
    {
        m_agent.speed = 0;
    }

    // Corroutine for attack repeatily by time intervals
    IEnumerator AttackRepeatily()
    {
        Attack();
        yield return new WaitForSeconds(3);
        float distance = Vector3.Distance(m_tarjet.transform.position, transform.position);
        if (distance < distanceAttack)
            StartCoroutine(AttackRepeatily());
        else
            attacking = false;

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heavy Projectile"))
        {
            Proyectile projectile = other.GetComponent<Proyectile>();

            TakeDamage(projectile.damage);
        }
        else if (other.CompareTag("Projectile"))
        {
            Proyectile projectile = other.GetComponent<Proyectile>();

            TakeDamage(projectile.damage);
            Destroy(other.gameObject);
        }
    }
}
