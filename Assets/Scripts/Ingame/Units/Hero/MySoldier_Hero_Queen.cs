using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySoldier_Hero_Queen : Hero
{
    [SerializeField] GameObject _ShootPos;
    float _EnergeFlyingTime = 0.5f;
    float _MagicReadyTime = 0.5f;
    List<GameObject> _GarbageList = new List<GameObject>();
    float _SkillRange;

    private void Start()
    {
        _MoveSpeed = 1.2f;
        _AttackRange = 6.0f;
        _SkillRange = 10.0f;
        _AttackDelay = 1.5f;
        _AttackDamageDelay = _EnergeFlyingTime+_MagicReadyTime;
        base.Start();
    }

    protected override void Attack()
    {
        if (Random.Range(0, 100) < MyUnitInfoMng.Data._Queen_SkillChance[UpgradeMng.Data._Skill_Level_Soldier_Hero_Queen - 1])
        {
            _Animator.SetTrigger("attack");
            StartCoroutine(Skill(_MagicReadyTime));
            _NowAttackDelay = _AttackDelay;
            AchievementMng.Data.AddAchievementCount(13);
        }
        else
        {
            base.Attack();
            StartCoroutine(ShootMagic(_MagicReadyTime));
        }
    }

    IEnumerator ShootMagic(float time)
    {
        yield return new WaitForSeconds(time);

        if (_SetTarget && GetStillAlive())
        {
            MySoldier_Hero_Queen_Magic arrow = ObjectPoolingMng.Data._MySoldier_Hero_Queen_MagicsList[ObjectPoolingMng.Data.UseMySoldier_Hero_Queen_Magic()];
            Vector2 dis = _TargetUnit.GetUnitHitPos() - GetShootPos();
            arrow.Shoot(GetShootPos(), dis.x, dis.y, (Mathf.Abs(dis.x) / _AttackRange) * _EnergeFlyingTime);
        }
    }


    IEnumerator Skill(float time)
    {
        yield return new WaitForSeconds(time);

        int unitcount = 1 + UnitMng.Data.GetQueenCount();
        int count = MyUnitInfoMng.Data._Queen_SkillCount[UpgradeMng.Data._Skill_Level_Soldier_Hero_Queen - 1];
        if (count <= unitcount)
            unitcount = count;
        Debug.Log(unitcount);
        Debug.Log(count);
        List<Unit> list = new List<Unit>();
        for(int i=0;i<_EnemyList.Count && list.Count< unitcount; i++)
        {
            if (Mathf.Abs(GetUnitPos().x - _EnemyList[i].GetUnitPos().x) <= _SkillRange && _EnemyList[i].GetStillAlive())
                list.Add(_EnemyList[i]);
        }
        for (int i = 0; i < list.Count; i++)
        {
            MySoldier_Hero_Queen_Magic arrow = ObjectPoolingMng.Data._MySoldier_Hero_Queen_MagicsList[ObjectPoolingMng.Data.UseMySoldier_Hero_Queen_Magic()];
            Vector2 dis = list[i].GetUnitHitPos() - GetShootPos();
            arrow.Shoot(GetShootPos(), dis.x, dis.y, (Mathf.Abs(dis.x) / _AttackRange) * _EnergeFlyingTime);
            StartCoroutine(Attack_Delay(_EnergeFlyingTime,list[i]));
        }
    }

    IEnumerator Attack_Delay(float time,Unit target)
    {
        yield return new WaitForSeconds(time);

        if (target)
        {
            if(target.GetStillAlive())
            {
                bool onedead = false;
                float dmg = _UnitDamage;

                if (_ShapeType == 0 || SoldierShapeBuffMng.Data.GetShapeOn_Seven_Spade())
                {
                    if (Random.Range(0, 100) < SoldierShapeBuffMng.Data._SpadeData[UpgradeMng.Data._Level_Shape_Spade - 1])
                        onedead = true;
                }
                target.HitDamage(dmg, onedead);
            }
        }
    }


    Vector2 GetShootPos()
    {
        Vector2 pos = new Vector2(_ShootPos.transform.localPosition.x * transform.localScale.x,
            _ShootPos.transform.localPosition.y * transform.localScale.y);
        return GetUnitPos() + pos;
    }
}