using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSummonInfo
{
    string _UnitName;

    public UnitSummonInfo(string unitname)
    {
        _UnitName = unitname;
    }

    public UnitSummonInfo GetInfo() { return this; }
    public string GetUnitName() { return _UnitName; }
}

public class UnitSummonInfo_Auto : UnitSummonInfo
{
    float _SummonTime;
    int _SummonCount;

    public UnitSummonInfo_Auto(string unitnumber, float summontime, int summoncount) : base(unitnumber)
    {
        _SummonTime = summontime;
        _SummonCount = summoncount;
    }

    public UnitSummonInfo_Auto GetInfo_Auto() { return this; }
    public float GetSummonTime() { return _SummonTime; }
    public int GetSummonCount() { return _SummonCount; }
}

public class EnemyInfoMng : MonoBehaviour
{
    public List<List<UnitSummonInfo_Auto>> _SpecialEnemySummonInfo = new List<List<UnitSummonInfo_Auto>>();
    public List<float> _UnitSpecialSummonLoopTime = new List<float>();

    public List<UnitSummonInfo> _StageBaseEnemyInfo = new List<UnitSummonInfo>();
    public List<UnitSummonInfo> _BossInfo = new List<UnitSummonInfo>();


    public List<float> _StageEnemySoldierHP = new List<float>();
    public List<float> _StageEnemySoldierDmg = new List<float>();
    public List<float> _StageBossHP = new List<float>();
    public List<float> _StageBossDmg = new List<float>();

    private void Awake()
    {

        StageEnemyUnitInit();
        BossInit();
    }

    void BossInit()
    {
        List<Dictionary<string, object>> boss = CSVReader.Read("Boss");
        int max = (int)boss[0]["max"];
        for (int i = 0; i < max; i++)
        {
            _BossInfo.Add(new UnitSummonInfo((string)boss[i]["unitname"]));
            _StageBossHP.Add((float)boss[i]["hp"]);
            _StageBossDmg.Add((float)boss[i]["dmg"]);
        }
    }
    
    void StageEnemyUnitInit()
    {


        List<Dictionary<string, object>> basestagedata = CSVReader.Read("StageBaseEnemy");
        List<Dictionary<string, object>> specialstagedata;
        int MaxStage = (int)basestagedata[0]["max"];
        for (int i = 0; i < MaxStage; i++)
        {
            float dmg = (float)basestagedata[i]["dmg"];
            float hp = (float)basestagedata[i]["hp"];
            _StageBaseEnemyInfo.Add(new UnitSummonInfo((string)basestagedata[i]["unitname"]));
            _StageEnemySoldierHP.Add(hp);
            _StageEnemySoldierDmg.Add(dmg);

            List<UnitSummonInfo_Auto> list = new List<UnitSummonInfo_Auto>();
            specialstagedata = CSVReader.Read("Stage/Stage" + (i + 1).ToString());
            _UnitSpecialSummonLoopTime.Add((float)specialstagedata[0]["looptime"]);

            int max = (int)(specialstagedata[0]["totalcount"]);
            for (int x = 0; x < max; x++)
            {
                string name = (string)specialstagedata[x]["unitname"];
                list.Add(new UnitSummonInfo_Auto(name, (float)specialstagedata[x]["summontime"], (int)specialstagedata[x]["count"]));
            }
            _SpecialEnemySummonInfo.Add(list);
        }
    }
}
