using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UnitSummonMng : MonoBehaviour
{
    [SerializeField] GameObject _GroundObj;

    //StageInfo
    [SerializeField] EnemyInfoMng _EnemyInfoMng;
    List<UnitSummonInfo_Auto> _SpecialUnitSummonInfoList;
    List<bool> _UnitSummonInfoChecker;

    int _NowStageNumber;
    bool _NowStageIsNormal;
    int _NowStageSummonUnitCount;
    float _SpecialEnemySummonLoopTime;
    float _SPECIALENEMYSUMMONLOOPTIME_MAX;

    float _BaseEnemySummonLoopTime;
    float _BASEENEMYSUMMONLOOPTIME_MAX = 4.0f;

    /// <summary>
    /// 1 - Ace
    /// 2 - Jack
    /// 3 - Queen
    /// 4 - King
    /// 5 - JOKER
    /// </summary>
    int _MyHeroNumber;
    [SerializeField] GameObject[] _NowHeroSummonIcon;


    const float _UnitSummonPosY_Min = 1.8f;
    const float _UnitSummonPosY_Max = 3.3f;

    const float _CastleSummonPosY = 1.2f;


    //MyUnitsDelay
    [SerializeField] Image _Soldier_Sword_Grey;
    [SerializeField] Image _Soldier_Archer_Grey;
    [SerializeField] Image _Soldier_Shield_Grey;
    [SerializeField] Image _Soldier_Spear_Grey;
    [SerializeField] Image _Soldier_Seven_Grey;
    [SerializeField] Image _Soldier_Hero_Grey;


    float _NowSoldier_Sword_Delay;
    float _MaxSoldier_Sword_Delay = 0.7f;
    float _NowSoldier_Archer_Delay;
    float _MaxSoldier_Archer_Delay = 1.0f;
    float _NowSoldier_Shield_Delay;
    float _MaxSoldier_Shield_Delay = 0.7f;
    float _NowSoldier_Spear_Delay;
    float _MaxSoldier_Spear_Delay = 0.7f;
    float _NowSoldier_Seven_Delay;
    float _MaxSoldier_Seven_Delay = 30.0f;
    float _NowSoldier_Hero_Delay;
    float _MaxSoldier_Hero_Delay = 50.0f;


    //UnitCost
    [SerializeField] Image _CostGaze;
    [SerializeField] Text _CostText;

    [SerializeField] Text _Sword_CostText;
    [SerializeField] Text _Archer_CostText;
    [SerializeField] Text _Shield_CostText;
    [SerializeField] Text _Spear_CostText;
    [SerializeField] Text _Seven_CostText;

    int _NowCost;
    int _MaxCost = 50;
    int _AddingCost = 1;
    const float _MaxCostTimer = 0.75f;
    float _CostTimer;

    int _Soldier_Sword_Cost = 10;
    int _Soldier_Archer_Cost = 10;
    int _Soldier_Shield_Cost = 10;
    int _Soldier_Spear_Cost = 10;
    int _Soldier_Seven_Cost = 25;
    int _Soldier_Hero_Cost = 0;


    int _CopySummonCount;

    

    public void ChoiceHero(int num)
    {
        _MyHeroNumber = num;
        for (int i = 0; i < 4; i++)
        {
            UpgradeMng.Data._CheckNowHero[i].SetActive(false);
            _NowHeroSummonIcon[i].SetActive(false);
        }
        UpgradeMng.Data._CheckNowHero[_MyHeroNumber - 1].SetActive(true);
        _NowHeroSummonIcon[_MyHeroNumber - 1].SetActive(true);
        PlayerPrefs.SetInt("heronumber", _MyHeroNumber);
    }

    private void Start()
    {
        _UnitSummonInfoChecker = new List<bool>();
        ChoiceHero(_MyHeroNumber = PlayerPrefs.GetInt("heronumber", 1));
    }

    public void IngameStart(int stagenumber,bool  normal)
    {
        _Sword_CostText.text = _Soldier_Sword_Cost.ToString();
        _Archer_CostText.text = _Soldier_Archer_Cost.ToString();
        _Shield_CostText.text = _Soldier_Shield_Cost.ToString();
        _Spear_CostText.text = _Soldier_Spear_Cost.ToString();
        _Seven_CostText.text = _Soldier_Seven_Cost.ToString();

        _CopySummonCount = 0;

        _NowCost = 45;
        _CostTimer = 0;
        ChoiceHero(_MyHeroNumber);
        _NowSoldier_Sword_Delay = 0;
        _NowSoldier_Archer_Delay = 0;
        _NowSoldier_Shield_Delay = 0;
        _NowSoldier_Spear_Delay = 0;
        _NowSoldier_Seven_Delay = 0;
        _NowSoldier_Hero_Delay =  _MaxSoldier_Hero_Delay;
        _BaseEnemySummonLoopTime = 0;
        _SpecialEnemySummonLoopTime = 0;


        _NowStageNumber = stagenumber;
        _NowStageIsNormal = normal;
        
        _SpecialUnitSummonInfoList = _EnemyInfoMng._SpecialEnemySummonInfo[stagenumber - 1];
        _SPECIALENEMYSUMMONLOOPTIME_MAX = _EnemyInfoMng._UnitSpecialSummonLoopTime[stagenumber - 1];
        _NowStageSummonUnitCount = _SpecialUnitSummonInfoList.Count;
        _UnitSummonInfoChecker.Clear();
        for (int i = 0; i < _NowStageSummonUnitCount; i++)
            _UnitSummonInfoChecker.Add(false);


        CreateMyCastle();
        CreateEnemyCastle(stagenumber/5);
    }

    private void Update()
    {
        if (StageMng.Data.IngameStarted())
        {
            EnemyUnitSummonUpdate();

            CostTimerUpdate();
            SummonDelayUpdate();
            SummonButtonGreyUpdate();
        }
    }

    void EnemyUnitSummonUpdate()
    {

        _BaseEnemySummonLoopTime += Time.smoothDeltaTime;
        _SpecialEnemySummonLoopTime += Time.smoothDeltaTime;
        for (int i=0;i< _NowStageSummonUnitCount; i++)
        {
            if(_SpecialUnitSummonInfoList[i].GetSummonTime()<=_SpecialEnemySummonLoopTime &&
                !_UnitSummonInfoChecker[i])
            {
                _UnitSummonInfoChecker[i] = true;

                EnemyUnitSummon(_SpecialUnitSummonInfoList[i].GetInfo(),
                    _SpecialUnitSummonInfoList[i].GetSummonCount());
            }
        }
        if(_SpecialEnemySummonLoopTime >= _SPECIALENEMYSUMMONLOOPTIME_MAX)
        {
            _SpecialEnemySummonLoopTime = 0;
            for(int i=0;i< _NowStageSummonUnitCount;i++)
                _UnitSummonInfoChecker[i] = false;
        }
        if(_BaseEnemySummonLoopTime> _BASEENEMYSUMMONLOOPTIME_MAX)
        {
            _BaseEnemySummonLoopTime -= _BASEENEMYSUMMONLOOPTIME_MAX;
            CreateBaseEnemy(_EnemyInfoMng._StageBaseEnemyInfo[_NowStageNumber - 1]);
        }
    }


    void CostTimerUpdate()
    {
        _CostGaze.fillAmount = (float)_NowCost / _MaxCost;
        _CostText.text = _NowCost.ToString() + " / " + _MaxCost.ToString();
        _CostTimer += Time.smoothDeltaTime;
        if (_CostTimer >= _MaxCostTimer)
        {
            _CostTimer -= _MaxCostTimer;
            _NowCost += _AddingCost;
        }
        if (_NowCost >= _MaxCost)
            _NowCost = _MaxCost;
    }
    void SummonDelayUpdate()
    {
        _NowSoldier_Sword_Delay -= Time.smoothDeltaTime;
        _NowSoldier_Archer_Delay -= Time.smoothDeltaTime;
        _NowSoldier_Shield_Delay -= Time.smoothDeltaTime;
        _NowSoldier_Spear_Delay -= Time.smoothDeltaTime;
        _NowSoldier_Seven_Delay -= Time.smoothDeltaTime;
        _NowSoldier_Hero_Delay -= Time.smoothDeltaTime;

    }
    void SummonButtonGreyUpdate()
    {
        _Soldier_Sword_Grey.fillAmount = _NowSoldier_Sword_Delay / _MaxSoldier_Sword_Delay;
        _Soldier_Archer_Grey.fillAmount = _NowSoldier_Archer_Delay / _MaxSoldier_Archer_Delay;
        _Soldier_Shield_Grey.fillAmount = _NowSoldier_Shield_Delay / _MaxSoldier_Shield_Delay;
        _Soldier_Spear_Grey.fillAmount = _NowSoldier_Spear_Delay / _MaxSoldier_Spear_Delay;
        _Soldier_Seven_Grey.fillAmount = _NowSoldier_Seven_Delay / _MaxSoldier_Seven_Delay;
        _Soldier_Hero_Grey.fillAmount = _NowSoldier_Hero_Delay / _MaxSoldier_Hero_Delay;

    }

    public int GetNowCost() { return _NowCost; }
    public void AddNowCost(int n) { _NowCost += n; }
}
