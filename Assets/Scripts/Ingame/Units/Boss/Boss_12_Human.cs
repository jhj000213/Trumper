using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_12_Human : Boss_Walk
{
    [SerializeField] GameObject _Bird_2;
    [SerializeField] GameObject _Bird_4;
    [SerializeField] GameObject _Bird_8;
    [SerializeField] GameObject _Bird_12;
    [SerializeField] GameObject _Pig;
    [SerializeField] GameObject _Giraffe;
    [SerializeField] GameObject _Toad;
    [SerializeField] SpriteRenderer _SkillGaze;
    float _SkillTimer = 0;
    const float _SkillCoolTime = 12.0f;
    const float _BossHpPer = 0.4f;
    const float _ToadHpPer = 0.2f;

    float _UnitSummonPosX;

    new private void Start()
    {
        _UnitSummonPosX = transform.localPosition.x;
        _MoveSpeed = -3.0f;
        _AttackRange = 7.0f;
        _AttackDelay = 0.7f;
        _AttackDamageDelay = 0.2f;
        _SkillTimer = 12.0f;//
        base.Start();
    }

    new protected void Update()
    {
        base.Update();
        _SkillTimer += Time.smoothDeltaTime;
        _SkillGaze.size = new Vector2(_SkillTimer / _SkillCoolTime, 1);
        if (_SkillTimer >= _SkillCoolTime)
        {
            _SkillTimer = 0;
            UseSkill();
        }
    }

    void UseSkill()
    {
        UnitMng.Data.BossSummon_Pushout(transform.localPosition.x);
        GameObject toad = Instantiate(_Toad, transform.parent);
        toad.transform.localPosition = transform.localPosition + new Vector3(-1, 0, 0);
        UnitMng.Data.AddEnemyUnit(toad.GetComponent<Boss_12_Toad>(), _MaxHP * _ToadHpPer, _UnitDamage, "boss_12_toad");

        GameObject obj = null;
        int r = Random.Range(0, 6);
        switch (r)
        {
            case 0:
                obj = Instantiate(_Bird_2, transform.parent);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_2>(), _MaxHP * _BossHpPer, _UnitDamage, "boss_2", false);
                break;
            case 1:
                obj = Instantiate(_Bird_4, transform.parent);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_4>(), _MaxHP * _BossHpPer, _UnitDamage, "boss_4", false);
                break;
            case 2:
                obj = Instantiate(_Bird_8, transform.parent);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_8_First>(), _MaxHP * _BossHpPer, _UnitDamage, "boss_8_first", false);
                break;
            case 3:
                obj = Instantiate(_Pig, transform.parent);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_7>(), _MaxHP * _BossHpPer, _UnitDamage, "boss_7", false);
                break;
            case 4:
                obj = Instantiate(_Giraffe, transform.parent);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_10>(), _MaxHP * _BossHpPer, _UnitDamage, "boss_10", false);
                break;
            case 5:
                obj = Instantiate(_Bird_12, transform.parent);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_12_Bird>(), _MaxHP * _BossHpPer, _UnitDamage, "boss_12_bird", false);
                break;
            case 6:
                Debug.Log("RangeError_boss12");
                break;
        }
        //obj.transform.localPosition = transform.localPosition;
        obj.transform.localPosition = new Vector3(_UnitSummonPosX, Random.Range(1.45f, 3.0f));
    }
}