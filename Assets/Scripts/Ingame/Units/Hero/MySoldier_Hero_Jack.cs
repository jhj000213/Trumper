using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySoldier_Hero_Jack : Hero
{
    private void Start()
    {
        _MoveSpeed = 1.8f;
        _AttackRange = 1.8f;
        _AttackDelay = 1.2f;
        _AttackDamageDelay = 0.38f;
        base.Start();
    }
}
