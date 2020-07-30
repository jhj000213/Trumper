using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : Soldier
{
    protected bool _IsBoss;
    protected int _BodyMonth;
    override public void Init(float hp, float dmg, string unitname, List<Unit> enemylist, List<Unit> deadlist,bool isboss,int maxmonth)
    {
        _MaxHP = hp;
        _UnitDamage = dmg;
        _UnitName = unitname;
        _EnemyList = enemylist;
        _DeadList = deadlist;
        _IsBoss = isboss;
        _BodyMonth = Random.Range(0,maxmonth)+1;

        if (unitname[0] != 'b')
        {
            _BodySprite.sprite = UnitMng.Data._EnemyBodyAtlas.GetSprite("ebody_" + _BodyMonth);
            if (_BodySprite_Hit)
                _BodySprite_Hit.sprite = UnitMng.Data._EnemyBodyAtlas.GetSprite("ebody_" + _BodyMonth);
        }
    }

    new protected void Update()
    {
        base.Update();
        if (!GetStillAlive())
            return;
        _NowAttackDelay -= Time.smoothDeltaTime;
        if (_SetTarget)
        {
            if (!_TargetUnit.GetStillAlive())
                _SetTarget = false;
            else
            {
                if (_NowAttackDelay <= 0.0f)
                    Attack();
            }
        }
        else
        {
            if (_NowAttackDelay <= 0.0f)
            {
                transform.localPosition += new Vector3(_MoveSpeed * Time.smoothDeltaTime, 0, 0);
                _Animator.Play(_UnitName + "_walk");
                //_Animator.SetTrigger("walk");
            }
            FindEnemy();
        }
    }

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(Attack_Delay(_AttackDamageDelay));
    }

    IEnumerator Attack_Delay(float time)
    {
        yield return new WaitForSeconds(time);

        if (_SetTarget)
            _TargetUnit.HitDamage(_UnitDamage,false);
    }
}
