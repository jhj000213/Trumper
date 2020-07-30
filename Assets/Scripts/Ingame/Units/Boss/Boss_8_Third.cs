using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_8_Third : Boss_Walk
{
    new private void Start()
    {
        UnitMng.Data.BossSummon_Pushout(transform.localPosition.x);
        _MoveSpeed = -3.0f;
        _AttackRange = 3.5f;
        _AttackDelay = 1.4f;
        _AttackDamageDelay = 0.2f;
        base.Start();
    }
}
