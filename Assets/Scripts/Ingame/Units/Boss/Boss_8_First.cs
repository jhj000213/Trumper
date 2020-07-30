using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_8_First : EnemySoldier
{
    [SerializeField] GameObject _SecondBird;

    new private void Start()
    {
        _MoveSpeed = -3.0f;
        _AttackRange = 3.5f;
        _AttackDelay = 1.4f;
        _AttackDamageDelay = 0.2f;
        base.Start();
    }

    public override void DeadUnit()
    {
        base.DeadUnit();
        MakeSecond();
    }
    void MakeSecond()
    {
        GameObject obj = Instantiate(_SecondBird, transform.parent);
        obj.transform.localPosition = transform.localPosition;
        UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_8_Second>(), _MaxHP, _UnitDamage, "boss_8_second",_IsBoss);
    }
}
