using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGroundSoldierMng : MonoBehaviour
{
    const int _MAXSOLDIERCOUNT = 4;
    const int _SOLDIERKINDCOUNT = 4;
    

    [SerializeField] GameObject _Ground;
    [SerializeField] GameObject[] _Soldiers = new GameObject[_SOLDIERKINDCOUNT];
    [SerializeField] GameObject[] _Heros = new GameObject[5];

    List<GameObject> _SoldierList = new List<GameObject>();
    public List<GameObject> _HeroList = new List<GameObject>();

    Vector2 _BorderRect1 = new Vector2(-5, -2);
    Vector2 _BorderRect2 = new Vector2(5, 2);

    void Awake()
    {
        for (int i = 0; i < _MAXSOLDIERCOUNT; i++)
        {
            for (int j = 0; j < _SOLDIERKINDCOUNT; j++)
            {
                GameObject obj1 = Instantiate(_Soldiers[j], _Ground.transform);
                obj1.transform.localPosition = new Vector3(Random.Range(_BorderRect1.x, _BorderRect2.x), Random.Range(_BorderRect1.y, _BorderRect2.y), 0);
                obj1.SetActive(false);
                _SoldierList.Add(obj1);
            }
        }
        for(int i=0;i<5;i++)
        {
            if (_Heros[i])
            {
                GameObject obj1 = Instantiate(_Heros[i], _Ground.transform);
                obj1.transform.localPosition = new Vector3(Random.Range(_BorderRect1.x, _BorderRect2.x), Random.Range(_BorderRect1.y, _BorderRect2.y), 0);
                obj1.SetActive(false);
                _HeroList.Add(obj1);
            }
            else
                _HeroList.Add(new GameObject());
        }
        
    }

    public void SetOnCount()
    {
        int n = (int)(UpgradeMng.Data._SoldierLevel /15.0f)+1;
        if (n > _MAXSOLDIERCOUNT) n = _MAXSOLDIERCOUNT;
        for (int i = 0; i < n; i++)
        {
            for(int j=0;j<_SOLDIERKINDCOUNT;j++)
                _SoldierList[i * _SOLDIERKINDCOUNT + j].SetActive(true);
        }
        if (UpgradeMng.Data._SkillLevel_Soldier_Seven >= 2)
        {
            _HeroList[0].SetActive(true);
        }
        if (UpgradeMng.Data._Skill_Level_Soldier_Hero_Ace >= 5)
        {
            _HeroList[1].SetActive(true);
        }
        if (UpgradeMng.Data._Skill_Level_Soldier_Hero_Jack >= 5)
        {
            _HeroList[2].SetActive(true);
        }
        if (UpgradeMng.Data._Skill_Level_Soldier_Hero_Queen >= 5)
        {
            _HeroList[3].SetActive(true);
        }
        if (UpgradeMng.Data._Skill_Level_Soldier_Hero_King >= 5)
        {
            _HeroList[4].SetActive(true);
        }
    }
}
