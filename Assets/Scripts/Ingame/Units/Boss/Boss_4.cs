using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_4 : Boss_Walk
{
    [SerializeField] SpriteRenderer _SkillGaze;
    [SerializeField] GameObject _AirDropBullet;
    public List<GameObject> _GarbageList = new List<GameObject>();

    float _SkillTimer = 0;
    const float _SkillCoolTime = 12.0f;
    const float _SkillRange = 6.5f;
    const float _SkillDmgPer = 0.175f;
    const float _SkillMotionTime = 3.0f;

    bool _Airdropping;

    new private void Start()
    {
        _MoveSpeed = -3.0f;
        _AttackRange = 3.5f;
        _AttackDelay = 1.4f;
        _AttackDamageDelay = 0.2f;
        _AvoidPer = 25.0f;
        base.Start();
    }

    new private void Update()
    {
        base.Update();
        _SkillTimer += Time.smoothDeltaTime;
        _SkillGaze.size = new Vector2(_SkillTimer / _SkillCoolTime, 1);
        if (_SkillTimer >= _SkillCoolTime)
        {
            _SkillTimer = 0;
            AirDrop();
        }
        if (_Airdropping)
            _AvoidPer = 100.0f;
        else
            _AvoidPer = 25.0f;
    }

    void AirDrop()
    {
        if (!GetStillAlive())
            return;

        _Animator.SetTrigger("airdrop");
        _NowAttackDelay = _SkillMotionTime;
        _Airdropping = true;

        SoundMng.Data.PlayEffectSound(EffectSound.Boss4_Airdrop, 1, transform);
        StartCoroutine(AirDropHit(1.0f));
        StartCoroutine(AirDropHit(1.2f));
        StartCoroutine(AirDropHit(1.4f));
        StartCoroutine(AirDropHit(1.6f));
        StartCoroutine(AirDropHit(1.8f));
        StartCoroutine(AirDropHit(2.0f));
        StartCoroutine(AirDropHit(2.2f));
        StartCoroutine(AirDropHit(2.4f));
        StartCoroutine(AirDropEnd());

        GameObject obj = Instantiate(_AirDropBullet, transform.parent);
        obj.transform.localPosition = transform.localPosition - new Vector3(_SkillRange / 2, -8, 0);
        _GarbageList.Add(obj);
    }

    IEnumerator AirDropHit(float time)
    {
        yield return new WaitForSeconds(time);

        List<Unit> hitenemy = new List<Unit>();
        for (int i = 0; i < _EnemyList.Count; i++)
        {
            if (Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) < _SkillRange)
                hitenemy.Add(_EnemyList[i]);
        }
        for (int i = 0; i < hitenemy.Count; i++)
            hitenemy[i].HitDamage(_UnitDamage * _SkillDmgPer);
    }

    IEnumerator AirDropEnd()
    {
        yield return new WaitForSeconds(_SkillMotionTime);

        _Airdropping = false;
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
}
