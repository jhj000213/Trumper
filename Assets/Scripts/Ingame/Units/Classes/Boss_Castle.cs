using System.Collections.Generic;
using UnityEngine;

public class Boss_Castle : Castle
{
    protected List<Unit> _EnemyList;
    protected float _Dmg;
    override public void Init(float hp, float dmg, string unitname, List<Unit> enemylist, List<Unit> deadlist,bool nonboss,int n)
    {
        _MaxHP = hp;
        _Dmg = dmg;
        _UnitName = unitname;
        _EnemyList = enemylist;
        _DeadList = deadlist;
    }
    public override void HitDamage(float dmg, bool onedead = false)
    {
        if (GetStillAlive())
        {
            HitEffect();
            SoundMng.Data.PlayEffectSound(EffectSound.Castle_Hit, 0, transform);
            _NowHP -= dmg;
            DamageFont obj = ObjectPoolingMng.Data._DamageFontObjList[ObjectPoolingMng.Data.UseDamageFont()];
            obj.SetDamage((int)dmg, GetUnitHPPos() + new Vector2(0, 1),true);
            if (_NowHP <= 0.0f)
            {
                DeadUnit();
                StageMng.Data.LastBossDestroy();
            }
        }
    }
}
