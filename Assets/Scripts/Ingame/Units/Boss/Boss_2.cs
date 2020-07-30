using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2 : Boss_Walk
{
    [SerializeField] GameObject _Trap;
    [SerializeField] SpriteRenderer _SkillGaze;
    public List<GameObject> _GarbageList = new List<GameObject>();

    float _TrapTimer;
    const float _TrapCoolTime = 5.0f;
    new private void Start()
    {
        _MoveSpeed = -3.0f;
        _AttackRange = 3.0f;
        _AttackDelay = 0.7f;
        _AttackDamageDelay = 0.2f;
        base.Start();
    }

    new private void Update()
    {
        base.Update();
        _TrapTimer += Time.smoothDeltaTime;
        _SkillGaze.size = new Vector2(_TrapTimer / _TrapCoolTime, 1);
        if (_TrapTimer>=_TrapCoolTime)
        {
            _TrapTimer = 0;
            MakeTrap();
        }
    }

    void MakeTrap()
    {
        if (!_Alive)
            return;
        Unit target = _EnemyList[0];
        for (int i = 0; i < _EnemyList.Count; i++)
        {
            if (_EnemyList[i].GetUnitPos().x > target.GetUnitPos().x)
                target = _EnemyList[i];
        }

        GameObject obj = Instantiate(_Trap, transform.parent);
        obj.transform.localPosition = GetUnitHitPos();
        obj.GetComponent<Boss_2_Trap>().Init(_UnitDamage, target.GetUnitPos(), _EnemyList);
        _GarbageList.Add(obj);
    }

    public override void DeadUnit()
    {
        for (int i = _GarbageList.Count - 1; i >= 0; i--)
            Destroy(_GarbageList[i]);
        base.DeadUnit();
    }
    override public void DeadUnit_GameEnd()
    {
        for (int i = _GarbageList.Count - 1; i >= 0; i--)
            Destroy(_GarbageList[i]);
        base.DeadUnit_GameEnd();
    }
}
