using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_6_Butterfly : MonoBehaviour
{
    [SerializeField] GameObject _Body;

    Unit _TargetUnit;
    float _SpeedTime;
    float _Speed;
    List<Unit> _EnemyList = new List<Unit>();
    float _Dmg;
    bool _On;

    const float _SkillRange = 2.0f;

    public void Init(Unit target,List<Unit> enemy,float dmg)
    {
        _TargetUnit = target;
        _Speed = 4.0f;
        _EnemyList = enemy;
        _Dmg = dmg;
        _On = true;
        transform.localScale = new Vector3(0.5f, 0.5f, 1);
    }

    private void Update()
    {
        if (_On && _TargetUnit)
        {
            _SpeedTime += Time.smoothDeltaTime;
            if (_SpeedTime >= 0.5f)
            {
                _SpeedTime -= 0.5f;
                _Speed += 0.5f;
            }

            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1.5f, 1.5f, 1),Time.smoothDeltaTime*0.25f);
            transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(_TargetUnit.GetUnitHitPos().y - transform.localPosition.y, _TargetUnit.GetUnitHitPos().x - transform.localPosition.x) * Mathf.Rad2Deg);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _TargetUnit.GetUnitHitPos(), _Speed * Time.smoothDeltaTime);
            if (Vector3.Distance(transform.localPosition, _TargetUnit.GetUnitHitPos()) < 0.1f)
                Boom();
        }
        else
        {
            TargetChange();
        }
    }

    void TargetChange()
    {
        for(int i=_EnemyList.Count-1;i>=0;i++)
        {
            if (_EnemyList[i])
            {
                _TargetUnit = _EnemyList[i];
                break;
            }
        }
    }

    void Boom()
    {
        SoundMng.Data.PlayEffectSound(EffectSound.Boss6_ButterflyBoom, 1,transform);
        List<Unit> hitenemy = new List<Unit>();
        for (int i = 0; i < _EnemyList.Count; i++)
        {
            if (Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) < _SkillRange)
                hitenemy.Add(_EnemyList[i]);
        }
        for (int i = 0; i < hitenemy.Count; i++)
            hitenemy[i].HitDamage(_Dmg);

        _On = false;
        gameObject.SetActive(false);
    }
}
