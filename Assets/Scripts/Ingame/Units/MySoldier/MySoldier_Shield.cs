using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySoldier_Shield : MySoldier
{
    List<Unit> _FriendList;
    float _FastMoveSpeed;
    float _NormalMoveSpeed;
    float _SpeedChangeSpeed = 1.0f;
    [SerializeField] GameObject _Dust;
    [SerializeField] ParticleSystem _Dust_P;

    override public void Init_Shield(float hp, float dmg, string unitname, int shapetype, List<Unit> friendlist, List<Unit> enemylist, List<Unit> deadlist)
    {
        _MaxHP = hp;
        _UnitDamage = dmg;
        _UnitName = unitname;
        _ShapeType = shapetype;
        _FriendList = friendlist;
        _EnemyList = enemylist;
        _DeadList = deadlist;
    }

    new private void Start()
    {
        _MoveSpeed = 0.9f;
        if (UpgradeMng.Data._SkillLevel_Soldier_Shield > 0)
            _FastMoveSpeed = MyUnitInfoMng.Data._Shield_SkillSpeed[UpgradeMng.Data._SkillLevel_Soldier_Shield - 1];
        _NormalMoveSpeed = 0.9f;

        _AttackRange = Random.Range(0.7f, 1.3f);
        _AttackDelay = 2.0f;
        _AttackDamageDelay = 0.33333f;
        base.Start();
    }

    new protected void Update()
    {
        base.Update();
        if (!GetStillAlive())
            return;
        if (UpgradeMng.Data._SkillLevel_Soldier_Shield > 0)
        {
            if (CheckFrontEmpty())
            {
                _Dust_P.Stop();
                if (Mathf.Abs(_MoveSpeed - _NormalMoveSpeed) < 0.1f)
                    _MoveSpeed = _NormalMoveSpeed;
                else
                    _MoveSpeed -= _SpeedChangeSpeed * Time.smoothDeltaTime;
            }
            else
            {
                if (!_Dust_P.isPlaying)
                    _Dust_P.Play();
                if (Mathf.Abs(_MoveSpeed - _FastMoveSpeed) < 0.1f)
                    _MoveSpeed = _FastMoveSpeed;
                else
                    _MoveSpeed += _SpeedChangeSpeed * Time.smoothDeltaTime;
            }
        }
    }

    bool CheckFrontEmpty()
    {
        bool check = true;
        for (int i = 1; i < _FriendList.Count; i++)
        {
            if (transform.localPosition.x < _FriendList[i].GetUnitPos().x && _FriendList[i].GetUnitName()!="mysoldier_shield")
                check = false;
        }
        return check;
    }
}