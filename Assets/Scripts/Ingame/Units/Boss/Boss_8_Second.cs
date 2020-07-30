using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_8_Second : EnemySoldier
{
    [SerializeField] GameObject _ThirdBird;

    new private void Start()
    {
        UnitMng.Data.BossSummon_Pushout(transform.localPosition.x);
        _MoveSpeed = -3.0f;
        _AttackRange = 3.5f;
        _AttackDelay = 1.4f;
        _AttackDamageDelay = 0.2f;
        base.Start();
    }
    public override void DeadUnit()
    {
        base.DeadUnit();
        MakeThird();
    }
    void MakeThird()
    {
        GameObject obj = Instantiate(_ThirdBird, transform.parent);
        obj.transform.localPosition = transform.localPosition;
        UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_8_Third>(), _MaxHP, _UnitDamage, "boss_8_third", _IsBoss);
    }
}
