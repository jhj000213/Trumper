using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GambleMng : MonoBehaviour
{
    const int _ChoiceGame_MaxCardCount_Max = 7;
    [SerializeField] GameObject[] _ChoiceGame_Cards = new GameObject[_ChoiceGame_MaxCardCount_Max];
    GambleCard_Choice[] _ChoiceGame_Cards_Comp = new GambleCard_Choice[_ChoiceGame_MaxCardCount_Max];
    int _ChoiceGame_MaxCanChoiceCount = 1;//렙올라갈수록 업
    int _ChoiceGame_NowCanChoiceCount = 1;

    [SerializeField] GameObject _ChoiceGameStartButton;
    [SerializeField] GameObject _ChoiceGameTable;
    List<int> _ChoiceGameGetNumber = new List<int>();
    List<GambleCard_Choice> _ChoiceGameChoicingCard = new List<GambleCard_Choice>();


    void ChoiceGameDeckSet()
    {
        for (int i = 0; i < _ChoiceGame_MaxCardCount_Max; i++)
        {
            _ChoiceGame_Cards_Comp[i] = _ChoiceGame_Cards[i].GetComponent<GambleCard_Choice>();
            _ChoiceGame_Cards[i].SetActive(false);
        }
    }

    public void ChoiceGameStart()
    {
        if (_Chip > 0 && StageMng.Data._NowClearStage > 0)
        {
            UseChip();
            _GambleHighGrey.SetActive(true);
            _ChoiceGameGetNumber.Clear();
            _ChoiceGameChoicingCard.Clear();
            _ChoiceGameTable.SetActive(true);
            _ChoiceGameStartButton.SetActive(false);
            _ChoiceGame_NowCanChoiceCount = 0;
            _ChoiceGame_MaxCanChoiceCount = 1;//

            float width = 1400 / (_ChoiceGame_MaxCardCount_Max + 1);

            for (int i = 0; i < _ChoiceGame_MaxCardCount_Max; i++)
            {
                _ChoiceGame_Cards[i].SetActive(false);
                _ChoiceGame_Cards_Comp[i].CardInit();
            }
            Debug.Log("width " + width);
            for (int i = 0; i < _ChoiceGame_MaxCardCount_Max; i++)
            {
                float speed = 6;

                _ChoiceGame_Cards[i].SetActive(true);
                _ChoiceGame_Cards[i].transform.localPosition = new Vector3(width, 0, 0);
                _ChoiceGame_Cards_Comp[i].MoveCard(new Vector3(width * (i + 1), 0, 0), width * speed, (1 / speed) * (i + 1));
               
            }
        }
        SetGambleTableGray();
    }

    public void ChoiceThisCard(GambleCard card)
    {
        if (!card.IsMoving() && !card.IsLerping() && !card.IsChoicing() && !card.IsOpening() && _ChoiceGame_MaxCanChoiceCount > _ChoiceGame_NowCanChoiceCount)
        {
            for (int i = 0; i < _ChoiceGame_MaxCardCount_Max; i++)
            {
                if (_ChoiceGame_Cards_Comp[i] == card)
                {
                    _ChoiceGame_Cards_Comp[i].ChoiceCard(-120);
                    _ChoiceGameChoicingCard.Add(_ChoiceGame_Cards_Comp[i]);
                }
            }

            _ChoiceGame_NowCanChoiceCount++;
            bool check;
            int cardnum;
            while (true)
            {
                check = true;
                cardnum = Random.Range(0, 53);
                for (int i = 0; i < _ChoiceGameGetNumber.Count; i++)
                {
                    if (_ChoiceGameGetNumber[i] == cardnum)
                        check = false;
                }
                if (check)
                    break;
            }

            _ChoiceGameGetNumber.Add(cardnum);

            StartCoroutine(ChoiceGame_ChoiceEnd(_ChoiceGame_NowCanChoiceCount));
        }
    }
    IEnumerator ChoiceGame_ChoiceEnd(int cc)
    {
        yield return new WaitForSeconds(0.5f);

        if (cc == _ChoiceGame_MaxCanChoiceCount)
        {
            int n = 0;
            for (int i = 0; i < _ChoiceGame_MaxCardCount_Max; i++)
            {
                bool check = false;
                for (int c = 0; c < _ChoiceGameChoicingCard.Count; c++)
                {
                    if (_ChoiceGame_Cards_Comp[i] == _ChoiceGameChoicingCard[c])
                        check = true;
                }
                if (check)
                    _ChoiceGame_Cards_Comp[i].OpenCard(_CardFrontAtlas.GetSprite(GetCardCode(_ChoiceGameGetNumber[n++])));
                else
                    _ChoiceGame_Cards_Comp[i].AlphaCard();
            }

            StartCoroutine(ChoiceGameEnd(3.0f));
        }
    }

    IEnumerator ChoiceGameEnd(float time)
    {
        yield return new WaitForSeconds(time);

        for (int i = 0; i < _ChoiceGameGetNumber.Count; i++)
        {
            GiveCoin_ChoiceGame(_ChoiceGameGetNumber[i]);
        }
        _ChoiceGameStartButton.SetActive(true);
        _ChoiceGameTable.SetActive(false);
    }

    void GiveCoin_ChoiceGame(int cardnum)
    {
        int c = StageMng.Data._ClearRewardMax[StageMng.Data._NowClearStage - 1];
        if (cardnum == 52)//JOKER
        {
            GetCoinPopup((int)(c * 1.5),1,1);
            AchievementMng.Data.AddAchievementCount(38);
        }
        else
        {
            cardnum %= 13;
            if (cardnum >= 10 && cardnum <= 12)//A
            {
                GetCoinPopup((int)(c * 1.5), 1, 0);
                if (cardnum == 10)
                    AchievementMng.Data.AddAchievementCount(35);
                if (cardnum == 11)
                    AchievementMng.Data.AddAchievementCount(36);
                if (cardnum == 12)
                    AchievementMng.Data.AddAchievementCount(37);
            }
            else if (cardnum == 0)
            {
                GetCoinPopup((int)(c * 1.5), 0, 1);
                AchievementMng.Data.AddAchievementCount(34);
            }
            else
            {
                GetCoinPopup(c + (int)(c * (cardnum / 20.0)), 0,0);
                AchievementMng.Data.AddAchievementCount(33);
            }
        }
    }
}
