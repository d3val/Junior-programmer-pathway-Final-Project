using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage = 10;
    public float speed = 2;
    private NavMeshAgent m_agent;
    public bool attacking = false;
    protected Objective m_tarjet;

    protected void Awake()
    {
        m_tarjet = GameObject.Find("Objective").GetComponent<Objective>();
        m_agent = GetComponent<NavMeshAgent>();

        SetNavMeshAgentValues();
        GoTo(m_tarjet.transform.position);
    }

    private void Update()
    {
        ObjectiveInRange();
    }

    protected void Attack()
    {
        if (m_tarjet != null)
        {
            m_tarjet.health -= damage;
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
        m_agent.speed = speed;

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
