using UnityEngine;

public class Boss_Walk : EnemySoldier
{
    protected float _AvoidPer = 0;
    public override void HitDamage(float dmg, bool onedead = false)
    {
        if (GetStillAlive())
        {
            HitEffect();
            //SoundMng.Data.PlayEffectSound(EffectSound.Boss_Hit, 0, transform);
            if (Random.Range(0, 100) < _AvoidPer)
                dmg = 0;
            _NowHP -= dmg;
            DamageFont obj = ObjectPoolingMng.Data._DamageFontObjList[ObjectPoolingMng.Data.UseDamageFont()];

            if (dmg < 1.0f && 0 < dmg)
                obj.SetDamage(1, GetUnitHPPos() + new Vector2(0, 1), true);
            else
                obj.SetDamage((int)dmg, GetUnitHPPos() + new Vector2(0, 1), true);
            if (_NowHP <= 0.0f)
            {
                DeadUnit();
                if (_IsBoss)
                    StageMng.Data.LastBossDestroy();
            }
        }
    }
}
