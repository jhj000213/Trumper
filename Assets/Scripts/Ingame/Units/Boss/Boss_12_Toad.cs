using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_12_Toad : EnemySoldier
{
    new private void Start()
    {
        _MoveSpeed = -6.0f;
        _AttackRange = 3.0f;
        _AttackDelay = 0.7f;
        _AttackDamageDelay = 0.2f;
        base.Start();
    }
}
