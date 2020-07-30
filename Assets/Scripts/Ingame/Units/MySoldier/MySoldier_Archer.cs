using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySoldier_Archer : MySoldier
{
    [SerializeField] GameObject _ShootPos;
    float _ArrowFlyingTime = 0.5f;
    float _DrowArrowTime = 0.888888f;
    List<GameObject> _GarbageList = new List<GameObject>();
    [SerializeField] GameObject _ArrowRain_Castle;
    [SerializeField] GameObject _ArrowRain;

    private void Start()
    {
        _MoveSpeed = 1.275f;
        _AttackRange = Random.Range(6.5f, 7.5f);
        _AttackDelay = 1.6f;
        _AttackDamageDelay = _ArrowFlyingTime + _DrowArrowTime;
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(CreateArrow(_DrowArrowTime));
    }

    IEnumerator CreateArrow(float time)
    {
        yield return new WaitForSeconds(time);
        
        if(_SetTarget && GetStillAlive())
        {
            MySoldier_Archer_Arrow arrow = ObjectPoolingMng.Data._MySoldier_Archer_ArrowsList[ObjectPoolingMng.Data.UseMySoldier_Archer_Arrow()];
            Vector2 dis = _TargetUnit.GetUnitHitPos() - GetShootPos();
            arrow.Shoot(GetShootPos(), dis.x, dis.y, (Mathf.Abs(dis.x) / _AttackRange) * _ArrowFlyingTime);
            if (UpgradeMng.Data._SkillLevel_Soldier_Archer > 0)
            {
                if (Random.Range(0, 100) < MyUnitInfoMng.Data._Archer_SkillChance[UpgradeMng.Data._SkillLevel_Soldier_Archer - 1])
                    ArrowRain();
            }
        }

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

    void ArrowRain()
    {
        float pos = _TargetUnit.GetUnitPos().x + Random.Range(2.0f,4.0f);
        GameObject obj = Instantiate(_ArrowRain_Castle, transform.parent);
        obj.transform.localPosition = new Vector3(-8, 1.2f, 0);
        GameObject obj1 = Instantiate(_ArrowRain, transform.parent);
        obj1.transform.localPosition = new Vector3(pos, 2.6f, 0);
        _GarbageList.Add(obj);
        _GarbageList.Add(obj1);
        StartCoroutine(ArrowRainHit(1.5f,pos));
        StartCoroutine(ArrowRainHit(1.7f,pos));
        StartCoroutine(ArrowRainHit(1.9f,pos));
        StartCoroutine(ArrowRainHit(2.1f,pos));
    }

    IEnumerator ArrowRainHit(float time,float posx)
    {
        yield return new WaitForSeconds(time);

        List<Unit> hitenemy = new List<Unit>();
        for (int i = 0; i < _EnemyList.Count; i++)
        {
            if (Mathf.Abs(posx - _EnemyList[i].GetUnitPos().x) < 3.0f)
                hitenemy.Add(_EnemyList[i]);
        }
        float dmg = _UnitDamage;
        if (UnitMng.Data.GetHeroAceAlive())//ACE
            dmg += dmg * MyUnitInfoMng.Data._Ace_UpDmg[UpgradeMng.Data._Skill_Level_Soldier_Hero_Ace - 1];

        for (int i = 0; i < hitenemy.Count; i++)
            hitenemy[i].HitDamage(dmg* MyUnitInfoMng.Data._Archer_SkillDmg[UpgradeMng.Data._SkillLevel_Soldier_Archer - 1]);
    }

    Vector2 GetShootPos()
    {
        Vector2 pos = new Vector2(_ShootPos.transform.localPosition.x * transform.localScale.x,
            _ShootPos.transform.localPosition.y * transform.localScale.y);
        return GetUnitPos() + pos;
    }
}
