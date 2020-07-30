using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class SceneMng : MonoBehaviour
{
    //Scene
    [SerializeField] GameObject _Camera;
    [SerializeField] GameObject _GameScene;
    [SerializeField] GameObject _MainScene;

    [SerializeField] Animator _SceneChangeFadeEffect;


    //Popup
    [SerializeField] GameObject _GamblePopup;
    [SerializeField] GameObject[] _GamblePopup_Tables = new GameObject[3];
    [SerializeField] GameObject[] _GambleTabs_Grey = new GameObject[3];

    [SerializeField] GameObject[] _HighTabsChoices = new GameObject[4];


    [SerializeField] GameObject _UpgradePopup;
    [SerializeField] GameObject _HeroPopup;
    [SerializeField] GameObject _SoldierLevelTab;
    [SerializeField] GameObject _ShapeUpPopup;
    [SerializeField] GameObject _StageSelectPopup;
    [SerializeField] GameObject _HighTabs;
    [SerializeField] GameObject _ShopPopup;
    [SerializeField] GameObject _StoryPopup;
    [SerializeField] GameObject _AchievementPopup;
    [SerializeField] GameObject _OptionPopup;
    [SerializeField] GameObject _CreditPoup;
    [SerializeField] GameObject _RankingPoup;

    [SerializeField] Text _MonthText;
    [SerializeField] GameObject[] _StageStartButton = new GameObject[5];
    [SerializeField] GameObject[] _StageLevelGrey = new GameObject[5];
    [SerializeField] GameObject[] _StageLevelClearCheck = new GameObject[5];
    [SerializeField] GameObject _LastStageStartButton;
    [SerializeField] GameObject _LastStageGrey;
    [SerializeField] GameObject _LastStageClearCheck;
    [SerializeField] GameObject _IsChallangeMode;
    [SerializeField] Text _ModeText;
    int _NowStageSelectPopupMonth;
    bool _NowCheckNormalMode;
    bool _FastMode;
    
    [SerializeField] GameObject[] _FastOnCircle;
    [SerializeField] Image[] _FastOnOffTable;
    [SerializeField] Sprite[] _OnOffTableSprites;


    private void Start()
    {
        _NowCheckNormalMode = true;
        _NowStageSelectPopupMonth = 1;
        _GameScene.SetActive(false);
        _MainScene.SetActive(true);
        ToolTipInit();
        _FastMode = PlayerPrefs.GetInt("fastmode", 0) == 1 ? true : false;
        FastModeUpdate();
    }

    public void CheckFastMode()
    {
        _FastMode = !_FastMode;
        PlayerPrefs.SetInt("fastmode", _FastMode ? 1 : 0);
        FastModeUpdate();
    }
    public void FastModeUpdate()
    {

        if (_FastMode)
        {
            if (StageMng.Data.IngameStarted())
                Time.timeScale = 5;
            else
                Time.timeScale = 1;
        }
        else
            Time.timeScale = 1;


        _FastOnCircle[0].transform.localPosition = new Vector3((_FastMode ? -1 : 1) * 26.7f, 0, 0);
        _FastOnCircle[1].transform.localPosition = new Vector3((_FastMode ? -1 : 1) * 26.7f, 0, 0);
        _FastOnOffTable[0].sprite = _OnOffTableSprites[_FastMode ? 0 : 1];
        _FastOnOffTable[1].sprite = _OnOffTableSprites[_FastMode ? 0 : 1];
    }

    public void SceneChange_Ingame()
    {
        SceneChange_First(1.5f, false);
        SoundMng.Data.StopBgm();
    }

    public void SceneChange_Lobby()
    {
        _StageSelectPopup.SetActive(false);
        SceneChange_First(1.0f, true);
        SoundMng.Data.StopBgm();
    }

    void SceneChange_First(float time, bool golobby)
    {
        SetToolTip();
        _SceneChangeFadeEffect.SetTrigger("fade");
        StartCoroutine(SceneChange_Fade(time, golobby));
    }

    IEnumerator SceneChange_Fade(float time, bool golobby)
    {
        yield return new WaitForSeconds(time);

        _MainScene.SetActive(golobby);
        _GameScene.SetActive(!golobby);
        if(golobby)
        {
            SoundMng.Data.PlayBgm();
            SoundMng.Data.GameEnd();
        }

        FastModeUpdate();
        _Camera.transform.localPosition = new Vector3(0, 0, -10);
    }

    public void SetStageSelectPopup(bool data)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        _StageSelectPopup.SetActive(data);
        if (_NowCheckNormalMode)
            _NowStageSelectPopupMonth = StageMng.Data._NowClearStage / 5 + 1;
        else
            _NowStageSelectPopupMonth = StageMng.Data._NowClearStage_Challange / 5 + 1;
        _IsChallangeMode.SetActive(!_NowCheckNormalMode);
        StageSelectUpdate();
    }

    public void SetStageSelectMonth(bool left)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        if (left)
        {
            if (_NowStageSelectPopupMonth > 1)
                _NowStageSelectPopupMonth--;
        }
        else
        {
            if (_NowStageSelectPopupMonth < 13)
                _NowStageSelectPopupMonth++;
        }
        StageSelectUpdate();
    }

    public void SetStageMode(bool normal)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        _NowCheckNormalMode = normal;
        if (_NowCheckNormalMode)
        {
            _NowStageSelectPopupMonth = StageMng.Data._NowClearStage / 5 + 1;
            _ModeText.text = "노말 모드";
        }
        else
        {
            _NowStageSelectPopupMonth = StageMng.Data._NowClearStage_Challange / 5 + 1;
            _ModeText.text = "챌린지 모드";
        }
        _IsChallangeMode.SetActive(!_NowCheckNormalMode);
        StageSelectUpdate();
    }

    void StageSelectUpdate()//61    2스테이지 1까지 별, 2 열려잇고 3부터 grey
    {
        int clearstage;
        if (_NowCheckNormalMode)
            clearstage = StageMng.Data._NowClearStage;
        else
        {
            if (StageMng.Data._NowClearStage < 61)
                clearstage = -1;
            else
                clearstage = StageMng.Data._NowClearStage_Challange;
        }


        _MonthText.text = _NowStageSelectPopupMonth+" 월";
        if(_NowStageSelectPopupMonth==12)
            _MonthText.text = "12-1 월";
        else if(_NowStageSelectPopupMonth==13)
            _MonthText.text = "12-2 월";

        int clearlevel = clearstage % 5;//1
        int clearmonth = clearstage / 5 + 1;//13
        for (int i = 0; i < 5; i++)
        {
            _StageStartButton[i].SetActive(false);
            _StageLevelGrey[i].SetActive(false);
            _StageLevelClearCheck[i].SetActive(false);
        }
        _LastStageStartButton.SetActive(false);
        _LastStageGrey.SetActive(false);
        _LastStageClearCheck.SetActive(false);

        //if(!_NowCheckNormalMode && StageMng.Data._NowClearStage < 61)
        //{ }

        if (_NowStageSelectPopupMonth==13)
        {
            _LastStageStartButton.SetActive(true);
            if (clearmonth == 13)
            {
                _LastStageGrey.SetActive(false);
                if (clearlevel > 0)
                    _LastStageClearCheck.SetActive(true);
            }
            else
                _LastStageGrey.SetActive(true);
        }
        else
        {
            for (int i = 0; i < 5; i++)
                _StageStartButton[i].SetActive(true);
            if (_NowStageSelectPopupMonth == clearmonth)
            {
                for (int i = 0; i < clearlevel; i++)
                    _StageLevelClearCheck[i].SetActive(true);
                for (int i = clearlevel + 1; i < 5; i++)
                    _StageLevelGrey[i].SetActive(true);
            }
            else if (_NowStageSelectPopupMonth < clearmonth)
            {
                for (int i = 0; i < 5; i++)
                {
                    _StageLevelClearCheck[i].SetActive(true);
                    _StageLevelGrey[i].SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    _StageLevelClearCheck[i].SetActive(false);
                    _StageLevelGrey[i].SetActive(true);
                }
            }
        }
    }

    public void SetGamblePopup(bool data)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        _UpgradePopup.SetActive(false);
        _HeroPopup.SetActive(false);
        _ShapeUpPopup.SetActive(false);
        _SoldierLevelTab.SetActive(false);

        for (int i = 0; i < 4; i++)
            _HighTabsChoices[i].SetActive(false);
        _HighTabsChoices[0].SetActive(true);

        _HighTabs.SetActive(data);
        _GamblePopup.SetActive(data);
    }
    public void SetUpgradePopup(bool data)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        _GamblePopup.SetActive(false);
        _HeroPopup.SetActive(false);
        _ShapeUpPopup.SetActive(false);

        for (int i = 0; i < 4; i++)
            _HighTabsChoices[i].SetActive(false);
        _HighTabsChoices[1].SetActive(true);

        _HighTabs.SetActive(data);
        _SoldierLevelTab.SetActive(data);
        _UpgradePopup.SetActive(data);
    }
    public void SetHeroPopup(bool data)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        _GamblePopup.SetActive(false);
        _UpgradePopup.SetActive(false);
        UpgradeMng.Data.HeroSelectTab(PlayerPrefs.GetInt("heronumber", 1) - 1);
        _ShapeUpPopup.SetActive(false);
        _SoldierLevelTab.SetActive(false);

        for (int i = 0; i < 4; i++)
            _HighTabsChoices[i].SetActive(false);
        _HighTabsChoices[2].SetActive(true);


        _HighTabs.SetActive(data);
        _SoldierLevelTab.SetActive(data);
        _HeroPopup.SetActive(data);
    }
    public void SetShapeUpPopup(bool data)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        _GamblePopup.SetActive(false);
        _UpgradePopup.SetActive(false);
        _HeroPopup.SetActive(false);
        _SoldierLevelTab.SetActive(false);

        for (int i = 0; i < 4; i++)
            _HighTabsChoices[i].SetActive(false);
        _HighTabsChoices[3].SetActive(true);


        _HighTabs.SetActive(data);
        _ShapeUpPopup.SetActive(data);
    }

    public void SetShopPopup(bool data)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        _ShopPopup.SetActive(data);
    }
    public void SetStoryPopup(bool data)
    {
        _StoryPopup.SetActive(data);
    }
    public void SetAchievementPopup(bool data)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        _AchievementPopup.SetActive(data);
        if (data)
            AchievementMng.Data.OpenAchievementTab();
    }
    public void SetOptionPopup(bool data)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        _OptionPopup.SetActive(data);
    }
    public void SetCreditPopup(bool data)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        _CreditPoup.SetActive(data);
    }
    public void SetRankingPopup(bool data)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        _RankingPoup.SetActive(data);
    }

    public void Tab_ChoiceGame(int tab)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.UI_Click, 0);
        for (int i = 0; i < 3; i++)
        {
            _GamblePopup_Tables[i].SetActive(false);
            _GambleTabs_Grey[i].SetActive(true);
        }
        _GamblePopup_Tables[tab].SetActive(true);
        _GambleTabs_Grey[tab].SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public int GetNowSelectMonth() { return _NowStageSelectPopupMonth - 1; }
    public bool GetNowStageIsNormal() { return _NowCheckNormalMode; }
}
