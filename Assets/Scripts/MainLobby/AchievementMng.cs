using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementMng : MonoBehaviour
{
    private static AchievementMng instance = null;
    public static AchievementMng Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(AchievementMng)) as AchievementMng;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }

    List<AchievementBlock> _Blocks = new List<AchievementBlock>();

    [SerializeField] GameObject _Block;
    [SerializeField] GameObject _ScrollViewContent;
    [SerializeField] RectTransform _ScrollViewContentRect;

    int _AchievementCount = 10;
    const int _BlockWidth = 1400;
    const int _BlockHeight = 130;
    const int _BlockPadding = 10;
    float _BlockPos;

    [SerializeField] Animator _AchievementClearAnimator;
    [SerializeField] Text _AchievementClearText;
    [SerializeField] GameObject _EyeCheckerRedDot;

    List<string> _AchievementType = new List<string>();
    List<string> _AchievementName = new List<string>();
    List<string> _AchievementText = new List<string>();
    List<int> _AchievementClearCount = new List<int>();
    List<string> _AchievementClearCountType = new List<string>();
    List<int> _ResultType1 = new List<int>();
    List<int> _ResultType2 = new List<int>();
    List<int> _ResultValue1 = new List<int>();
    List<int> _ResultValue2 = new List<int>();
    
    bool _EyeCheck;

    private void Awake()
    {
        EyeCheckerSet(ItoB(PlayerPrefs.GetInt("achieveeyecheck", 0)));
        AddAchievementContent();

        int height = _AchievementCount * (_BlockHeight + _BlockPadding);
        _ScrollViewContentRect.sizeDelta = new Vector2(1400, height);
        _BlockPos = 0 - (_BlockHeight / 2) - (_BlockPadding / 2);

        for (int i = 0; i < _AchievementCount; i++)
        {
            GameObject obj = Instantiate(_Block, _ScrollViewContent.transform);
            obj.transform.localPosition = new Vector3(_BlockWidth / 2, _BlockPos - 140 * i);
            AchievementBlock block = obj.GetComponent<AchievementBlock>();
            block.Init(this, i, _AchievementType[i], _AchievementName[i], _AchievementText[i],
                _AchievementClearCount[i], _AchievementClearCountType[i],
                _ResultType1[i], _ResultValue1[i], _ResultType2[i], _ResultValue2[i],
                _AchievementType[i] == "soldierlevel" || _AchievementType[i] == "shape" 
                ? PlayerPrefs.GetInt("achievenow" + i.ToString(), 1) : PlayerPrefs.GetInt("achievenow" + i.ToString(), 0),
                ItoB(PlayerPrefs.GetInt("achieveclear" + i.ToString(), 0)),
                ItoB(PlayerPrefs.GetInt("achieveovermax" + i.ToString(), 0)));
            _Blocks.Add(block);
        }
    }

    void AddAchievementContent()
    {
        List<Dictionary<string, object>> achieve = CSVReader.Read("AchievementList");
        int max = (int)achieve[0]["countmax"];
        _AchievementCount = max;
        Debug.Log(_AchievementCount);
        for (int i = 0; i < max; i++)
        {
            _AchievementType.Add((string)achieve[i]["type"]);
            _AchievementName.Add((string)achieve[i]["name"]);
            _AchievementText.Add((string)achieve[i]["text"]);
            _AchievementClearCount.Add((int)achieve[i]["clearcount"]);
            _AchievementClearCountType.Add((string)achieve[i]["clearcounttype"]);
            _ResultType1.Add((int)achieve[i]["resulttype1"]);
            _ResultValue1.Add((int)achieve[i]["resultvalue1"]);
            _ResultType2.Add((int)achieve[i]["resulttype2"]);
            _ResultValue2.Add((int)achieve[i]["resultvalue2"]);
        }
    }
    public void AddAchievementCount(int index)
    {
        _Blocks[index].AddClearing();
        if (_Blocks[index].GetOverMax())
        {
            EyeCheckerSet(true);
            PlayerPrefs.SetInt("achieveovermax" + index.ToString(), 1);

            _AchievementClearAnimator.SetTrigger("achievementclear");
            _AchievementClearText.text = _AchievementName[index];
        }
    }

    public void ClearAchievement(int index)
    {
        PlayerPrefs.SetInt("achieveclear" + index.ToString(), 1);
        GetResult(index);
    }

    void GetResult(int index)
    {
        Debug.Log(index);
        switch (_ResultType1[index])
        {
            case 1:
                UpgradeMng.Data.CoinUp(_ResultValue1[index]);
                break;
            case 2:
                UpgradeMng.Data.HeroCoinUp(_ResultValue1[index]);
                break;
            case 3:
                UpgradeMng.Data.ShapeCoinUp(_ResultValue1[index]);
                break;
            case 4:
                UpgradeMng.Data.SkillCoinUp(_ResultValue1[index]);
                break;
        }
        switch (_ResultType2[index])
        {
            case 1:
                UpgradeMng.Data.CoinUp(_ResultValue1[index]);
                break;
            case 2:
                UpgradeMng.Data.HeroCoinUp(_ResultValue1[index]);
                break;
            case 3:
                UpgradeMng.Data.ShapeCoinUp(_ResultValue1[index]);
                break;
            case 4:
                UpgradeMng.Data.SkillCoinUp(_ResultValue1[index]);
                break;
        }
    }

    public void OpenAchievementTab()
    {
        EyeCheckerSet(false);
    }

    void EyeCheckerSet(bool b)
    {
        PlayerPrefs.SetInt("achieveeyecheck", BtoI(b));
        _EyeCheck = b;
        _EyeCheckerRedDot.SetActive(b);
    }
    int BtoI(bool b) { return b ? 1 : 0; }
    bool ItoB(int n) { return n==0? false: true; }
}
