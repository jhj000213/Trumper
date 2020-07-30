using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_5 : Boss_Castle
{
    [SerializeField] SpriteRenderer _SkillGaze;
    float _SkillTimer = 0;
    const float _SkillCoolTime = 3;
    static int _NiddleCount = 8;
    float[] _SkillRange = new float[_NiddleCount];
    const float _SkillDmgPer = 0.1f;

    float _DeadTimer = 0;
    const float _DeadTimeMax = 300;
    [SerializeField] Transform _NiddlePos;

    private void Start()
    {
        base.Start();
        for (int i = 0; i < _NiddleCount; i++)
            _SkillRange[i] = 2.0f*i;
    }

    new protected void Update()
    {
        base.Update();
        _DeadTimer += Time.smoothDeltaTime;
        _SkillTimer += Time.smoothDeltaTime;
        _SkillGaze.size = new Vector2(_SkillTimer / _SkillCoolTime, 1);
        if (_DeadTimer >= _DeadTimeMax)
        {
            Unit castle = _EnemyList[0];
            for (int i = 0; i < 100; i++)
            {
                if (!castle.GetStillAlive())
                    break;
                _EnemyList[0].HitDamage(1);
            }
        }
        if (_SkillTimer >= _SkillCoolTime)
        {
            _SkillTimer = 0;

            StartCoroutine(MakeNiddle(0.6f/40.0f));//0.025f

            //GameObject obj = Instantiate(_Niddle, transform);
            //obj.transform.localPosition = new Vector3(-1.35f, 0, 0);
            //_GarbageList.Add(obj);
            for (int i=1;i< _NiddleCount;i++)
                StartCoroutine(UseSkill(i,0.15f*i));
        }
    }
    IEnumerator MakeNiddle(float time)
    {
        for (int i = 0; i < 40; i++)
        {
            yield return new WaitForSeconds(time);
            if (GetStillAlive())
            {
                Boss_5_Niddle niddle = ObjectPoolingMng.Data._Boss_5_NiddleList[ObjectPoolingMng.Data.UseBoss_5_Niddle()];
                niddle.Shoot(new Vector3(-0.25f * i, 0, 0) + transform.localPosition + _NiddlePos.localPosition, new Vector3(0.4f + i * 0.04f, 0.4f + i * 0.04f, 1));
            }
        }
    }
    IEnumerator UseSkill(int n,float time)
    {
        yield return new WaitForSeconds(time);
        if(_Alive)
        {

            List<Unit> hitenemy = new List<Unit>();
            for (int i = 0; i < _EnemyList.Count; i++)
            {
                if (Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) >= _SkillRange[n - 1] && Vector2.Distance(transform.localPosition, _EnemyList[i].GetUnitPos()) < _SkillRange[n])
                    hitenemy.Add(_EnemyList[i]);
            }

            for (int i = 0; i < hitenemy.Count; i++)
                hitenemy[i].HitDamage(_Dmg * _SkillDmgPer);
        }
    }

    //public override void DeadUnit()
    //{
    //    for (int i = _GarbageList.Count - 1; i >= 0; i--)
    //        Destroy(_GarbageList[i]);
    //    base.DeadUnit();
    //}
}
