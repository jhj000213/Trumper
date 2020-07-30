using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoldierShapeBuffMng : MonoBehaviour
{
    private static SoldierShapeBuffMng instance = null;
    public static SoldierShapeBuffMng Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(SoldierShapeBuffMng)) as SoldierShapeBuffMng;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }


    [SerializeField] GameObject _ShapeButtonList;

    [SerializeField] Image _ShapeButtonGrey;
    [SerializeField] GameObject[] _ShapeButtonIcon;

    [SerializeField] GameObject _SevenUnitOn;

    [SerializeField] Animator _GetCostAnimator;

    float _MaxChangeDelay = 15.0f;
    float _StartChangeDelay = 15;
    float _NowChangeDelay;

    /// <summary>
    /// 0 - ♠    
    /// 1 - ♥    
    /// 2 - ◆    
    /// 3 - ♣    
    /// </summary>
    bool[] _ShapeOn = { false, false, false, false };
    int _NowBuffShape;

    bool _SpadeOn;
    bool _HeartOn;
    bool _DiamondOn;
    bool _ClubOn;

    float _NowHeartDelayTime;
    float _MaxHeartDelayTime = 5.0f;

    float _NowDiamondDelayTime;
    float _MaxDiamondDelayTime = 7.0f;

    [SerializeField] UnitMng _UnitMng;
    [SerializeField] UnitSummonMng _UnitSummonMng;


    public List<int> _SpadeData = new List<int>();
    public List<int> _HeartData = new List<int>();
    public List<int> _DiamondData = new List<int>();
    public List<int> _ClubData = new List<int>();

    [SerializeField] GameObject[] _SevenOnImage;

    int[,] _LevelArr = new int[4, 2];

    private void Awake()
    {
        GetShapeData();
    }

    void GetShapeData()
    {
        List<Dictionary<string, object>> status = CSVReader.Read("ShapeStatus");
        int max = (int)status[0]["Max"];
        for (int i = 0; i < max; i++)
        {
            _SpadeData.Add((int)status[i]["Spade"]);
            _HeartData.Add((int)status[i]["Heart"]);
            _DiamondData.Add((int)status[i]["Diamond"]);
            _ClubData.Add((int)status[i]["Club"]);
            UpgradeMng.Data._ShapePrice.Add((int)status[i]["price"]);
        }
        UpgradeMng.Data._LEVEL_SHAPE_MAX = max;
    }

    public void IngameStart()
    {
        _ShapeButtonList.SetActive(false);
        SetShapeElseMng(-1);
        _NowChangeDelay = _StartChangeDelay;

        _ShapeButtonIcon[0].SetActive(true);
        for (int i = 1; i < 5; i++)
        {
            _ShapeButtonIcon[i].SetActive(false);
            _ShapeOn[i - 1] = false;
        }
        SetRankInit();
        SetRankShapeLevel();
    }



    private void Update()
    {
        _NowChangeDelay -= Time.smoothDeltaTime;
        _ShapeButtonGrey.fillAmount = _NowChangeDelay / _MaxChangeDelay;


        if (_NowBuffShape == 1 || GetShapeOn_Seven_Heart())
        {
            _NowHeartDelayTime += Time.smoothDeltaTime;
            if (_NowHeartDelayTime >= _MaxHeartDelayTime)
            {
                Debug.Log("heart");
                _NowHeartDelayTime = 0;
                _UnitMng.UsingHeartAbility(_HeartData[UpgradeMng.Data._Level_Shape_Heart - 1]);
            }
        }
        if (_NowBuffShape == 2 || GetShapeOn_Seven_Diamond())
        {
            _NowDiamondDelayTime += Time.smoothDeltaTime;
            if (_NowDiamondDelayTime >= _MaxDiamondDelayTime)
            {
                _NowDiamondDelayTime = 0;
                _UnitSummonMng.AddNowCost(_DiamondData[UpgradeMng.Data._Level_Shape_Diamond - 1]);
                _GetCostAnimator.gameObject.SetActive(true);
                _GetCostAnimator.SetTrigger("getcost");
                StartCoroutine(GetCostAnimator_Off());
            }
        }
        if(UnitMng.Data.GetSoldierSevenAlive())
        {
            _SevenUnitOn.SetActive(true);
            _SevenOnImage[0].SetActive(GetShapeOn_Seven_Spade());
            _SevenOnImage[1].SetActive(GetShapeOn_Seven_Heart());
            _SevenOnImage[2].SetActive(GetShapeOn_Seven_Diamond());
            _SevenOnImage[3].SetActive(GetShapeOn_Seven_Club());

            _SevenOnImage[_NowBuffShape].SetActive(true);
        }
        else
            _SevenUnitOn.SetActive(false);
    }

    IEnumerator GetCostAnimator_Off()
    {
        yield return new WaitForSeconds(1.0f);
        _GetCostAnimator.gameObject.SetActive(false);
    }

    public void ShapeButtonListOnOff()
    {
        if (_NowChangeDelay < 0.0f)
            _ShapeButtonList.SetActive(!_ShapeButtonList.activeSelf);
    }

    public void ShapeChange(int num)
    {
        if (_NowBuffShape != num)
        {
            SetShapeElseMng(num);
            SetRankShapeLevel();
            _ShapeButtonIcon[0].SetActive(false);
            for (int i = 0; i < 4; i++)
            {
                _ShapeButtonIcon[i + 1].SetActive(false);
                _ShapeOn[i] = false;
            }
            _ShapeOn[_NowBuffShape] = true;
            _ShapeButtonIcon[num + 1].SetActive(true);
            _NowChangeDelay = _MaxChangeDelay;
        }
        _ShapeButtonList.SetActive(false);
    }

    void SetShapeElseMng(int shape)
    {
        _NowBuffShape = shape;
        _NowHeartDelayTime = 0;
        _NowDiamondDelayTime = 0;
        _UnitMng.SetShapeAbility(_NowBuffShape);
    }

    public int GetNowShapeNum() { return _NowBuffShape; }

    public bool GetShapeOn_Seven_Spade() { return _SpadeOn && UnitMng.Data.GetSoldierSevenAlive(); }
    public bool GetShapeOn_Seven_Heart() { return _HeartOn && UnitMng.Data.GetSoldierSevenAlive(); }
    public bool GetShapeOn_Seven_Diamond() { return _DiamondOn && UnitMng.Data.GetSoldierSevenAlive(); }
    public bool GetShapeOn_Seven_Club() { return _ClubOn && UnitMng.Data.GetSoldierSevenAlive(); }

    void SetRankInit()
    {
        for (int i = 0; i < 4; i++)
            _LevelArr[i, 0] = i;
        _LevelArr[0, 1] = UpgradeMng.Data._Level_Shape_Spade;
        _LevelArr[1, 1] = UpgradeMng.Data._Level_Shape_Heart;
        _LevelArr[2, 1] = UpgradeMng.Data._Level_Shape_Diamond;
        _LevelArr[3, 1] = UpgradeMng.Data._Level_Shape_Club;

        for (int i = 0; i < 3; i++)
        {
            for (int j = i + 1; j < 4; j++)
            {
                if (_LevelArr[i, 1] < _LevelArr[j, 1] || (_LevelArr[i, 1] == _LevelArr[j, 1] && _LevelArr[i, 0] > _LevelArr[j, 0]))
                {
                    int t = _LevelArr[i, 0];
                    _LevelArr[i, 0] = _LevelArr[j, 0];
                    _LevelArr[j, 0] = t;

                    t = _LevelArr[i, 1];
                    _LevelArr[i, 1] = _LevelArr[j, 1];
                    _LevelArr[j, 1] = t;
                }
            }
        }
    }

    public void SetRankShapeLevel()
    {
        _SpadeOn = false;
        _HeartOn = false;
        _DiamondOn = false;
        _ClubOn = false;

        
        int add = 1;
        for (int i = 0; i < UpgradeMng.Data._SkillLevel_Soldier_Seven + add; i++)
        {
            if (_LevelArr[i, 0] == _NowBuffShape)
            {
                add++;
                continue;
            }
            if (_LevelArr[i, 0] == 0)
                _SpadeOn = true;
            else if (_LevelArr[i, 0] == 1)
                _HeartOn = true;
            else if (_LevelArr[i, 0] == 2)
                _DiamondOn = true;
            else if (_LevelArr[i, 0] == 3)
                _ClubOn = true;
        }
    }
    
}
