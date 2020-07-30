using UnityEngine;
using System.Collections;
using UnityEngine.U2D;
using UnityEngine.UI;

public partial class GambleMng : MonoBehaviour
{
    private static GambleMng instance = null;
    public static GambleMng Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(GambleMng)) as GambleMng;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }

    [SerializeField] GameObject _GambleHighGrey;

    [SerializeField] GameObject _GambleDot;
    [SerializeField] Text _ChipText;
    [SerializeField] GameObject[] _GambleTableGray = new GameObject[3];
    [SerializeField] SpriteAtlas _CardFrontAtlas;
    int _Chip;
    readonly int _MAXCANGAMBLE = 3;

    //[SerializeField] GameObject[] _HelpTable = new GameObject[3];
    //[SerializeField] GameObject _HelpTable_Chip;

    [SerializeField] Animator _ResultPopup;
    [SerializeField] Text[] _CoinText = new Text[3];


    private void Start()
    {
        bool check = IntToBool(_Chip = PlayerPrefs.GetInt("chip", 0));
        for(int i=0;i< _MAXCANGAMBLE;i++)
            _GambleTableGray[i].SetActive(!check);

        


        //PlayerPrefs.SetInt("chip", 0);//test
        SetGambleTableGray();


        ChoiceGameDeckSet();
        BlackJackDeckSet();
        PokerDeckSet();

    }

    public int GetChip() { return _Chip; }
    public void AddChip(int n)
    {
        _Chip += n;
        PlayerPrefs.SetInt("chip", _Chip);
        SetGambleTableGray();
    }
    public void UseChip()
    {
        PlayerPrefs.SetInt("chip", --_Chip);
        SetGambleTableGray();
    }

    public void StageClear()
    {
        if (_Chip < 3)
            _Chip = 3;
        PlayerPrefs.SetInt("chip", _Chip);
        SetGambleTableGray();
    }

    void SetGambleTableGray()
    {
        bool check = IntToBool(_Chip = PlayerPrefs.GetInt("chip", 0));
        for (int i = 0; i < _MAXCANGAMBLE; i++)
            _GambleTableGray[i].SetActive(!check);
        _GambleDot.SetActive(_Chip > 0);
        _ChipText.text = _Chip.ToString();// + " / " + _MAXCANGAMBLE.ToString();
    }

    bool IntToBool(int n)
    {
        if (n == 0)
            return false;
        else
            return true;
    }

    //public void SetHelpTable_Choice(bool b)
    //{
    //    _HelpTable[0].SetActive(b);
    //}
    //public void SetHelpTable_BlackJack(bool b)
    //{
    //    _HelpTable[1].SetActive(b);
    //}
    //public void SetHelpTable_Poker(bool b)
    //{
    //    _HelpTable[2].SetActive(b);
    //}
    //
    //public void SetHelpTable_Chip(bool b)
    //{
    //    _HelpTable_Chip.SetActive(b);
    //}

    void GetCoinPopup(int coin, int herocoin, int shapecoin)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.Gamble_Result, 0);
        _ResultPopup.gameObject.SetActive(true);
        _ResultPopup.SetTrigger("get");

        _CoinText[0].text = "+ ";
        _CoinText[1].text = "+ ";
        _CoinText[2].text = "+ ";
        //_CoinText[0].gameObject.SetActive(false);
        //_CoinText[1].gameObject.SetActive(false);
        //_CoinText[2].gameObject.SetActive(false);
        
        UpgradeMng.Data.CoinUp(coin);
        UpgradeMng.Data.HeroCoinUp(herocoin);
        UpgradeMng.Data.ShapeCoinUp(shapecoin);

        if (coin > 0)
        {
            _CoinText[0].text += string.Format("{0:#,0}", coin);
            _CoinText[0].gameObject.SetActive(true);
        }
        if (herocoin > 0)
        {
            _CoinText[1].text += herocoin.ToString();
            _CoinText[1].gameObject.SetActive(true);
        }
        if (shapecoin > 0)
        {
            _CoinText[2].text += shapecoin.ToString();
            _CoinText[2].gameObject.SetActive(true);
        }
        StartCoroutine(ResultPopupOff());
    }
    IEnumerator ResultPopupOff()
    {
        yield return new WaitForSeconds(2.0f);
        _ResultPopup.gameObject.SetActive(false);
        _GambleHighGrey.SetActive(false);
    }

    string GetCardCode(int n)
    {
        string shape = "";
        int num;
        string ns="";

        if (n < 52)
        {
            switch (n / 13)
            {
                case 0:
                    shape = "s";
                    break;
                case 1:
                    shape = "h";
                    break;
                case 2:
                    shape = "d";
                    break;
                case 3:
                    shape = "c";
                    break;
            }
            num = n % 13 + 1;

            switch (num)
            {
                case 1:
                    ns = "a";
                    break;
                case 11:
                    ns = "j";
                    break;
                case 12:
                    ns = "q";
                    break;
                case 13:
                    ns = "k";
                    break;
                default:
                    ns = num.ToString();
                    break;
            }

            return shape + ns;
        }
        else
            return "jk";
    }
}