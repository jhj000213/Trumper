using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MySoldier : Soldier
{
    protected int _ShapeType;

    [SerializeField] SpriteAtlas _BodyAtlas;

    [SerializeField] GameObject _JumpBar;
    [SerializeField] protected SpriteRenderer _JumpGaze;
    float _NowJumpReadyTime;
    float _MaxJumpReadyTime;

    int _BodyNumber;

    protected float _JumpPercent;
    protected Vector2 _JumpOriginPos;
    protected float _JumpTargetPos;
    protected bool _Jumping;
    protected bool _JumpReady;
    float _JumpHeight = 200.0f;
    float _JumpTime;
    const float _MaxJumpTime = 2.0f;

    protected Vector2 _PushoutPos;
    protected bool _Pushoutting;
    const float _PushoutSpeed = 0.15f;

    override public void Init(float hp, float dmg, string unitname, int shapetype, List<Unit> enemylist, List<Unit> deadlist)
    {
        _MaxHP = hp;
        _UnitDamage = dmg;
        _UnitName = unitname;
        _ShapeType = shapetype;
        _EnemyList = enemylist;
        _DeadList = deadlist;
    }

    public override void Pushout(float range)
    {
        if(_Alive)
        {
            if (_Jumping)
                _PushoutPos = new Vector3(_JumpTargetPos, _JumpOriginPos.y, 0) + new Vector3(-range, 0, 0);
            else
                _PushoutPos = transform.localPosition + new Vector3(-range, 0, 0);
            _Pushoutting = true;
            StartCoroutine(PushoutEnd(1.0f));
        }
    }

    IEnumerator PushoutEnd(float time)
    {
        yield return new WaitForSeconds(time);

        _Pushoutting = false;
        transform.localPosition = _PushoutPos;
    }

    public override void Kidnap()
    {
        _Kidnapping = true;
        if(_Animator!=null)
            _Animator.speed =0;
    }

    protected void Start()
    {
        base.Start();
        JumpUnit();

        int n = Random.Range(0, 2);
        if (_UnitName == "mysoldier_sword")
        {
            if (n == 0)
                _BodyNumber = 2;
            else
                _BodyNumber = 4;
        }
        else if (_UnitName == "mysoldier_archer")
        {
            if (n == 0)
                _BodyNumber = 9;
            else
                _BodyNumber = 10;
        }
        else if (_UnitName == "mysoldier_shield")
        {
            if (n == 0)
                _BodyNumber = 3;
            else
                _BodyNumber = 5;
        }
        else if (_UnitName == "mysoldier_spear")
        {
            if (n == 0)
                _BodyNumber = 6;
            else
                _BodyNumber = 8;
        }
        else if (_UnitName == "mysoldier_seven")
            _BodyNumber = 7;
        else if (_UnitName == "mysoldier_hero_ace")
            _BodyNumber = 1;
        else if (_UnitName == "mysoldier_hero_jack")
            _BodyNumber = 11;
        else if (_UnitName == "mysoldier_hero_queen")
            _BodyNumber = 12;
        else if (_UnitName == "mysoldier_hero_king")
            _BodyNumber = 13;
        ChangeBodySprite();
    }

    new protected void Update()
    {
        base.Update();
        if (!GetStillAlive())
            return;
        if (_Jumping)
        {
            _JumpTime += Time.smoothDeltaTime;
            float x = (_JumpTargetPos - _JumpOriginPos.x) * (_JumpTime / _MaxJumpTime);
            transform.localPosition = _JumpOriginPos + new Vector2(x,JumpFunc(_JumpTime)/100.0f);
            if(_JumpTime>=_MaxJumpTime)
            {
                _Jumping = false;
                transform.localPosition = new Vector3(_JumpTargetPos,_JumpOriginPos.y);
            }
        }
        else if (_Pushoutting)
        {
            if (Vector3.Distance(transform.localPosition, _PushoutPos) > 0.05f)
                transform.localPosition = Vector3.Lerp(transform.localPosition, _PushoutPos, _PushoutSpeed);
        }
        else if (!_JumpReady)
        {
            if(!_Kidnapping)
            {
                _NowAttackDelay -= Time.smoothDeltaTime;
                if (_SetTarget)
                {
                    if (!_TargetUnit.GetStillAlive())
                        _SetTarget = false;
                    else
                    {
                        if (_NowAttackDelay <= 0.0f)
                            Attack();
                    }
                }
                else
                {
                    if (_NowAttackDelay <= 0.0f)
                    {
                        transform.localPosition += new Vector3(_MoveSpeed * Time.smoothDeltaTime, 0, 0);
                        _Animator.Play(_UnitName + "_walk");
                        //_Animator.SetTrigger("walk");
                    }
                    FindEnemy();
                }
            }
        }
        
        if (_JumpReady)
        {
            _NowJumpReadyTime += Time.smoothDeltaTime;
            _JumpGaze.size = new Vector2(_NowJumpReadyTime / _MaxJumpReadyTime, 1);

            if(_MaxJumpReadyTime <= _NowJumpReadyTime)
            {
                float targetpos = _EnemyList[0].GetUnitPos().x;
                for (int i = 1; i < _EnemyList.Count; i++)
                {
                    if (_EnemyList[i].GetUnitPos().x < targetpos)
                        targetpos = _EnemyList[i].GetUnitPos().x;
                }
                JumpEdit(targetpos);
            }
        }
    }

    public override void SetShape(int n)
    {
        _ShapeType = n;
        ChangeBodySprite();
    }

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(Attack_Delay(_AttackDamageDelay));

    }

    IEnumerator Attack_Delay(float time)
    {
        yield return new WaitForSeconds(time);

        if (_SetTarget)
        {
            bool onedead = false;
            float dmg = _UnitDamage;
            

            if (UnitMng.Data.GetHeroAceAlive())//ACE
                dmg += dmg*MyUnitInfoMng.Data._Ace_UpDmg[UpgradeMng.Data._Skill_Level_Soldier_Hero_Ace - 1];
            
            if (_ShapeType == 0 || SoldierShapeBuffMng.Data.GetShapeOn_Seven_Spade())
            {
                if (Random.Range(0, 100) < SoldierShapeBuffMng.Data._SpadeData[UpgradeMng.Data._Level_Shape_Spade - 1])
                    onedead = true;
            }
            _TargetUnit.HitDamage(dmg,onedead);
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
            obj.SetDamage((int)dmg, GetUnitHPPos(), false);
            if (_NowHP <= 0.0f)
                DeadUnit();
        }
    }

    public void JumpUnit()
    {
        if (UnitMng.Data.GetHeroJackAlive() && (MyUnitInfoMng.Data._Jack_JumpRandom[UpgradeMng.Data._Skill_Level_Soldier_Hero_Jack - 1] > Random.Range(0, 100) || _UnitName == "mysoldier_hero_jack"))
        {
            if (_EnemyList.Count != 0 && !FindEnemy())
            {
                AchievementMng.Data.AddAchievementCount(12);
                float targetpos = _EnemyList[0].GetUnitPos().x;
                for (int i = 1; i < _EnemyList.Count; i++)
                {
                    if (_EnemyList[i].GetUnitPos().x < targetpos)
                        targetpos = _EnemyList[i].GetUnitPos().x;
                }
                if (targetpos - GetUnitPos().x - _AttackRange - 1.5f > 15.0f)
                {
                    float delay = MyUnitInfoMng.Data._Jack_JumpDelay[UpgradeMng.Data._Skill_Level_Soldier_Hero_Jack - 1];
                    _JumpReady = true;
                    _JumpBar.SetActive(true);
                    _MaxJumpReadyTime = delay;
                    _NowJumpReadyTime = 0;
                }
            }
        }
    }
    void JumpEdit(float targetpos)
    {
        _JumpBar.SetActive(false);
        _JumpTargetPos = targetpos - _AttackRange - 1.5f;
        _JumpOriginPos = GetUnitPos();
        _Jumping = true;
        _JumpPercent = (_JumpTargetPos - GetUnitPos().x)*100.0f/1000.0f;
        _JumpTime = 0;
        _JumpReady = false;
    }
    float JumpFunc(float time)
    {
        float x = (time/ _MaxJumpTime) *1000-500;
        return (-0.0008f * x * x + _JumpHeight)*_JumpPercent/2;
    }

    void ChangeBodySprite()
    {
        string type = "n";
        if (_ShapeType == 0)
            type = "s";
        else if (_ShapeType == 1)
            type = "h";
        else if (_ShapeType == 2)
            type = "d";
        else if (_ShapeType == 3)
            type = "c";
        _BodySprite.sprite = _BodyAtlas.GetSprite(type + _BodyNumber);
        if(_BodySprite_Hit)
            _BodySprite_Hit.sprite = _BodyAtlas.GetSprite(type + _BodyNumber);
    }

    public bool GetNowJump() { return _Jumping || _JumpReady; }
}
