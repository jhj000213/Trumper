using System.Collections.Generic;
using UnityEngine;

public class Castle : Unit
{
    protected bool _MyCastle;
    override public void Init(List<Unit> deadlist)
    {
        _DeadList = deadlist;
        _UnitName = "castle";
    }

    public override void HitDamage(float dmg, bool onedead = false)
    {
        dmg = 1;
        HitEffect();
        if (GetStillAlive())
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Castle_Hit, 0, transform);
            _NowHP -= dmg;
            DamageFont obj = ObjectPoolingMng.Data._DamageFontObjList[ObjectPoolingMng.Data.UseDamageFont()];
            obj.SetDamage((int)dmg, GetUnitHPPos() + new Vector2(0, 1), IsEnemy());
            if (_NowHP < 1)
            {
                DeadUnit();
                StageMng.Data.CastleDestroy(!_MyCastle);
            }
        }
    }
}
