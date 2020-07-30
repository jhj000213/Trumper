using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public partial class UnitMng : MonoBehaviour
{

    private static UnitMng instance = null;
    public static UnitMng Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(UnitMng)) as UnitMng;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }

    public List<Unit> _MySoldierList = new List<Unit>();
    [SerializeField] List<Unit> _EnemySoldierList = new List<Unit>();
    public SpriteAtlas _EnemyBodyAtlas;

    public List<Unit> _DeadUnitList = new List<Unit>();

    bool _Soldier_Seven_Alive;
    bool _Hero_Ace_Alive;
    bool _Hero_Jack_Alive;
    bool _Hero_Queen_Alive;
    bool _Hero_King_Alive;
    const float _PushoutCenter = 47.23f;
    const float _PushoutRange = 10.0f;

    int _NowStage;

    bool _Sword_Alive;
    bool _Archer_Alive;
    bool _Shield_Alive;
    bool _Spear_Alive;

    Boss_11 _Boss_11_obj;

    private void Update()
    {
        for(int i=0;i<_DeadUnitList.Count;i++)
        {
            Unit obj = _DeadUnitList[i];
            _MySoldierList.Remove(obj);
            _EnemySoldierList.Remove(obj);
            _DeadUnitList.Remove(obj);
            Destroy(obj.gameObject);
            //Destroy(obj);
            CheckHeroAlive();
            break;
        }
        MiniMapUpdate();
    }

    public void GameStart(int stage)
    {
        _NowStage = stage;
    }

    public void AddMyUnit(Unit unit)//castle
    {
        unit.Init(_DeadUnitList);
        _MySoldierList.Add(unit);
    }
    public void AddMyUnit(Unit unit,float hp,float dmg,string name)
    {
        if(name== "mysoldier_shield")
            unit.Init_Shield(hp, dmg, name, SoldierShapeBuffMng.Data.GetNowShapeNum(), _MySoldierList,_EnemySoldierList, _DeadUnitList);
        else
            unit.Init(hp, dmg, name, SoldierShapeBuffMng.Data.GetNowShapeNum(), _EnemySoldierList, _DeadUnitList);
        _MySoldierList.Add(unit);
        CheckHeroAlive();
    }

    public void AddEnemyUnit(Unit unit)//castle
    {
        unit.Init(_DeadUnitList);
        _EnemySoldierList.Add(unit);
    }

    public void AddEnemyUnit(Unit unit, float hp, float dmg, string name, bool isboss = true)
    {
        int month = (_NowStage - 1) / 5 + 1;
        if (month > 12)
            month = 12;
        unit.Init(hp, dmg, name, _MySoldierList, _DeadUnitList, isboss, month);
        _EnemySoldierList.Add(unit);
    }
    public void AddEnemyUnit_Boss11(Boss_11 unit, float dmg, string name)
    {
        unit.Init( dmg, name, _MySoldierList);
        _Boss_11_obj = unit;
    }

    public void BossSummon_Pushout(float pos = _PushoutCenter)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.NuckBack, 0);
        for (int i = 0; i < _MySoldierList.Count; i++)
        {
            if (_MySoldierList[i].GetUnitPos().x > pos - _PushoutRange)
                _MySoldierList[i].Pushout(7.0f);
        }
    }

    public void CheckHeroAlive()
    {
        _Soldier_Seven_Alive = false;
        _Hero_Ace_Alive = false;
        _Hero_Jack_Alive = false;
        _Hero_Queen_Alive = false;
        _Hero_King_Alive = false;

        _Sword_Alive = false;
        _Archer_Alive = false;
        _Shield_Alive = false;
        _Spear_Alive = false;

        for (int i = 0; i < _MySoldierList.Count; i++)
        {
            if (_MySoldierList[i].GetUnitName() == "mysoldier_hero_ace")
                _Hero_Ace_Alive = true;
            else if (_MySoldierList[i].GetUnitName() == "mysoldier_hero_jack")
                _Hero_Jack_Alive = true;
            else if (_MySoldierList[i].GetUnitName() == "mysoldier_hero_queen")
                _Hero_Queen_Alive = true;
            else if (_MySoldierList[i].GetUnitName() == "mysoldier_hero_king")
                _Hero_King_Alive = true;
            else if (_MySoldierList[i].GetUnitName() == "mysoldier_seven")
                _Soldier_Seven_Alive = true;
            else if (_MySoldierList[i].GetUnitName() == "mysoldier_sword")
                _Sword_Alive = true;
            else if (_MySoldierList[i].GetUnitName() == "mysoldier_archer")
                _Archer_Alive = true;
            else if (_MySoldierList[i].GetUnitName() == "mysoldier_shield")
                _Shield_Alive = true;
            else if (_MySoldierList[i].GetUnitName() == "mysoldier_spear")
                _Spear_Alive = true;


        }
    }

    public void SetShapeAbility(int shapenum)
    {
        for(int i=0;i<_MySoldierList.Count;i++)
        {
            _MySoldierList[i].SetShape(shapenum);
        }
    }

    public void UsingHeartAbility(int d)
    {
        for (int i = 0; i < _MySoldierList.Count; i++)
        {
            _MySoldierList[i].SetHeartPercent(d);
        }
    }

    public void EndGame_StopMove()
    {
        for (int i = 0; i < _MySoldierList.Count; i++)
        {
            _MySoldierList[i].UnitStop_GameEnd();
        }
        for (int i = 0; i < _EnemySoldierList.Count; i++)
        {
            _EnemySoldierList[i].UnitStop_GameEnd();
        }
        if (_Boss_11_obj)
        {
            _Boss_11_obj.DeadBoss_11();
            _Boss_11_obj = null;
        }
    }

    public void EndGame()
    {
        for(int i=0;i<_MySoldierList.Count;i++)
        {
            _MySoldierList[i].DeadUnit_GameEnd();
        }
        for (int i = 0; i < _EnemySoldierList.Count; i++)
        {
            _EnemySoldierList[i].DeadUnit_GameEnd();
        }
    }

    public int GetQueenCount()
    {
        int count = 0;
        if (_Sword_Alive)
            count++;
        if (_Archer_Alive)
            count++;
        if (_Shield_Alive)
            count++;
        if (_Spear_Alive)
            count++;
        if (_Soldier_Seven_Alive)
            count++;

        return count;
    }

    public bool GetSoldierSevenAlive() { return _Soldier_Seven_Alive; }
    public bool GetHeroAceAlive() { return _Hero_Ace_Alive; }
    public bool GetHeroJackAlive() { return _Hero_Jack_Alive; }
    public bool GetHeroQueenAlive() { return _Hero_Queen_Alive; }
    public bool GetHeroKingAlive() { return _Hero_King_Alive; }
}
