using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_12_Bird : Boss_Walk
{
    [SerializeField] GameObject _Bird_2;
    [SerializeField] GameObject _Bird_4;
    [SerializeField] GameObject _Bird_8;
    [SerializeField] SpriteRenderer _SkillGaze;
    float _SkillTimer = 0;
    const float _SkillCoolTime = 4.5f;
    const float _BirdHpPer = 0.4f;

    new private void Start()
    {
        _MoveSpeed = -3.0f;
        _AttackRange = 4.0f;
        _AttackDelay = 1.1f;
        _AttackDamageDelay = 0.2f;
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
            MakeBird();
        }
    }

    void MakeBird()
    {
        GameObject obj;
        int r = Random.Range(0, 3);
        switch(r)
        {
            case 0:
                obj = Instantiate(_Bird_2, transform.parent);
                obj.transform.localPosition = transform.localPosition;
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_2>(), _MaxHP * _BirdHpPer, _UnitDamage, "boss_2", false);
                break;
            case 1:
                obj = Instantiate(_Bird_4, transform.parent);
                obj.transform.localPosition = transform.localPosition;
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_4>(), _MaxHP * _BirdHpPer, _UnitDamage, "boss_4", false);
                break;
            case 2:
                obj = Instantiate(_Bird_8, transform.parent);
                obj.transform.localPosition = transform.localPosition;
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_8_First>(), _MaxHP * _BirdHpPer, _UnitDamage, "boss_8", false);
                break;
            case 3:
                Debug.Log("RangeError_boss11");
                break;
        }
    }
}
