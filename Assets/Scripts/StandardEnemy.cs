using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : Enemy
{
    public int speed;

    private new void Awake()
    {
        
        m_speed = speed;
        SetNavMeshAgentValues();
    }
}
