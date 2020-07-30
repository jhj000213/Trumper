using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_11 : MonoBehaviour
{
    float _SkillTimer = 0;
    const float _SkillCoolTime = 10.0f;
    
    const float _SkillRange_Breath = 5.0f;
    const float _SkillDmgPer = 0.3f;

    bool _Kidnap;
    Unit _KidnapTarget;

    [SerializeField] GameObject _BodyObject;
    [SerializeField] GameObject _BreathEffect;
    List<GameObject> _GarbageList = new List<GameObject>();

    List<Unit> _EnemyList;
    Animator _Animator;
    float _UnitDamage;
    string _UnitName;

    public void Init(float dmg, string unitname, List<Unit> enemy)
    {
        _UnitDamage = dmg;
        _UnitName = unitname;
        _EnemyList = enemy;
        //_SkillTimer=-7.0f;
    }

    private void Start()
    {
        _Animator = GetComponent<Animator>();
        _Kidnap = false;
    }

    private void Update()
    {
        if(_SkillTimer>=_SkillCoolTime)
        {
            _SkillTimer = 0;
            Attack();
        }
        if(_Kidnap)
        {
            if(_KidnapTarget)
                _KidnapTarget.transform.parent = _BodyObject.transform;
        }
        else
            _SkillTimer += Time.smoothDeltaTime;
    }

    protected void Attack()
    {
        int r = Random.Range(0, 2);

        if (_EnemyList.Count <= 1)
            return;
        Unit target = null;
        for (int i = 1; i < _EnemyList.Count; i++)
        {
            bool ishero = false;
            if (_EnemyList[i].GetUnitName() == "mysoldier_hero_ace")
                ishero = true;
            else if (_EnemyList[i].GetUnitName() == "mysoldier_hero_jack")
                ishero = true;
            else if (_EnemyList[i].GetUnitName() == "mysoldier_hero_queen")
                ishero = true;
            else if (_EnemyList[i].GetUnitName() == "mysoldier_hero_king")
                ishero = true;
            if(ishero && !((MySoldier)_EnemyList[i]).GetNowJump())
                target = _EnemyList[i];
        }
        if (target == null)
            r = 1;
        else
            r = 0;


        switch(r)
        {
            case 0:
                HeroCatch(target);
                break;
            case 1:
                Breath();
                break;
            case 2:
                Debug.Log("Randnomerror boss11");
                break;
        }
    }

    void HeroCatch(Unit target)
    {
        transform.localPosition = new Vector3(target.GetUnitPos().x, 0);
        _Animator.SetTrigger("kidnap");
        _KidnapTarget = target;

        _KidnapTarget.Kidnap();
        StartCoroutine(Kidnapping());
        Debug.Log("Kidnap");
    }

    IEnumerator Kidnapping()
    {
        yield return new WaitForSeconds(1.5f);

        _Kidnap = true;
        StartCoroutine(KidnapDamage());

        SoundMng.Data.PlayEffectSound(EffectSound.Boss11_Kidnap, 0, transform);
    }

    IEnumerator KidnapDamage()
    {
        yield return new WaitForSeconds(2.0f);

        _KidnapTarget.HitDamage(100000000,true);
        _Kidnap = false;
        _KidnapTarget = null;
    }

    void Breath()
    {
        Debug.Log("breath");
        _Animator.SetTrigger("breath");
        SoundMng.Data.PlayEffectSound(EffectSound.Boss11_Breath, 0, transform);

        Unit target = _EnemyList[0];
        for (int i = 0; i < _EnemyList.Count; i++)
        {
            if (_EnemyList[i].GetUnitPos().x > target.GetUnitPos().x)
                target = _EnemyList[i];
        }
        transform.localPosition = new Vector3(target.GetUnitPos().x, 0);

        StartCoroutine(BreathEffect());
        StartCoroutine(BreathHit(1.0f));
        StartCoroutine(BreathHit(1.1f));
        StartCoroutine(BreathHit(1.2f));
        StartCoroutine(BreathHit(1.3f));
        StartCoroutine(BreathHit(1.4f));
        StartCoroutine(BreathHit(1.5f));
        StartCoroutine(BreathHit(1.6f));
        StartCoroutine(BreathHit(1.7f));
    }

    IEnumerator BreathEffect()
    {
        yield return new WaitForSeconds(0.7f);

        GameObject obj = Instantiate(_BreathEffect,transform);
        obj.transform.localPosition = new Vector3(0.93f,6.74f,0);
        _GarbageList.Add(obj);
    }

    IEnumerator BreathHit(float time)
    {
        yield return new WaitForSeconds(time);

        List<Unit> hitenemy = new List<Unit>();
        for (int i = 0; i < _EnemyList.Count; i++)
        {
            if (Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) <= _SkillRange_Breath)
                hitenemy.Add(_EnemyList[i]);
        }
        for (int i = 0; i < hitenemy.Count; i++)
            hitenemy[i].HitDamage(_UnitDamage * _SkillDmgPer);
    }

    public void DeadBoss_11()
    {
        for (int i = _GarbageList.Count - 1; i >= 0; i--)
            Destroy(_GarbageList[i]);
        Destroy(gameObject);
    }
}
