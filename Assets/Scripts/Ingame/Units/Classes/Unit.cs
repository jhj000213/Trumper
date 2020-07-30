using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] GameObject _UnitCenter;
    protected Animator _Animator = null;
    protected List<Unit> _DeadList;
    [SerializeField] protected SpriteRenderer _HPGaze;
    [SerializeField] protected SpriteRenderer _HPGaze_Under;
    [SerializeField] GameObject _HPBar;
    protected float _NowHP;
    protected float _MaxHP;

    protected bool _Kidnapping = false;
    protected bool _Alive;
    protected string _UnitName;

    [SerializeField] protected SpriteRenderer _BodySprite;
    [SerializeField] protected SpriteRenderer _BodySprite_Hit;
    [SerializeField] protected SpriteRenderer[] _FrontHandFoot_Hit;
    [SerializeField] protected SpriteRenderer[] _BackHandFoot_Hit;
    protected int _LayerOrder;

    protected float _HitEffectTimer;
    protected float _HitBackTime;
    
    virtual public void Init(List<Unit> s) { }
    virtual public void Init(List<Unit> s,List<Unit> f) { }
    virtual public void Init(float hp,float dmg,string unitname,int shapetype, List<Unit> enemy, List<Unit> dead) { }
    virtual public void Init_Shield(float hp,float dmg,string unitname,int shapetype, List<Unit> friend, List<Unit> enemy, List<Unit> dead) { }
    virtual public void Init(float hp,float dmg,string unitname,List<Unit> enemy, List<Unit> dead,bool isboss,int maxmonth) { }
    virtual public void Pushout(float range) { }
    virtual public void Kidnap() { }

    protected void Start()
    {
        _Animator = GetComponent<Animator>();
        _NowHP = _MaxHP;
        _Alive = true;
        _LayerOrder = 30000 - (int)((GetUnitPos().y - 1.1f) * 10000);
        _BodySprite.sortingOrder = _LayerOrder;
        if (_BodySprite_Hit)
            _BodySprite_Hit.sortingOrder = _LayerOrder + 1;
        
    }

    protected void Update()
    {
        _HPGaze.size = new Vector2(_NowHP / _MaxHP, 1);
        //_HPGaze.size = new Vector2(Mathf.MoveTowards(_HPGaze.size.x, _NowHP / _MaxHP, Time.smoothDeltaTime * 0.5f), 1);
        if(_HPGaze_Under)
            _HPGaze_Under.size = new Vector2(Mathf.MoveTowards(_HPGaze_Under.size.x, _NowHP / _MaxHP,Time.smoothDeltaTime), 1);

        _HitEffectTimer -= Time.smoothDeltaTime * 3;
        _HitBackTime -= Time.smoothDeltaTime;
        if (_HitEffectTimer < 0)
        {
            _HitEffectTimer = 0;
            if(_BodySprite_Hit)
                _BodySprite_Hit.gameObject.SetActive(false);
            for (int i = 0; i < _FrontHandFoot_Hit.Length; i++)
                _FrontHandFoot_Hit[i].gameObject.SetActive(false);
            for (int i = 0; i < _BackHandFoot_Hit.Length; i++)
                _BackHandFoot_Hit[i].gameObject.SetActive(false);
        }
        else
        {
            if (_BodySprite_Hit)
                _BodySprite_Hit.color = new Color(1, 1 - _HitEffectTimer, 1 - _HitEffectTimer);
            for (int i = 0; i < _FrontHandFoot_Hit.Length; i++)
                _FrontHandFoot_Hit[i].color = new Color(1, 1 - _HitEffectTimer, 1 - _HitEffectTimer);
            for (int i = 0; i < _BackHandFoot_Hit.Length; i++)
                _BackHandFoot_Hit[i].color = new Color(1, 1 - _HitEffectTimer, 1 - _HitEffectTimer);

        }
        
    }

    public virtual void HitDamage(float dmg,bool onedead=false)
    {
        if(GetStillAlive())
        {
            HitEffect();
            if (onedead)
                dmg = _MaxHP;
            _NowHP -= dmg;
            DamageFont obj = ObjectPoolingMng.Data._DamageFontObjList[ObjectPoolingMng.Data.UseDamageFont()];
            if (dmg < 1.0f && 0 < dmg)
                obj.SetDamage(1,GetUnitHPPos(),IsEnemy(),onedead);
            else
                obj.SetDamage((int)dmg, GetUnitHPPos(), IsEnemy(), onedead);
            if (_NowHP <= 0.0f)
                DeadUnit();
        }
    }

    void HitSound()
    {
        //if (IsEnemy())
        //    SoundMng.Data.PlayEffectSound(EffectSound.EnemySoldier_Hit, 0, transform);
        //else
        //    SoundMng.Data.PlayEffectSound(EffectSound.MySoldier_Hit, 0, transform);
    }

    public void UnitStop_GameEnd()
    {
        if(_Alive)
        {
            _Animator.Play(_UnitName + "_stay");
            _Animator.SetTrigger("stay");
        }
        _Alive = false;
    }

    virtual public void DeadUnit_GameEnd()
    {
        _Alive = false;
        gameObject.SetActive(false);
        _DeadList.Add(this);
    }

    virtual public void DeadUnit()
    {
        _NowHP = 0.0f;
        if(_Alive)
        {
            _Animator.SetTrigger("stop");
            _Animator.SetTrigger("dead");
            _Alive = false;

            if (_BodySprite_Hit)
                _BodySprite_Hit.gameObject.SetActive(false);
            for (int i = 0; i < _FrontHandFoot_Hit.Length; i++)
                _FrontHandFoot_Hit[i].gameObject.SetActive(false);
            for (int i = 0; i < _BackHandFoot_Hit.Length; i++)
                _BackHandFoot_Hit[i].gameObject.SetActive(false);

            StartCoroutine(DeadAni());
        }
    }

    IEnumerator DeadAni()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
        _DeadList.Add(this);
    }

    
    public void SetHeart(float heal)
    {
        float dmg = heal;
        float max = _MaxHP - _NowHP;

        _NowHP += heal;
        if (_NowHP >= _MaxHP)
            _NowHP = _MaxHP;


        if (dmg > max)
            dmg = max;

        DamageFont obj = ObjectPoolingMng.Data._DamageFontObjList[ObjectPoolingMng.Data.UseDamageFont()];
        if (dmg < 1.0f && 0 < dmg)
            obj.SetDamage(1, GetUnitHPPos(), IsEnemy());
        else
            obj.SetDamage((int)dmg, GetUnitHPPos(), IsEnemy());
        obj.IsHeal();
    }
    public void SetHeartPercent(int perc)
    {
        float dmg = (perc / 100.0f) * _MaxHP;
        float max = _MaxHP - _NowHP;
        _NowHP += dmg;


        if (_NowHP >= _MaxHP)
            _NowHP = _MaxHP;
        if (dmg > max)
            dmg = max;

        DamageFont obj = ObjectPoolingMng.Data._DamageFontObjList[ObjectPoolingMng.Data.UseDamageFont()];
        if (dmg < 1.0f && 0 < dmg)
            obj.SetDamage(1, GetUnitHPPos(), IsEnemy());
        else
            obj.SetDamage((int)dmg, GetUnitHPPos(), IsEnemy());
        obj.IsHeal();
    }

    virtual protected void HitEffect()
    {
        if (!GetStillAlive())
            return;
        _HitEffectTimer = 1.0f;
        //_Animator.SetTrigger("hit");
        if (_BodySprite_Hit)
            _BodySprite_Hit.gameObject.SetActive(true);
        for (int i = 0; i < _FrontHandFoot_Hit.Length; i++)
            _FrontHandFoot_Hit[i].gameObject.SetActive(true);
        for (int i = 0; i < _BackHandFoot_Hit.Length; i++)
            _BackHandFoot_Hit[i].gameObject.SetActive(true);

    }

    public Vector2 GetUnitPos() { return transform.localPosition; }
    public Vector2 GetUnitHitPos() { return transform.localPosition + _UnitCenter.transform.localPosition; }
    public Vector2 GetUnitHPPos() { return transform.localPosition + _HPBar.transform.localPosition; }
    public bool GetStillAlive() { return _Alive; }
    public bool IsEnemy() { return _UnitName[0] == 'e' || _UnitName[0] == 'b'; }
    public string GetUnitName() { return _UnitName; }
    public bool GetKidnapping() { return _Kidnapping; }
    public float GetUnitMaxHP() { return _MaxHP; }
    public virtual void SetShape(int n) { }
}
