﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier_Arrow : EnemySoldier
{
    [SerializeField] GameObject _ShootPos;
    float _BulletFlyingTime = 0.5f;
    float _DrowBulletTime = 1.33333f;
    private void Start()
    {
        _MoveSpeed = -1.275f;
        _AttackRange = Random.Range(6.0f, 7.5f); ;
        _AttackDelay = 2.0f;
        _AttackDamageDelay = _BulletFlyingTime + _DrowBulletTime-0.15f;
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(CreateArrow(_DrowBulletTime));
    }

    IEnumerator CreateArrow(float time)
    {
        yield return new WaitForSeconds(time);

        if (_SetTarget)
        {
            MySoldier_Archer_Arrow arrow = ObjectPoolingMng.Data._MySoldier_Archer_ArrowsList[ObjectPoolingMng.Data.UseMySoldier_Archer_Arrow()];
            Vector2 dis = _TargetUnit.GetUnitHitPos() - GetShootPos();
            arrow.Shoot(GetShootPos(), dis.x, dis.y, (Mathf.Abs(dis.x) / _AttackRange) * _BulletFlyingTime);
        }

    }

    Vector2 GetShootPos()
    {
        Vector2 pos = new Vector2(_ShootPos.transform.localPosition.x * transform.localScale.x, 
            _ShootPos.transform.localPosition.y * transform.localScale.y);
        return GetUnitPos() + pos;
    }
}
