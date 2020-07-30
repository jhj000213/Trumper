using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
    [SerializeField] SpriteRenderer[] _FrontHandFoot;
    [SerializeField] SpriteRenderer[] _BackHandFoot;
    [SerializeField] TrailRenderer[] _TrailRenderer;

    public List<Unit> _EnemyList;
    protected float _MoveSpeed;

    [SerializeField] protected int _AttackCount;

    protected bool _SetTarget = false;
    protected Unit _TargetUnit;


    protected float _AttackRange;
    protected float _AttackDelay;
    protected float _NowAttackDelay = 0.0f;

    protected float _AttackDamageDelay;
    protected float _UnitDamage;

    const float _HitBackPos = 0.1f;

    new protected void Start()
    {
        base.Start();
        for(int i=0;i<_FrontHandFoot.Length;i++)
            _FrontHandFoot[i].sortingOrder = _LayerOrder + (i*2+1);
        for (int i = 0; i < _BackHandFoot.Length; i++)
            _BackHandFoot[i].sortingOrder = _LayerOrder - ((i+1)*2);

        for (int i = 0; i < _FrontHandFoot_Hit.Length; i++)
            _FrontHandFoot_Hit[i].sortingOrder = _LayerOrder + (i*2 + 2);
        for (int i = 0; i < _BackHandFoot_Hit.Length; i++)
            _BackHandFoot_Hit[i].sortingOrder = _LayerOrder - (i*2+1);

        for (int i = 0; i < _TrailRenderer.Length; i++)
            _TrailRenderer[i].sortingOrder = _LayerOrder + 10;
    }

    new protected void Update()
    {
        base.Update();
        if (!GetStillAlive())
            return;
        if (_SetTarget)
        {
            if (!TargetEnemyNullCheck())
            {
                _SetTarget = false;
                _TargetUnit = null;
            }
            else if (Mathf.Abs(_TargetUnit.GetUnitPos().x - GetUnitPos().x) >
                _AttackRange+0.6f || !_TargetUnit.GetStillAlive())
            {
                _SetTarget = false;
                _TargetUnit = null;
            }
        }
    }

    protected bool TargetEnemyNullCheck()
    {
        return _TargetUnit;

    }

    protected bool FindEnemy()
    {
        if (!_SetTarget)
        {
            for (int i = 0; i < _EnemyList.Count; i++)
            {
                if (Mathf.Abs(_EnemyList[i].GetUnitPos().x- GetUnitPos().x) 
                    < _AttackRange && _EnemyList[i].GetStillAlive())
                {
                    _TargetUnit = _EnemyList[i];
                    _SetTarget = true;
                    break;
                }
            }
        }
        return _SetTarget;
    }

    protected virtual void Attack()
    {
        if(_AttackCount<=1)
            _Animator.SetTrigger("attack");
        else
        {
            int r = Random.Range(0, _AttackCount) + 1;
            _Animator.SetTrigger("attack" + r.ToString());
        }
        if (UnitMng.Data.GetHeroAceAlive() && !IsEnemy())
            _NowAttackDelay = (_AttackDelay * MyUnitInfoMng.Data._Ace_UpDelay
                [UpgradeMng.Data._Skill_Level_Soldier_Hero_Ace-1]);
        else
            _NowAttackDelay = _AttackDelay;

        AttackSound();
    }
    void AttackSound()
    {
        if (GetUnitName() == "mysoldier_sword")
            SoundMng.Data.PlayEffectSound(EffectSound.Sword_Attack, 0.4f, transform);
        else if (GetUnitName() == "mysoldier_archer")
            SoundMng.Data.PlayEffectSound(EffectSound.Archer_Attack, 0.8888f, transform);
        else if (GetUnitName() == "mysoldier_shield")
            SoundMng.Data.PlayEffectSound(EffectSound.Shield_Attack, 0.33333f, transform);
        else if (GetUnitName() == "mysoldier_spear")
            SoundMng.Data.PlayEffectSound(EffectSound.Spear_Attack, 0.5f, transform);
        else if (GetUnitName() == "mysoldier_seven")
            SoundMng.Data.PlayEffectSound(EffectSound.Seven_Attack, 0.45f, transform);
        else if (GetUnitName() == "mysoldier_hero_ace")
            SoundMng.Data.PlayEffectSound(EffectSound.Ace_Attack, 0.28f, transform);
        else if (GetUnitName() == "mysoldier_hero_jack")
        {
            SoundMng.Data.PlayEffectSound(EffectSound.Jack_Attack, 0.352f, transform);
            SoundMng.Data.PlayEffectSound(EffectSound.Jack_Attack, 0.5f, transform);
        }
        else if (GetUnitName() == "mysoldier_hero_queen")
            SoundMng.Data.PlayEffectSound(EffectSound.Queen_Attack, 0.5f, transform);
        else if (GetUnitName() == "mysoldier_hero_king")
            SoundMng.Data.PlayEffectSound(EffectSound.King_Attack, 0.28f, transform);
        else if (GetUnitName() == "enemysoldier_woodsword")
            SoundMng.Data.PlayEffectSound(EffectSound.WoodSword_Attack, 0.4f, transform);
        else if (GetUnitName() == "enemysoldier_katana" || GetUnitName() == "enemysoldier_gsword")
            SoundMng.Data.PlayEffectSound(EffectSound.Katana_Attack, 0.5f, transform);
        else if (GetUnitName() == "enemysoldier_spear" || GetUnitName() == "enemysoldier_gspear")
            SoundMng.Data.PlayEffectSound(EffectSound.Spear_Attack, 0.4f, transform);
        else if (GetUnitName() == "enemysoldier_arrow")
            SoundMng.Data.PlayEffectSound(EffectSound.Archer_Attack, 1.15f, transform);
        else if (GetUnitName() == "enemysoldier_rifle")
            SoundMng.Data.PlayEffectSound(EffectSound.Rifle_Attack, 0.848f, transform);
        else if (GetUnitName() == "boss_1")
            SoundMng.Data.PlayEffectSound(EffectSound.Boss1_Attack, 0.333f, transform);
        else if (GetUnitName() == "boss_2")
            SoundMng.Data.PlayEffectSound(EffectSound.Boss2_Attack, 0.192f, transform);
        else if (GetUnitName() == "boss_4")
            SoundMng.Data.PlayEffectSound(EffectSound.Boss4_Attack, 0.112f, transform);
        else if (GetUnitName() == "boss_7")
            SoundMng.Data.PlayEffectSound(EffectSound.Boss7_Attack, 0.25f, transform);
        else if (GetUnitName() == "boss_8_first"|| GetUnitName() == "boss_8_second"|| GetUnitName() == "boss_8_third")
            SoundMng.Data.PlayEffectSound(EffectSound.Boss8_Attack, 0.112f, transform);
        else if (GetUnitName() == "boss_12_toad")
            SoundMng.Data.PlayEffectSound(EffectSound.Boss12_Toad_Attack, 0.336f, transform);
        else if (GetUnitName() == "boss_12_bird")
            SoundMng.Data.PlayEffectSound(EffectSound.Boss12_Bird_Attack, 0.064f, transform);
        else if (GetUnitName() == "boss_12_Human")
            SoundMng.Data.PlayEffectSound(EffectSound.Boss12_Human_Attack, 0.384f, transform);
    }

    protected override void HitEffect()
    {
        base.HitEffect();
        if (!GetStillAlive())
            return;
        if (_HitBackTime <= 0)
        {
            _HitBackTime = 1.5f;
            if(_MoveSpeed>0)
                transform.localPosition -= new Vector3(_HitBackPos, 0,0);
            else
                transform.localPosition += new Vector3(_HitBackPos, 0,0);
        }
    }
}
