using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_6 : Boss_Castle
{
    [SerializeField] GameObject _Butterfly;
    [SerializeField] SpriteRenderer _SkillGaze;
    float _SkillTimer = 0;
    const float _SkillCoolTime = 4.5f;


    List<GameObject> _GarbageList = new List<GameObject>();

    new protected void Update()
    {
        base.Update();
        _SkillTimer += Time.smoothDeltaTime;
        _SkillGaze.size = new Vector2(_SkillTimer / _SkillCoolTime, 1);
        if (_SkillTimer >= _SkillCoolTime)
        {
            _SkillTimer = 0;
            MakeButterfly();
        }
    }

    void MakeButterfly()
    {
        if (_EnemyList.Count <= 0)
            return;
        GameObject obj = Instantiate(_Butterfly, transform.parent);
        obj.transform.localPosition = transform.localPosition+  new Vector3(0,4,0);
        _GarbageList.Add(obj);

        Unit target = _EnemyList[0];
        for(int i=0;i<_EnemyList.Count;i++)
        {
            if (_EnemyList[i].GetUnitPos().x > target.GetUnitPos().x)
                target = _EnemyList[i];
        }
        obj.GetComponent<Boss_6_Butterfly>().Init(target, _EnemyList, _Dmg);
    }

    public override void DeadUnit()
    {
        for (int i = _GarbageList.Count - 1; i >= 0; i--)
            Destroy(_GarbageList[i]);
        base.DeadUnit();
    }
}
