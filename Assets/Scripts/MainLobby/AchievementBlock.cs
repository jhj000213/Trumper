using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementBlock : MonoBehaviour
{
    AchievementMng _Mng;
    int _Number;
    

    [SerializeField] Text _Name;
    [SerializeField] Text _Text;
    [SerializeField] Text _ClearCount;

    [SerializeField] GameObject _IfValue1;
    [SerializeField] GameObject _IfValue2;

    [SerializeField] GameObject[] _Value1_1I;
    [SerializeField] Text _Value1_1;
    [SerializeField] GameObject[] _Value2_1I;
    [SerializeField] Text _Value2_1;
    [SerializeField] GameObject[] _Value2_2I;
    [SerializeField] Text _Value2_2;


    [SerializeField] Image _TypeImage;

    [SerializeField] Sprite _ClearButtonSprite;
    string _ClearCountType;
    bool _Clear;
    int _NowClearing;
    int _MaxClearCount;
    bool _OverMax;
    string _Type;

    [SerializeField] Image _ClearButton;
    [SerializeField] GameObject _ClearGrey;

    Dictionary<string, Color> _TypeColor = new Dictionary<string, Color>();

    void ColorInit()
    {
        _TypeColor.Add("soldierlevel", new Color(0.3584f, 0.3584f, 0.3584f));
        _TypeColor.Add("hero", new Color(0.4425f, 0.8301f, 0.4452f));
        _TypeColor.Add("bossclear", new Color(1, 0, 0));
        _TypeColor.Add("shape", new Color(0, 0, 0));
        _TypeColor.Add("choicegame", new Color(1, 0.5f, 0));
        _TypeColor.Add("blackjack", new Color(1, 0.5f, 0));
        _TypeColor.Add("poker", new Color(1, 0.5f, 0));

        _TypeImage.color = _TypeColor[_Type];
    }

    public void Init(AchievementMng mng,int number,string type, string name, string text, int clearcount,string clearcounttype, int resulttype1, int resultvalue1, int resulttype2, int resultvalue2,int nowclear ,bool clear,bool overmax)
    {
        _Mng = mng;
        _Number = number;
        _Type = type;
        ColorInit();
        _Name.text = name;
        _Text.text = text;
        _ClearCount.text = nowclear.ToString() + " / "+clearcount.ToString() + clearcounttype;
        _ClearCountType = clearcounttype;

        if(resulttype2!=0)
        {
            _IfValue1.SetActive(false);
            _IfValue2.SetActive(true);
            _Value2_1.text = resultvalue1.ToString();
            _Value2_2.text = resultvalue2.ToString();

            _Value2_1I[resulttype1 - 1].SetActive(true);
            _Value2_2I[resulttype2 - 1].SetActive(true);
        }
        else
        {
            _IfValue1.SetActive(true);
            _IfValue2.SetActive(false);
            _Value1_1.text = resultvalue1.ToString();

            _Value1_1I[resulttype1 - 1].SetActive(true);
        }

        _NowClearing = nowclear;
        _MaxClearCount = clearcount;
        _Clear = clear;
        _OverMax = overmax;
        ClearCheck();
    }

    public void AddClearing()
    {
        if (!_Clear)
        {
            _NowClearing++;
            PlayerPrefs.SetInt("achievenow" + _Number.ToString(), _NowClearing);
            ClearCheck();
        }
    }
    
    public void ClickClearButton()
    {
        if (_NowClearing >= _MaxClearCount)
        {
            GetResult();
        }
    }

    public bool GetOverMax()
    {
        if (!_OverMax && _NowClearing >= _MaxClearCount && !_Clear)
        {
            _OverMax = true;
            return true;
        }
        return false;
    }

    void GetResult()
    {
        SoundMng.Data.PlayEffectSound(EffectSound.Achievement_Clear, 0);
        _Clear = true;
        _Mng.ClearAchievement(_Number);
        ClearCheck();
    }

    void ClearCheck()
    {
        _ClearGrey.SetActive(_Clear);
        if (_NowClearing >= _MaxClearCount)
        {
            _NowClearing = _MaxClearCount;
            //_ClearButton.color = new Color(1f, 0.8113828f, 0.3820755f);
            _ClearButton.sprite = _ClearButtonSprite;
        }
        _ClearCount.text = _NowClearing.ToString() + " / " + _MaxClearCount.ToString() + _ClearCountType;
    }
}
