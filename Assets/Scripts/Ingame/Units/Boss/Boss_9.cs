using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_9 : Boss_Castle
{
    [SerializeField] SpriteRenderer _SkillGaze;
    float _SkillTimer = 0;
    const float _SkillCoolTime = 0.5f;
    const float _SkillRange = 55.0f;
    const float _SkillDmgPer = 0.01f;

    float _DeadTimer = 0;
    const float _DeadTimeMax = 200;

    new protected void Update()
    {
        base.Update();
        _DeadTimer += Time.smoothDeltaTime;
        _SkillTimer += Time.smoothDeltaTime;
        _SkillGaze.size = new Vector2(_SkillTimer / _SkillCoolTime, 1);
        _SkillGaze.size = new Vector2(_DeadTimer / _DeadTimeMax, 1);
        if (_DeadTimer >= _DeadTimeMax)
        {
            Unit castle = _EnemyList[0];
            for (int i = 0; i < 100; i++)
            {
                if (!castle.GetStillAlive())
                    break;
                _EnemyList[0].HitDamage(1);
            }
        }
        if (_SkillTimer >= _SkillCoolTime)
        {
            _SkillTimer = 0;
            UseSkill();
        }
    }

    void UseSkill()
    {
        List<Unit> hitenemy = new List<Unit>();
        for (int i = 1; i < _EnemyList.Count; i++)
        {
            if (Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) < _SkillRange)
                hitenemy.Add(_EnemyList[i]);
        }
        for (int i = 0; i < hitenemy.Count; i++)
            hitenemy[i].HitDamage(_Dmg * _SkillDmgPer);
    }
}
