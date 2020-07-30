using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UnitSummonMng : MonoBehaviour
{

    [SerializeField] GameObject _EnemyUnitCreatePoint;
    [SerializeField] GameObject _EnemyCastleCreatePoint;
    
    [SerializeField] GameObject _EnemyCastle;
    [SerializeField] GameObject _EnemySoldier_WoodSoldier;
    [SerializeField] GameObject _EnemySoldier_Katana;
    [SerializeField] GameObject _EnemySoldier_Rifle;
    [SerializeField] GameObject _EnemySoldier_Spear;
    [SerializeField] GameObject _EnemySoldier_Arrow;
    [SerializeField] GameObject _EnemySoldier_GKatana;
    [SerializeField] GameObject _EnemySoldier_GSpear;


    [SerializeField] GameObject _Boss_1;
    [SerializeField] GameObject _Boss_2;
    [SerializeField] GameObject _Boss_3;
    [SerializeField] GameObject _Boss_4;
    [SerializeField] GameObject _Boss_5;
    [SerializeField] GameObject _Boss_6;
    [SerializeField] GameObject _Boss_7;
    [SerializeField] GameObject _Boss_8_First;
    [SerializeField] GameObject _Boss_9;
    [SerializeField] GameObject _Boss_10;
    [SerializeField] GameObject _Boss_11;
    [SerializeField] GameObject _Boss_12_Bird;
    [SerializeField] GameObject _Boss_12_Human;

    const float _CHALLANGEMODE = 1.5f;
    Dictionary<string, float> _UnitDmgPer = new Dictionary<string, float>();
    Dictionary<string, float> _UnitHpPer = new Dictionary<string, float>();

    private void Awake()
    {
        _UnitHpPer.Add("woodsword", 1.0f);
        _UnitHpPer.Add("arrow", 0.7f);
        _UnitHpPer.Add("katana", 1.15f);
        _UnitHpPer.Add("spear", 0.75f);
        _UnitHpPer.Add("rifle", 0.5f);
        _UnitHpPer.Add("gkatana", 1.5f);
        _UnitHpPer.Add("gspear", 1.2f);

        _UnitDmgPer.Add("woodsword", 1.0f);
        _UnitDmgPer.Add("arrow", 1.15f);
        _UnitDmgPer.Add("katana", 1.15f);
        _UnitDmgPer.Add("spear", 1.3f);
        _UnitDmgPer.Add("rifle", 4.0f);
        _UnitDmgPer.Add("gkatana", 1.5f);
        _UnitDmgPer.Add("gspear", 1.7f);
    }

    void CreateEnemyCastle(int month)
    {
        float hp = 0;
        float dmg = 0;
        if (month > 0)
        {
            hp = _EnemyInfoMng._StageBossHP[month - 1];
            dmg = _EnemyInfoMng._StageBossDmg[month - 1];
            if (!_NowStageIsNormal)
            {
                hp = _EnemyInfoMng._StageBossHP[12] * _CHALLANGEMODE;
                dmg = _EnemyInfoMng._StageBossDmg[12] * _CHALLANGEMODE;
            }
        }
        GameObject obj;
        if (_NowStageNumber==3*StageMng.Data._STAGEGAP)
        {
            obj = Instantiate(_Boss_3, _GroundObj.transform);
            obj.transform.localPosition = _EnemyCastleCreatePoint.transform.localPosition;
            UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_3>(),
                hp, dmg, _EnemyInfoMng._BossInfo[2].GetUnitName());
        }
        else if (_NowStageNumber == 5 * StageMng.Data._STAGEGAP)
        {
            obj = Instantiate(_Boss_5, _GroundObj.transform);
            obj.transform.localPosition = _EnemyCastleCreatePoint.transform.localPosition;
            UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_5>(),
                hp, dmg, _EnemyInfoMng._BossInfo[4].GetUnitName());
        }
        else if (_NowStageNumber == 6 * StageMng.Data._STAGEGAP)
        {
            obj = Instantiate(_Boss_6, _GroundObj.transform);
            obj.transform.localPosition = _EnemyCastleCreatePoint.transform.localPosition;
            UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_6>(),
                hp, dmg, _EnemyInfoMng._BossInfo[5].GetUnitName());
        }
        else if (_NowStageNumber == 9 * StageMng.Data._STAGEGAP)
        {
            obj = Instantiate(_Boss_9, _GroundObj.transform);
            obj.transform.localPosition = _EnemyCastleCreatePoint.transform.localPosition;
            UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_9>(),
                hp, dmg, _EnemyInfoMng._BossInfo[8].GetUnitName());
        }
        else
        {
            obj = Instantiate(_EnemyCastle, _GroundObj.transform);
            obj.transform.localPosition = _EnemyCastleCreatePoint.transform.localPosition;
            UnitMng.Data.AddEnemyUnit(obj.GetComponent<EnemyCastle>());
        }
        if (_NowStageNumber == 11 * StageMng.Data._STAGEGAP)
        {
            BossSummon(11);
        }
    }
    

    //<===EnemyUnitSummon_Body===>
    void EnemyUnitSummon(UnitSummonInfo info, float summoncount)
    {
        for (int i = 0; i < summoncount; i++)
        {
            CreateEnemySoldier(info);
        }
    }

    void CreateBaseEnemy(UnitSummonInfo info)
    {
        _CopySummonCount++;
        if (_CopySummonCount >= 2)
        {
            _CopySummonCount = 0;
            CreateEnemySoldier(info);
        }
    }
    
    public void CreateEnemySoldier(UnitSummonInfo info)
    {
        string unitname = info.GetUnitName();
        float hp = _EnemyInfoMng._StageEnemySoldierHP[_NowStageNumber - 1] * _UnitHpPer[unitname];
        float dmg = _EnemyInfoMng._StageEnemySoldierDmg[_NowStageNumber - 1] * _UnitDmgPer[unitname];
        if (!_NowStageIsNormal)
        {
            hp = _EnemyInfoMng._StageEnemySoldierHP[60] * _CHALLANGEMODE;
            dmg = _EnemyInfoMng._StageEnemySoldierDmg[60] * _CHALLANGEMODE;
        }
        GameObject obj=null;
        switch(unitname)//1 = 목검 2 = 카타나 3 = 조총
        {
            case "woodsword":
                obj = Instantiate(_EnemySoldier_WoodSoldier, _GroundObj.transform);
                break;
            case "katana":
                obj = Instantiate(_EnemySoldier_Katana, _GroundObj.transform);
                break;
            case "spear":
                obj = Instantiate(_EnemySoldier_Spear, _GroundObj.transform);
                break;
            case "arrow":
                obj = Instantiate(_EnemySoldier_Arrow, _GroundObj.transform);
                break;
            case "rifle":
                obj = Instantiate(_EnemySoldier_Rifle, _GroundObj.transform);
                break;
            case "gkatana":
                obj = Instantiate(_EnemySoldier_GKatana, _GroundObj.transform);
                break;
            case "gspear":
                obj = Instantiate(_EnemySoldier_GSpear, _GroundObj.transform);
                break;
        }
        Debug.Log(unitname);
        obj.transform.localPosition = SoldierSummonRandonPos(_EnemyUnitCreatePoint.transform.localPosition.x);
        UnitMng.Data.AddEnemyUnit(obj.GetComponent<EnemySoldier>(), hp, dmg, "enemysoldier_" + unitname);
    }

    public void BossSummon(int month)
    {
        float hp = _EnemyInfoMng._StageBossHP[month - 1];
        float dmg = _EnemyInfoMng._StageBossDmg[month - 1];
        //hp = 1000000;
        if (!_NowStageIsNormal)
        {
            hp = _EnemyInfoMng._StageBossHP[12] * _CHALLANGEMODE;
            dmg = _EnemyInfoMng._StageBossDmg[12] * _CHALLANGEMODE;
        }
        CreateBoss(month, hp, dmg, _EnemyInfoMng._BossInfo[month-1].GetUnitName());

    }

    void CreateBoss(int month,float hp,float dmg,string name)
    {
        UnitMng.Data.BossSummon_Pushout();
        Debug.Log(name);
        GameObject obj = null;
        switch (month)
        {
            case 1:
                obj = Instantiate(_Boss_1, _GroundObj.transform);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_1>(), hp, dmg, name);
                break;
            case 2:
                obj = Instantiate(_Boss_2, _GroundObj.transform);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_2>(), hp, dmg, name);
                break;
            case 4:
                obj = Instantiate(_Boss_4, _GroundObj.transform);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_4>(), hp, dmg, name);
                break;
            case 7:
                obj = Instantiate(_Boss_7, _GroundObj.transform);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_7>(), hp, dmg, name);
                break;
            case 8:
                obj = Instantiate(_Boss_8_First, _GroundObj.transform);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_8_First>(), hp, dmg, name);
                break;
            case 10:
                obj = Instantiate(_Boss_10, _GroundObj.transform);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_10>(), hp, dmg, name);
                break;
            case 11:
                obj = Instantiate(_Boss_11, _GroundObj.transform);
                UnitMng.Data.AddEnemyUnit_Boss11(obj.GetComponent<Boss_11>(),  dmg, name);
                break;
            case 12:
                obj = Instantiate(_Boss_12_Bird, _GroundObj.transform);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_12_Bird>(), hp, dmg, "boss_12_bird");
                break;
            case 13:
                obj = Instantiate(_Boss_12_Human, _GroundObj.transform);
                UnitMng.Data.AddEnemyUnit(obj.GetComponent<Boss_12_Human>(), hp, dmg, "boss_12_human");
                break;
        }
        //obj.transform.localPosition = _EnemyUnitCreatePoint.transform.localPosition + SummonRandonYPos();
        obj.transform.localPosition = BossSummonPos(_EnemyUnitCreatePoint.transform.localPosition.x);

    }
}
