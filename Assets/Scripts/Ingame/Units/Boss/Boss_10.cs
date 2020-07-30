using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_10 : Boss_Walk
{
    float _SkillTimer = 0;
    const float _SkillCoolTime = 1.0f;

    const float _SkillRange_Left_1 = 9.0f;
    const float _SkillRange_Right_1 = 6.0f;
    const float _SkillRange_Left_2 = 4.0f;
    const float _SkillRange_Right_2 = 0;
    const float _SkillDmgPer = 0.3f;

    new private void Start()
    {
        _MoveSpeed = -3.0f;
        _AttackRange = 1.5f;
        _AttackDelay = 0.1f;
        _AttackDamageDelay = 0.0f;
        base.Start();
    }

    protected override void Attack()
    {
        _Animator.SetTrigger("attack");
        _NowAttackDelay = _AttackDelay;
    }

    new private void Update()
    {
        base.Update();

        _SkillTimer += Time.smoothDeltaTime;
        if(_SkillTimer>=_SkillCoolTime)
        {
            _SkillTimer = 0;
            HitCircle_1();
            HitCircle_2();
        }
    }

    void HitCircle_1()
    {
        List<Unit> hitenemy = new List<Unit>();
        for (int i = 0; i < _EnemyList.Count; i++)
        {
            if (Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) <= _SkillRange_Left_1 &&
                Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) >= _SkillRange_Right_1)
                hitenemy.Add(_EnemyList[i]);
        }
        for (int i = 0; i < hitenemy.Count; i++)
            hitenemy[i].HitDamage(_UnitDamage * _SkillDmgPer);
    }
    void HitCircle_2()
    {
        List<Unit> hitenemy = new List<Unit>();
        for (int i = 0; i < _EnemyList.Count; i++)
        {
            if (Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) <= _SkillRange_Left_2 &&
                Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) >= _SkillRange_Right_2)
                hitenemy.Add(_EnemyList[i]);
        }
        for (int i = 0; i < hitenemy.Count; i++)
            hitenemy[i].HitDamage(_UnitDamage * _SkillDmgPer);
    }
}
