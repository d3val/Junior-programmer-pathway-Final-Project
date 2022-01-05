using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected int m_health;
    protected int m_damage;
     protected float m_speed;
    protected NavMeshAgent m_agent;
    protected bool attacking = false;
    protected Objective m_tarjet;

    protected void Awake()
    {
        m_tarjet = GameObject.Find("Objective").GetComponent<Objective>();
        m_agent = GetComponent<NavMeshAgent>();

        GoTo(m_tarjet.transform.position);
    }

    protected void Update()
    {
        ObjectiveInRange();
    }

    protected void Attack()
    {
        if (m_tarjet != null)
        {
            m_tarjet.health -= m_damage;
        }
    }

    protected void ObjectiveInRange()
    {
        float distance = Vector3.Distance(m_tarjet.transform.position, transform.position);
        if (distance < 2.0f && !attacking)
        {
            attacking = true;
            StartCoroutine(AttackRepeatily());
        }
    }

    protected void SetNavMeshAgentValues()
    {
        m_agent.speed = m_speed;

    }

    protected void GoTo(Vector3 destination)
    {
        m_agent.SetDestination(destination);
    }

    IEnumerator AttackRepeatily()
    {
        Attack();
        yield return new WaitForSeconds(3);
        float distance = Vector3.Distance(m_tarjet.transform.position, transform.position);
        if (distance < 2.0f)
            StartCoroutine(AttackRepeatily());
        else
            attacking = false;

    }
}
