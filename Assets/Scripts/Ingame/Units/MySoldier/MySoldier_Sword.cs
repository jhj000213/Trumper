using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySoldier_Sword : MySoldier
{
    bool _Bursucker;
    [SerializeField] Animator _HpBarAni;

    private void Start()
    {
        _Bursucker = false;
        _MoveSpeed = 1.5f;
        _AttackRange = Random.Range(1.7f,2.3f);
        _AttackDelay = 1.2f;
        _AttackDamageDelay = 0.4f;
        base.Start();
    }
    new protected void Update()
    {
        base.Update();
        if (!GetStillAlive())
            return;
        if (_Bursucker)
        {
            _NowHP = 0.0f;

        }
    }

    public override void HitDamage(float dmg, bool onedead = false)
    {
        if (GetStillAlive())
        {
            //SoundMng.Data.PlayEffectSound(EffectSound.MySoldier_Hit, 0, transform);
            HitEffect();
            if (_ShapeType == 3 || SoldierShapeBuffMng.Data.GetShapeOn_Seven_Club())
            {
                if (Random.Range(0, 100) < SoldierShapeBuffMng.Data._ClubData[UpgradeMng.Data._Level_Shape_Club - 1])
                    dmg = 0;
            }
            _NowHP -= dmg;
            DamageFont obj = ObjectPoolingMng.Data._DamageFontObjList[ObjectPoolingMng.Data.UseDamageFont()];
            obj.SetDamage((int)dmg, GetUnitHPPos(),false);
            if (_NowHP <= 0.0f && !_Bursucker)
                Bursucker();
        }
    }

    void Bursucker()
    {
        if (UpgradeMng.Data._SkillLevel_Soldier_Sword > 0)
        {
            if (Random.Range(0, 100) < MyUnitInfoMng.Data._Sword_SkillChance[UpgradeMng.Data._SkillLevel_Soldier_Sword - 1])
            {
                _HpBarAni.SetTrigger("bursucker");
                _HpBarAni.speed = 1 / MyUnitInfoMng.Data._Sword_SkillTime[UpgradeMng.Data._SkillLevel_Soldier_Sword - 1];
                _Bursucker = true;
                StartCoroutine(BursuckerDown(MyUnitInfoMng.Data._Sword_SkillTime[UpgradeMng.Data._SkillLevel_Soldier_Sword - 1]));
                _UnitDamage *= 1.2f;
                _AttackDelay *= 0.6f;
                _NowAttackDelay = 0.0f;
                return;
            }
        }
        DeadUnit();
    }

    IEnumerator BursuckerDown(float time)
    {
        yield return new WaitForSeconds(time);
        
        DeadUnit();
    }
}
