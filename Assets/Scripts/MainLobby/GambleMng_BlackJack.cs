using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GambleMng : MonoBehaviour
{
    [SerializeField] GameObject _BlackJack_Deck;
    [SerializeField] GameObject _BlackJack_Card;

    [SerializeField] GameObject _BlackJack_CardTable;
    [SerializeField] GameObject _BlackJack_MoveCard;

    [SerializeField] GameObject _BlackJackStartButton;
    [SerializeField] GameObject _BlackJackTable;
    [SerializeField] GameObject _BlackJack_Setbutton;
    [SerializeField] Text _BlackJack_MostNum_Text;
    List<int> _BlackJackGetNumber = new List<int>();
    List<GameObject> _BlackJack_MoveObj = new List<GameObject>();
    int _BlackJack_MostNumber;
    bool _BlackJack_BlackJack;
    bool _BlackJack_CanAddCard;
    bool _BlackJack_IsCardMove;

    [SerializeField] GameObject _DeckPosition;

    readonly float _OpenSpeed = 2.0f;

    void BlackJackDeckSet()
    {
        float dis = 1;
        for (int i = 0; i < 52; i++)
        {
            GameObject obj = Instantiate(_BlackJack_Card, _BlackJack_Deck.transform);
            obj.transform.localPosition = new Vector3(0, i * dis, 0);
        }
    }

    public void BlackJackGameStart()
    {
        if (_Chip > 0 && StageMng.Data._NowClearStage>0)
        {
            UseChip();
            _GambleHighGrey.SetActive(true);
            _BlackJack_MostNum_Text.text = "0";
            _BlackJack_IsCardMove = false;
            _BlackJack_BlackJack = false;
            _BlackJack_CanAddCard = true;
            _BlackJack_Setbutton.SetActive(false);
            _BlackJackTable.SetActive(true);
            _BlackJackStartButton.SetActive(false);
            _BlackJackGetNumber.Clear();
        }
        SetGambleTableGray();
    }

    public void BlackJack_ChoiceCard(GambleCard card)
    {
        if (_BlackJackGetNumber.Count < 10 && _BlackJack_CanAddCard && !_BlackJack_IsCardMove)
        {
            int cardnum;
            bool check;
            while (true)
            {
                check = true;
                cardnum = Random.Range(0, 52);
                for (int i = 0; i < _BlackJackGetNumber.Count; i++)
                {
                    if (_BlackJackGetNumber[i] == cardnum)
                        check = false;
                }
                if (check)
                    break;
            }
            _BlackJackGetNumber.Add(cardnum % 13 + 1);
            GameObject obj = Instantiate(_BlackJack_MoveCard, _BlackJack_CardTable.transform);
            obj.transform.localPosition = _DeckPosition.transform.localPosition;
            obj.GetComponent<GambleCard_BlackJack>().OpenCard(_CardFrontAtlas.GetSprite(GetCardCode(cardnum)), _BlackJackGetNumber.Count - 1, _OpenSpeed);
            _BlackJack_MoveObj.Add(obj);
            _BlackJack_IsCardMove = true;
            StartCoroutine(BlackJack_ScoreCheck());

            if (_BlackJackGetNumber.Count >= 2)
                _BlackJack_Setbutton.SetActive(true);
        }
    }

    IEnumerator BlackJack_ScoreCheck()
    {

        List<int> num = new List<int>();
        List<bool> blackjack = new List<bool>();
        num.Add(0);
        blackjack.Add(true);
        for (int i = 0; i < _BlackJackGetNumber.Count; i++)
        {
            int t = num.Count;
            for (int n = 0; n < t; n++)
            {
                if (_BlackJackGetNumber[i] == 1)
                {
                    num.Add(num[n] + 11);
                    blackjack.Add(true);

                    num[n] += 1;
                    blackjack[n] = false;
                }
                else if (_BlackJackGetNumber[i] >= 11)
                {
                    num[n] += 10;
                }
                else
                {
                    num[n] += _BlackJackGetNumber[i];
                    blackjack[n] = false;
                }
            }
        }
        num.Sort();
        int most = num[0];
        bool mostb = blackjack[0];
        for (int i = 1; i < num.Count; i++)
        {
            if ((num[i] <= 21 ? 21 - num[i] : 99) < (most <= 21 ? 21 - most : 99))
            {
                most = num[i];
                mostb = blackjack[i];
            }
        }
        _BlackJack_BlackJack = mostb;
        _BlackJack_MostNumber = most;

        yield return new WaitForSeconds(1.0f/_OpenSpeed + 0.5f);
        _BlackJack_IsCardMove = false;
        _BlackJack_MostNum_Text.text = most.ToString();// + (_BlackJack_BlackJack ? "BlackJack!" : "");
        if (most >= 21)
            _BlackJack_CanAddCard = false;
    }

    public void BlackJack_SettingEnd()
    {
        if (_BlackJackGetNumber.Count >= 2 && !_BlackJack_IsCardMove)
        {
            if(_BlackJackGetNumber.Count>=4)
                AchievementMng.Data.AddAchievementCount(43);
            int c = StageMng.Data._ClearRewardMax[StageMng.Data._NowClearStage - 1];

            if (_BlackJack_MostNumber == 21 && _BlackJack_BlackJack)//JOKER
            {
                GetCoinPopup((int)(c * 1.5), 1, 1);
                AchievementMng.Data.AddAchievementCount(42);
            }
            else if (_BlackJack_MostNumber == 21)
            {
                GetCoinPopup((int)(c * 1.5), 0,1);
                AchievementMng.Data.AddAchievementCount(41);
            }
            else if (_BlackJack_MostNumber >= 19 && _BlackJack_MostNumber < 21)
            {
                GetCoinPopup((int)(c * 1.5), 1, 0);
                AchievementMng.Data.AddAchievementCount(41);
            }
            else if (_BlackJack_MostNumber > 21)
            {
                GetCoinPopup((int)(c * 0.5f), 0, 0);
                AchievementMng.Data.AddAchievementCount(39);
            }
            else
            {
                GetCoinPopup(c + (int)(c * (_BlackJack_MostNumber / 36.0f)), 0, 0);
                AchievementMng.Data.AddAchievementCount(40);
            }
            _BlackJackGetNumber.Clear();
            StartCoroutine(BlackJack_EndGame_Cor());
        }
    }

    IEnumerator BlackJack_EndGame_Cor()
    {
        yield return new WaitForSeconds(1.0f);

        BlackJack_EndGame();

    }

    void BlackJack_EndGame()
    {
        _BlackJackTable.SetActive(false);
        _BlackJackStartButton.SetActive(true);
        for (int i = 0; i < _BlackJack_MoveObj.Count; i++)
        {
            Destroy(_BlackJack_MoveObj[i]);
        }
        _BlackJack_MoveObj.Clear();
        _BlackJackGetNumber.Clear();
    }

}
