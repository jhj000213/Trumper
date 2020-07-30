using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySoldier_Spear : MySoldier
{
    List<GameObject> _GarbageList = new List<GameObject>();
    [SerializeField] GameObject _SkillEffect;
    [SerializeField] GameObject _SkillEffect_New;
    private void Start()
    {
        _MoveSpeed = 1.2f;
        _AttackRange = Random.Range(2.7f, 3.3f);
        _AttackDelay = 1.5f;
        _AttackDamageDelay = 0.52f;
        
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        if (UpgradeMng.Data._SkillLevel_Soldier_Spear > 0)
        {
            if (Random.Range(0, 100) < MyUnitInfoMng.Data._Spear_SkillChance[UpgradeMng.Data._SkillLevel_Soldier_Spear - 1])
                StartCoroutine(RangeAttack());
        }
    }
    public override void DeadUnit()
    {
        for (int i = _GarbageList.Count-1; i >= 0; i--)
            Destroy(_GarbageList[i]);
        base.DeadUnit();
    }
    override public void DeadUnit_GameEnd()
    {
        for (int i = _GarbageList.Count - 1; i >= 0; i--)
            Destroy(_GarbageList[i]);
        base.DeadUnit_GameEnd();
    }

    IEnumerator RangeAttack()
    {
        yield return new WaitForSeconds(_AttackDamageDelay);

        if (_SetTarget && GetStillAlive())
        {
            //GameObject obj = Instantiate(_SkillEffect, transform.parent);
            //obj.transform.localPosition = _TargetUnit.GetUnitHitPos();
            //_GarbageList.Add(obj);

            GameObject obj1 = Instantiate(_SkillEffect_New, transform.parent);
            obj1.transform.localPosition = GetUnitHitPos()+new Vector2(0,-0.8f);
            _GarbageList.Add(obj1);

            List<Unit> hitenemy = new List<Unit>();
            for (int i = 0; i < _EnemyList.Count; i++)
            {
                if (Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) <= _AttackRange + 1)
                    hitenemy.Add(_EnemyList[i]);
            }
            float dmg = _UnitDamage;
            if (UnitMng.Data.GetHeroAceAlive())//ACE
                dmg += dmg * MyUnitInfoMng.Data._Ace_UpDmg[UpgradeMng.Data._Skill_Level_Soldier_Hero_Ace - 1];

            for (int i = 0; i < hitenemy.Count; i++)
                hitenemy[i].HitDamage(dmg*MyUnitInfoMng.Data._Spear_SkillDmg[UpgradeMng.Data._SkillLevel_Soldier_Spear - 1], false);
        }
    }
}
