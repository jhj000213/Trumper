using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_7 : Boss_Walk
{
    new private void Start()
    {
        _MoveSpeed = -7.0f;
        _AttackRange = 4.0f;
        _AttackDelay = 0.7f;
        _AttackDamageDelay = 0.2f;
        base.Start();
    }
}
