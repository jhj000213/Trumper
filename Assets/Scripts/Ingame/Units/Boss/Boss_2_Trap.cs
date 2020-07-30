using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_Trap : MonoBehaviour
{
    Animator _Animator;
    List<Unit> _EnemyList;
    float _Dmg;
    const float _TRAPDAMAGEPER = 1;
    Vector2 _TargetPos;
    bool _Moving;
    const float _MOVESPEED = 7.5f;
    bool _Using;

    public void Init(float dmg,Vector2 pos,List<Unit> list)
    {
        _Animator = GetComponent<Animator>();
        _Dmg = dmg * _TRAPDAMAGEPER;
        _TargetPos = pos;
        _Moving = true;
        _EnemyList = list;
        _Using = false;
    }

    private void Update()
    {
        if(!_Using)
        {
            if (_Moving)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, _TargetPos, _MOVESPEED * Time.smoothDeltaTime);
                if (Vector2.Distance(transform.localPosition, _TargetPos) < 0.1f)
                {
                    transform.localPosition = _TargetPos;
                    _Moving = false;
                    _Animator.SetTrigger("stay");
                }
            }
            else
            {
                if (Check())
                {
                    _Using = true;
                    _Animator.SetTrigger("boom");
                    StartCoroutine(Boom(7));
                    StartCoroutine(Boom(13));
                    StartCoroutine(Boom(19));
                    StartCoroutine(Boom(24));
                }
            }
        }
    }

    bool Check()
    {
        for(int i=0;i<_EnemyList.Count;i++)
        {
            if (Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) < 2) 
                return true;
        }
        return false;
    }

    IEnumerator Boom(int time)
    {
        yield return new WaitForSeconds(time*0.016f);

        SoundMng.Data.PlayEffectSound(EffectSound.Boss2_Boom, 0, transform);

        List<Unit> hitenemy = new List<Unit>();
        for(int i=0;i<_EnemyList.Count;i++)
        {
            if(Vector2.Distance(transform.localPosition,_EnemyList[i].GetUnitPos()) < 3.5f)
                hitenemy.Add(_EnemyList[i]);
        }
        for(int i=0;i<hitenemy.Count;i++)
            hitenemy[i].HitDamage(_Dmg);
    }
}
