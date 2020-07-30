using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySoldier_Seven : MySoldier
{
    [SerializeField] GameObject _ShootPos;
    float _EnergeFlyingTime = 0.5f;
    float _MagicReadyTime = 0.8f;

    private void Start()
    {
        _MoveSpeed = 1.35f;
        _AttackRange = Random.Range(4.5f, 5.5f);
        _AttackDelay = 2.0f;
        _AttackDamageDelay = _EnergeFlyingTime + _MagicReadyTime;
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(ShootMagic(_MagicReadyTime));

    }

    IEnumerator ShootMagic(float time)
    {
        yield return new WaitForSeconds(time);

        if (_SetTarget && GetStillAlive())
        {
            MySoldier_Seven_Magic arrow = ObjectPoolingMng.Data._MySoldier_Seven_MagicsList[ObjectPoolingMng.Data.UseMySoldier_Seven_Magic()];
            Vector2 dis = _TargetUnit.GetUnitHitPos() - GetShootPos();
            arrow.Shoot(GetShootPos(), dis.x, dis.y, (Mathf.Abs(dis.x) / _AttackRange) * _EnergeFlyingTime);
        }
    }

    Vector2 GetShootPos()
    {
        Vector2 pos = new Vector2(_ShootPos.transform.localPosition.x * transform.localScale.x,
            _ShootPos.transform.localPosition.y * transform.localScale.y);
        return GetUnitPos() + pos;
    }
}
