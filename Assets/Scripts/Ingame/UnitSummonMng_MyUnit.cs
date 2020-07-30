using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UnitSummonMng : MonoBehaviour
{
    [SerializeField] GameObject _MyUnitCreatePoint;
    [SerializeField] GameObject _MyCastleCreatePoint;
    [SerializeField] GameObject _MyCastle;
    [SerializeField] GameObject _Soldier_Sword;
    [SerializeField] GameObject _Soldier_Archer;
    [SerializeField] GameObject _Soldier_Shield;
    [SerializeField] GameObject _Soldier_Spear;
    [SerializeField] GameObject _Soldier_Seven;


    [SerializeField] GameObject _Soldier_Hero_Ace;
    [SerializeField] GameObject _Soldier_Hero_Jack;
    [SerializeField] GameObject _Soldier_Hero_Queen;
    [SerializeField] GameObject _Soldier_Hero_King;

    void CreateMyCastle()
    {
        GameObject obj = Instantiate(_MyCastle, _GroundObj.transform);
        obj.transform.localPosition = _MyCastleCreatePoint.transform.localPosition;
        UnitMng.Data.AddMyUnit(obj.GetComponent<MyCastle>());
    }

    public void CreateSoldier_Sword()
    {
        if (_NowSoldier_Sword_Delay < 0.0f && _NowCost >= _Soldier_Sword_Cost)
        {
            _NowSoldier_Sword_Delay = _MaxSoldier_Sword_Delay;
            _NowCost -= _Soldier_Sword_Cost;
            for (int i = 0; i < UpgradeMng.Data._SummonCount_Soldier_Sword * KingDouble(); i++)
            {
                GameObject obj = Instantiate(_Soldier_Sword, _GroundObj.transform);
                obj.transform.localPosition = SoldierSummonRandonPos(_MyUnitCreatePoint.transform.localPosition.x);
                UnitMng.Data.AddMyUnit(obj.GetComponent<MySoldier_Sword>(),
                    MyUnitInfoMng.Data._Sword_HP[UpgradeMng.Data._SoldierLevel - 1],
                    MyUnitInfoMng.Data._Sword_Dmg[UpgradeMng.Data._SoldierLevel - 1],
                    "mysoldier_sword");
            }
            for (int i = 0; i < 1; i++)//생성갯수는 고민
            {
                //CreateCopyEnemy(_EnemyInfoMng._StageCopyEnemyInfo[_NowStageNumber-1]);
            }
        }

    }
    public void CreateSoldier_Archer()
    {
        if (_NowSoldier_Archer_Delay < 0.0f && _NowCost >= _Soldier_Archer_Cost)
        {
            _NowSoldier_Archer_Delay = _MaxSoldier_Archer_Delay;
            _NowCost -= _Soldier_Archer_Cost;
            for (int i = 0; i < UpgradeMng.Data._SummonCount_Soldier_Archer * KingDouble(); i++)
            {
                GameObject obj = Instantiate(_Soldier_Archer, _GroundObj.transform);
                obj.transform.localPosition = SoldierSummonRandonPos(_MyUnitCreatePoint.transform.localPosition.x);
                UnitMng.Data.AddMyUnit(obj.GetComponent<MySoldier_Archer>(),
                    MyUnitInfoMng.Data._Archer_HP[UpgradeMng.Data._SoldierLevel - 1],
                    MyUnitInfoMng.Data._Archer_Dmg[UpgradeMng.Data._SoldierLevel - 1],
                    "mysoldier_archer");
            }
            for (int i = 0; i < 1; i++)//생성갯수는 고민
            {
                //CreateCopyEnemy(_EnemyInfoMng._StageCopyEnemyInfo[_NowStageNumber - 1]);
            }
        }

    }
    public void CreateSoldier_Shield()
    {
        if (_NowSoldier_Shield_Delay < 0.0f && _NowCost >= _Soldier_Shield_Cost)
        {
            _NowSoldier_Shield_Delay = _MaxSoldier_Shield_Delay;
            _NowCost -= _Soldier_Shield_Cost;
            for (int i = 0; i < UpgradeMng.Data._SummonCount_Soldier_Shield * KingDouble(); i++)
            {
                GameObject obj = Instantiate(_Soldier_Shield, _GroundObj.transform);
                obj.transform.localPosition = SoldierSummonRandonPos(_MyUnitCreatePoint.transform.localPosition.x);
                UnitMng.Data.AddMyUnit(obj.GetComponent<MySoldier_Shield>(),
                    MyUnitInfoMng.Data._Shield_HP[UpgradeMng.Data._SoldierLevel - 1],
                    MyUnitInfoMng.Data._Shield_Dmg[UpgradeMng.Data._SoldierLevel - 1],
                    "mysoldier_shield");
            }
            for (int i = 0; i < 1; i++)//생성갯수는 고민
            {
                //CreateCopyEnemy(_EnemyInfoMng._StageCopyEnemyInfo[_NowStageNumber - 1]);
            }
        }

    }
    public void CreateSoldier_Spear()
    {
        if (_NowSoldier_Spear_Delay < 0.0f && _NowCost >= _Soldier_Spear_Cost)
        {
            _NowSoldier_Spear_Delay = _MaxSoldier_Spear_Delay;
            _NowCost -= _Soldier_Spear_Cost;
            for (int i = 0; i < UpgradeMng.Data._SummonCount_Soldier_Spear * KingDouble(); i++)
            {
                GameObject obj = Instantiate(_Soldier_Spear, _GroundObj.transform);
                obj.transform.localPosition = SoldierSummonRandonPos(_MyUnitCreatePoint.transform.localPosition.x);
                UnitMng.Data.AddMyUnit(obj.GetComponent<MySoldier_Spear>(),
                    MyUnitInfoMng.Data._Spear_HP[UpgradeMng.Data._SoldierLevel - 1],
                    MyUnitInfoMng.Data._Spear_Dmg[UpgradeMng.Data._SoldierLevel - 1],
                    "mysoldier_spear");
            }
            for (int i = 0; i < 1; i++)//생성갯수는 고민
            {
                //CreateCopyEnemy(_EnemyInfoMng._StageCopyEnemyInfo[_NowStageNumber - 1]);
            }
        }

    }
    public void CreateSoldier_Seven()
    {
        if (_NowSoldier_Seven_Delay < 0.0f && _NowCost >= _Soldier_Seven_Cost && SoldierShapeBuffMng.Data.GetNowShapeNum()!=-1)
        {
            _NowSoldier_Seven_Delay = _MaxSoldier_Seven_Delay;
            _NowCost -= _Soldier_Seven_Cost;
            for (int i = 0; i < KingDouble(); i++)
            {
                GameObject obj = Instantiate(_Soldier_Seven, _GroundObj.transform);
                obj.transform.localPosition = SoldierSummonRandonPos(_MyUnitCreatePoint.transform.localPosition.x);
                UnitMng.Data.AddMyUnit(obj.GetComponent<MySoldier_Seven>(),
                    MyUnitInfoMng.Data._Seven_HP[UpgradeMng.Data._SoldierLevel - 1],
                    MyUnitInfoMng.Data._Seven_Dmg[UpgradeMng.Data._SoldierLevel - 1],
                    "mysoldier_seven");
                UnitMng.Data.CheckHeroAlive();
            }
            SoldierShapeBuffMng.Data.SetRankShapeLevel();
            for (int i = 0; i < 1; i++)//생성갯수는 고민
            {
                //CreateCopyEnemy(_EnemyInfoMng._StageCopyEnemyInfo[_NowStageNumber - 1]);
            }
        }
    }
    public void CreateSoldier_Hero()//temp
    {
        if (_NowSoldier_Hero_Delay < 0.0f && _NowCost >= _Soldier_Hero_Cost)
        {
            GameObject obj = null;
            _NowSoldier_Hero_Delay = _MaxSoldier_Hero_Delay;
            _NowCost -= _Soldier_Hero_Cost;
            if (_MyHeroNumber == 1)
            {
                AchievementMng.Data.AddAchievementCount(15);
                obj = Instantiate(_Soldier_Hero_Ace, _GroundObj.transform);
                UnitMng.Data.AddMyUnit(obj.GetComponent<MySoldier_Hero_Ace>(),
                    MyUnitInfoMng.Data._Ace_HP[UpgradeMng.Data._SoldierLevel - 1],
                    MyUnitInfoMng.Data._Ace_Dmg[UpgradeMng.Data._SoldierLevel - 1],
                    "mysoldier_hero_ace");
            }
            else if (_MyHeroNumber == 2)
            {
                obj = Instantiate(_Soldier_Hero_Jack, _GroundObj.transform);
                UnitMng.Data.AddMyUnit(obj.GetComponent<MySoldier_Hero_Jack>(),
                    MyUnitInfoMng.Data._Jack_HP[UpgradeMng.Data._SoldierLevel - 1],
                    MyUnitInfoMng.Data._Jack_Dmg[UpgradeMng.Data._SoldierLevel - 1],
                    "mysoldier_hero_jack");
            }
            else if (_MyHeroNumber == 3)
            {
                obj = Instantiate(_Soldier_Hero_Queen, _GroundObj.transform);
                UnitMng.Data.AddMyUnit(obj.GetComponent<MySoldier_Hero_Queen>(),
                    MyUnitInfoMng.Data._Queen_HP[UpgradeMng.Data._SoldierLevel - 1],
                    MyUnitInfoMng.Data._Queen_Dmg[UpgradeMng.Data._SoldierLevel - 1],
                    "mysoldier_hero_queen");
            }
            else if (_MyHeroNumber == 4)
            {
                obj = Instantiate(_Soldier_Hero_King, _GroundObj.transform);
                UnitMng.Data.AddMyUnit(obj.GetComponent<MySoldier_Hero_King>(),
                    MyUnitInfoMng.Data._King_HP[UpgradeMng.Data._SoldierLevel - 1],
                    MyUnitInfoMng.Data._King_Dmg[UpgradeMng.Data._SoldierLevel - 1],
                    "mysoldier_hero_king");
            }
            UnitMng.Data.CheckHeroAlive();
            for (int i = 0; i < 2; i++)//생성갯수는 고민
            {
                //CreateCopyEnemy(_EnemyInfoMng._StageCopyEnemyInfo[_NowStageNumber - 1]);
            }
            obj.transform.localPosition = SoldierSummonRandonPos(_MyUnitCreatePoint.transform.localPosition.x);
        }
    }

    int KingDouble()
    {
        int r = (UnitMng.Data.GetHeroKingAlive() && MyUnitInfoMng.Data._King_DoublePercent[UpgradeMng.Data._Skill_Level_Soldier_Hero_King - 1] > Random.Range(0, 100)) ? 2 : 1;
        if(r==2)
            AchievementMng.Data.AddAchievementCount(14);
        return r;
    }
    //Vector3 SummonRandonYPos() { return new Vector3(0, Random.Range(0, 1.5f), 0); }
    Vector3 SoldierSummonRandonPos(float x) { return new Vector3(x, Random.Range(_UnitSummonPosY_Min, _UnitSummonPosY_Max), 0); }
    Vector3 BossSummonPos(float x) { return new Vector3(x, (_UnitSummonPosY_Min+ _UnitSummonPosY_Max)/2.0f, 0); }
}
