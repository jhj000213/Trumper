using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class GambleMng : MonoBehaviour
{
    enum Genealogy { Top, OnePair, TwoPair, Tripple, Straight, Mountain, Flush, FullHouse, FourCard, StraightFlush, RoyalStraightFlush };

    class PokerGenealogy
    {

        Genealogy _CardGenealogy;
        List<int> List;
        public PokerGenealogy(List<int> list)
        {
            _CardGenealogy = Genealogy.Top;
            List = list;

            IsRoyalStraightFlush();
            IsStraightFlush();
            IsMountain();
            IsStraight();
            IsFourCard();
            IsFullHouse();
            IsFlush();
            IsTripple();
            IsTwoPair();
            IsOnePair();
        }
        void IsRoyalStraightFlush()
        {
            if (IsFlush() && IsMountain())
                _CardGenealogy = Genealogy.RoyalStraightFlush;
        }
        void IsStraightFlush()
        {
            if (IsFlush() && IsStraight())
                _CardGenealogy = Max(Genealogy.StraightFlush, _CardGenealogy);
        }
        void IsFourCard()
        {
            if ((List[1] % 13 == List[2] % 13 && List[2] % 13 == List[3] % 13) &&
                (List[0] % 13 == List[1] % 13 || List[3] % 13 == List[4] % 13))
                _CardGenealogy = Max(Genealogy.FourCard, _CardGenealogy);
        }
        void IsFullHouse()
        {
            if ((List[0] % 13 == List[1] % 13 && List[1] % 13 == List[2] % 13) && (List[3] % 13 == List[4] % 13) ||
                (List[0] % 13 == List[1] % 13) && (List[2] % 13 == List[3] % 13 && List[3] % 13 == List[4] % 13))
                _CardGenealogy = Max(Genealogy.FullHouse, _CardGenealogy);
        }
        bool IsFlush()
        {
            if ((List[0] / 13) == (List[1] / 13) &&
                (List[1] / 13) == (List[2] / 13) &&
                (List[2] / 13) == (List[3] / 13) &&
                (List[3] / 13) == (List[4] / 13))//Flush
            {
                _CardGenealogy = Max(Genealogy.Flush, _CardGenealogy);
                return true;
            }
            return false;
        }
        bool IsMountain()
        {
            if (List[0] % 13 == 0 && List[1] % 13 == 9 && List[2] % 13 == 10 && List[3] % 13 == 11 && List[4] % 13 == 12)
            {
                _CardGenealogy = Max(Genealogy.Mountain, _CardGenealogy);
                return true;
            }
            return false;
        }
        bool IsStraight()
        {
            if (_StraightFunc(0))
            {
                _CardGenealogy = Max(Genealogy.Straight, _CardGenealogy);
                return true;
            }
            return false;
        }

        void IsTripple()
        {
            if ((List[0] % 13 == List[1] % 13 && List[1] % 13 == List[2] % 13) ||
                (List[1] % 13 == List[2] % 13 && List[2] % 13 == List[3] % 13) ||
                (List[2] % 13 == List[3] % 13 && List[3] % 13 == List[4] % 13))
                _CardGenealogy = Max(Genealogy.Tripple, _CardGenealogy);
        }
        void IsTwoPair()
        {
            if (((List[0] % 13 == List[1] % 13) && (List[2] % 13 == List[3] % 13)) ||
                ((List[0] % 13 == List[1] % 13) && (List[3] % 13 == List[4] % 13)) ||
                ((List[1] % 13 == List[2] % 13) && (List[3] % 13 == List[4] % 13)))
                _CardGenealogy = Max(Genealogy.TwoPair, _CardGenealogy);
        }
        void IsOnePair()
        {
            if ((List[0] % 13 == List[1] % 13) || (List[1] % 13 == List[2] % 13) ||
                (List[2] % 13 == List[3] % 13) || (List[3] % 13 == List[4] % 13))
                _CardGenealogy = Max(Genealogy.OnePair, _CardGenealogy);
        }

        bool _StraightFunc(int n)
        {
            if (n == 4)
                return true;
            if (List[n] % 13 == (List[n + 1] % 13) - 1)
                return _StraightFunc(n + 1);
            else
                return false;
        }
        Genealogy Max(Genealogy a, Genealogy b) { return a > b ? a : b; }
        public int GetGenealogy()
        {
            //Debug.Log(_CardGenealogy);
            return (int)_CardGenealogy;
        }
        public static string GetKoreanText(int num)
        {
            string text="";

            Genealogy genealogy = (Genealogy)num;

            if (genealogy == Genealogy.Top)
                text = "탑";
            else if (genealogy == Genealogy.OnePair)
                text = "원 페어";
            else if (genealogy == Genealogy.TwoPair)
                text = "투 페어";
            else if (genealogy == Genealogy.Tripple)
                text = "트리플";
            else if (genealogy == Genealogy.Straight)
                text = "스트레이트";
            else if (genealogy == Genealogy.Mountain)
                text = "마운틴";
            else if (genealogy == Genealogy.Flush)
                text = "플러시";
            else if (genealogy == Genealogy.FullHouse)
                text = "풀 하우스";
            else if (genealogy == Genealogy.FourCard)
                text = "포 카드";
            else if (genealogy == Genealogy.StraightFlush)
                text = "스트레이트 플러시";
            else if (genealogy == Genealogy.RoyalStraightFlush)
                text = "로얄 스트레이트 플러시";

            return text;
        }
    }

    const int _Poker_MaxCardCount_Max = 5;
    [SerializeField] GameObject[] _Poker_Cards = new GameObject[_Poker_MaxCardCount_Max];
    GambleCard_Poker[] _Poker_Cards_Comp = new GambleCard_Poker[_Poker_MaxCardCount_Max];
    PokerGenealogy _PokerGenealogy;

    [SerializeField] GameObject _PokerStartButton;
    [SerializeField] GameObject _PokerTable;
    [SerializeField] GameObject _PokerCardChangeButtonTable;
    [SerializeField] Text _PokerUseCardNumText;
    [SerializeField] GameObject _PokerEndButton;
    [SerializeField] Text _PokerGenealogyText;

    List<int> _Poker_UseCardNum = new List<int>();
    List<int> _Poker_HandCardNum = new List<int>();
    int _Poker_ChangeAbleCount;
    int _Poker_ChangeAbleCount_Max = 3;//렙오를수록 업?
    [SerializeField] Text _ChangeAbleCountText;

    [SerializeField] GameObject _GenealogyImage;

    public void SetGenealogyImage(bool b)
    {
        _GenealogyImage.SetActive(b);
    }

    void PokerDeckSet()
    {
        for (int i = 0; i < _Poker_MaxCardCount_Max; i++)
        {
            _Poker_Cards_Comp[i] = _Poker_Cards[i].GetComponent<GambleCard_Poker>();
            _Poker_Cards[i].SetActive(false);
        }
    }

    public void Poker_Start()
    {
        if (_Chip > 0 && StageMng.Data._NowClearStage > 0)
        {
            UseChip();
            _GambleHighGrey.SetActive(true);

            _Poker_UseCardNum.Clear();
            _Poker_HandCardNum.Clear();
            _PokerStartButton.SetActive(false);
            _PokerTable.SetActive(true);
            _PokerCardChangeButtonTable.SetActive(false);
            _PokerEndButton.SetActive(false);
            _Poker_ChangeAbleCount = 0;
            _PokerUseCardNumText.text = "";
            _PokerGenealogyText.text = "";
            _ChangeAbleCountText.text = "남은 교체 횟수 : " + (3 - _Poker_ChangeAbleCount);

            float width = 275;

            for (int i = 0; i < _Poker_MaxCardCount_Max; i++)
            {
                _Poker_Cards[i].SetActive(false);
                _Poker_Cards_Comp[i].CardInit();
            }
            for (int i = 0; i < _Poker_MaxCardCount_Max; i++)
            {
                float speed = 6;
                _Poker_Cards[i].SetActive(true);
                _Poker_Cards[i].transform.localPosition = new Vector3(0, 0, 0);
                if (i != 0)
                    _Poker_Cards_Comp[i].MoveCard(new Vector3(width * i, 0, 0), width * speed, (1 / speed) * i);
            }
            StartCoroutine(Poker_OpenCard(1 + 0.2f));//날라가는속도+추가 여분
        }
        SetGambleTableGray();
    }

    IEnumerator Poker_OpenCard(float time)
    {
        yield return new WaitForSeconds(time);

        for (int i = 0; i < _Poker_MaxCardCount_Max; i++)
        {
            int num = Poker_MakeCardNum();
            _Poker_HandCardNum.Add(num);


            _Poker_Cards_Comp[i].OpenCard(_CardFrontAtlas.GetSprite(GetCardCode(num)));

        }
        StartCoroutine(Poker_ChangeCardDelay(0.5f));
    }

    public void Poker_ChangeCard(int n)
    {
        if (_Poker_ChangeAbleCount < _Poker_ChangeAbleCount_Max)
        {
            _Poker_ChangeAbleCount++;
            _ChangeAbleCountText.text = "남은 교체 횟수 : " + (3 - _Poker_ChangeAbleCount);
            int num = Poker_MakeCardNum();
            _Poker_HandCardNum.RemoveAt(n);
            _Poker_HandCardNum.Insert(n, num);
            _Poker_Cards_Comp[n].ChangeThisCard(_CardFrontAtlas.GetSprite(GetCardCode(num)));


            StartCoroutine(Poker_ChangeCardDelay(0.5f));
        }
    }
    IEnumerator Poker_ChangeCardDelay(float time)
    {
        _PokerCardChangeButtonTable.SetActive(false);
        _PokerEndButton.SetActive(false);
        yield return new WaitForSeconds(time);
        
        _PokerGenealogyText.text = PokerGenealogy.GetKoreanText(GetHandGenealogy());

        _PokerCardChangeButtonTable.SetActive(true);
        _PokerEndButton.SetActive(true);
    }

    int Poker_MakeCardNum()
    {
        int cardnum = 0;
        bool check = false;
        while (!check)
        {
            cardnum = Random.Range(0, 52);
            int ct = cardnum % 13;
            check = ct == 0 || (7 <= ct && ct <= 12);
            for (int i = 0; i < _Poker_UseCardNum.Count; i++)
            {
                if (_Poker_UseCardNum[i] == cardnum)
                    check = false;
            }
        }
        _Poker_UseCardNum.Add(cardnum);
        _PokerUseCardNumText.text += GetCardCode(cardnum) + "   ";
        return cardnum;
    }

    public void Poker_SettingEnd()
    {
        if (_Poker_HandCardNum.Count >= 5)
        {
            _Poker_ChangeAbleCount = _Poker_ChangeAbleCount_Max;
            int resultnum = GetHandGenealogy();
            Poker_GetResult(resultnum);
            _Poker_HandCardNum.Clear();
            StartCoroutine(Poker_EndGame());
        }
    }

    void Poker_GetResult(int resultnum)
    {
        int c = StageMng.Data._ClearRewardMax[StageMng.Data._NowClearStage - 1];
        if (resultnum == (int)Genealogy.Top)
        {
            GetCoinPopup((int)(c * 0.5f), 0,0);
            AchievementMng.Data.AddAchievementCount(44);
        }
        else if (resultnum == (int)Genealogy.OnePair)
        {
            GetCoinPopup((int)(c * 0.8f), 0, 0);
            AchievementMng.Data.AddAchievementCount(45);
        }
        else if (resultnum == (int)Genealogy.TwoPair)
        {
            GetCoinPopup((int)(c * 1.0f), 0, 0);
            AchievementMng.Data.AddAchievementCount(46);
        }
        else if (resultnum == (int)Genealogy.Tripple)
        {
            GetCoinPopup((int)(c * 1.1f), 0, 0);
            AchievementMng.Data.AddAchievementCount(47);
        }
        else if (resultnum == (int)Genealogy.Straight)
        {
            GetCoinPopup((int)(c * 1.3f), 1, 0);
            AchievementMng.Data.AddAchievementCount(48);
        }
        else if (resultnum == (int)Genealogy.Mountain)
        {
            GetCoinPopup((int)(c * 1.4f), 0, 1);
            AchievementMng.Data.AddAchievementCount(49);
        }
        else if (resultnum == (int)Genealogy.Flush)
        {
            GetCoinPopup((int)(c * 1.5f), 1, 1);
            AchievementMng.Data.AddAchievementCount(50);
        }
        else if (resultnum == (int)Genealogy.FullHouse)
        {
            GetCoinPopup((int)(c * 1.7f), 1, 1);
            AchievementMng.Data.AddAchievementCount(51);
        }
        else if (resultnum == (int)Genealogy.FourCard)
        {
            GetCoinPopup((int)(c * 2.0f), 2, 2);
            AchievementMng.Data.AddAchievementCount(52);
        }
        else if (resultnum == (int)Genealogy.StraightFlush)
        {
            GetCoinPopup((int)(c * 2.5f), 3, 3);
            AchievementMng.Data.AddAchievementCount(53);
        }
        else if (resultnum == (int)Genealogy.RoyalStraightFlush)
        {
            GetCoinPopup((int)(c * 3.0f), 5, 5);
            AchievementMng.Data.AddAchievementCount(54);
        }
    }

    //Top, OnePair, TwoPair, Tripple, Straight, Mountain, 
    //Flush, FullHouse, FourCard, StraightFlush, RoyalStraightFlush


    int GetHandGenealogy()
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < 5; i++)
            temp.Add(_Poker_HandCardNum[i]);

        //temp[0] = 12;
        //temp[1] = 8;
        //temp[2] = 9;
        //temp[3] = 10;
        //temp[4] = 11;

        temp.Sort(delegate (int a, int b)
        {
            if (a % 13 > b % 13)
                return 1;
            else if (a % 13 < b % 13)
                return -1;
            else
            {
                if (a < b)
                    return 1;
                else
                    return -1;
            }
        });
        string str = "";
        for (int i = 0; i < 5; i++)
            str += GetCardCode(temp[i]) + " ";
        //Debug.Log("Sort : " + str);
        str = "";
        for (int i = 0; i < 5; i++)
            str += GetCardCode(_Poker_HandCardNum[i]) + " ";
        //Debug.Log("Hand : " + str);
        return new PokerGenealogy(temp).GetGenealogy();
    }

    IEnumerator Poker_EndGame()
    {
        yield return new WaitForSeconds(1.0f);


        _PokerTable.SetActive(false);
        _PokerStartButton.SetActive(true);
    }
}