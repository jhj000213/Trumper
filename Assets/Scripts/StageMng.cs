using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMng : MonoBehaviour
{
    private static StageMng instance = null;
    public static StageMng Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(StageMng)) as StageMng;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }

    int _NowStage;
    public readonly int _STAGEGAP = 5;
    public int _NowClearStage;
    public int _NowClearStage_Challange;
    //[SerializeField] GameObject[] _StageButtonGrayArray;

    bool _NowStageIsNormal;
    bool _Clear;

    float _NowBoss8Timer;
    float _MaxBoss8Timer = 30.0f;
    bool _Boss8TimerOn = false;
    [SerializeField] Animator _Boss8_Moon;
    [SerializeField] Animator _Boss8_BgAni;

    [SerializeField] GameObject[] _MonthBgs;


    bool _IngameStart = false;
    [SerializeField] SceneMng _SceneMng;
    [SerializeField] ScreenMng _ScreenMng;
    [SerializeField] UnitMng _UnitMng;
    [SerializeField] UnitSummonMng _UnitSummonMng;
    [SerializeField] SoldierShapeBuffMng _SoldierShapeBuffMng;
    [SerializeField] GambleMng _GambleMng;
    [SerializeField] GameObject _OptionPopup;

    [SerializeField] Animator[] _ResultPopupAni = new Animator[2];
    [SerializeField] Text _ResultCoinText;

    bool _GameEndCheck = false;

    public List<int> _ClearRewardMin = new List<int>();
    public List<int> _ClearRewardMax = new List<int>();

    private void Awake()
    {
        Application.targetFrameRate = 144;
        List<Dictionary<string, object>> stage = CSVReader.Read("TotalStageInfo");
        int max = (int)stage[0]["Max_Stage"];
        for(int i=0;i<max;i++)
        {
            _ClearRewardMin.Add((int)stage[i]["Min_ClearCoin"]);
            _ClearRewardMax.Add((int)stage[i]["Max_ClearCoin"]);
        }
    }

    private void Start()
    {
        //PlayerPrefs.SetInt("nowclearstage", 61);//
        StageClearNumUpdate(PlayerPrefs.GetInt("nowclearstage", 0), true);
        StageClearNumUpdate(PlayerPrefs.GetInt("nowclearstage_c", 0),false);

    }

    private void Update()
    {
        if(_GameEndCheck)
        {
            _GameEndCheck = false;
            GameEnd();
        }
        if(_Boss8TimerOn)
        {
            _NowBoss8Timer += Time.smoothDeltaTime;
            if(_NowBoss8Timer>=_MaxBoss8Timer)
            {
                SoundMng.Data.PlayEffectSound(EffectSound.Boss8_TimeOver, 0);
                _Boss8TimerOn = false;
                if(_UnitMng._MySoldierList.Count>0)
                {
                    Unit castle = _UnitMng._MySoldierList[0];
                    for (int i = 0; i < 100; i++)
                    {
                        castle.HitDamage(1);
                        if (!castle.GetStillAlive())
                            break;
                    }
                }
                else
                {
                    GameEnd();
                }
            }
        }
    }

    void StageClearNumUpdate(int nowstage,bool normal)
    {
        if(normal)
        {
            if (_NowClearStage < nowstage)
            {
                _NowClearStage = nowstage;
                PlayerPrefs.SetInt("nowclearstage", _NowClearStage);
            }
        }
        else
        {
            if (_NowClearStage_Challange < nowstage)
            {
                _NowClearStage_Challange = nowstage;
                PlayerPrefs.SetInt("nowclearstage_c", _NowClearStage_Challange);
                GoogleLogin.Data.SetRank();
            }
        }
    }

    public void CastleDestroy(bool clear)
    {
        _ScreenMng.CameraMove(clear);
        if (clear==false)
        {
            GameEndClearSet(clear);
        }
        else
        {
            if (_NowStage == 1 * _STAGEGAP)
            {
                _UnitSummonMng.BossSummon(1);
            }
            else if (_NowStage == 2 * _STAGEGAP)
            {
                _UnitSummonMng.BossSummon(2);
            }
            else if (_NowStage == 4 * _STAGEGAP)
            {
                _UnitSummonMng.BossSummon(4);
            }
            else if (_NowStage == 7 * _STAGEGAP)
            {
                _UnitSummonMng.BossSummon(7);
            }
            else if (_NowStage == 8 * _STAGEGAP)
            {
                _UnitSummonMng.BossSummon(8);
                Boss8Timer();
            }
            else if (_NowStage == 10 * _STAGEGAP)
            {
                _UnitSummonMng.BossSummon(10);
            }
            else if (_NowStage == 12 * _STAGEGAP)
            {
                _UnitSummonMng.BossSummon(12);
            }
            else if (_NowStage == 12 * _STAGEGAP + 1)
            {
                _UnitSummonMng.BossSummon(13);//진보스
            }
            else
            {
                GameEndClearSet(true);
            }
        } 
    }

    void Boss8Timer()
    {
        _Boss8TimerOn = true;
        _Boss8_BgAni.SetTrigger("on");
        _Boss8_Moon.SetTrigger("on");
    }

    public void LastBossDestroy()
    {
        GameEndClearSet(true);
    }

    void GameEndClearSet(bool clear)//성이 부서지면 들어옴, 애니메이션
    {
        _NowBoss8Timer = 0.0f;
        _Boss8TimerOn = false;
        _IngameStart = false;
        _Clear = clear;
        _UnitMng.EndGame_StopMove();
        _ScreenMng.IngameEnd();
        StartCoroutine(SetResultPopup(clear,1.0f));
        if (clear)
            SetClearAchievement();
        StartCoroutine(GameEndAnimationTime(4.0f));
    }

    void SetClearAchievement()
    {
        for(int i=1;i<13;i++)
        {
            if (_NowStage == i * _STAGEGAP)
                AchievementMng.Data.AddAchievementCount(19+i);
        }
        if (_NowStage == 12 * _STAGEGAP + 1)
            AchievementMng.Data.AddAchievementCount(32);
    }

    IEnumerator SetResultPopup(bool clear,float time)
    {
        yield return new WaitForSeconds(time);

        if (clear)
            SoundMng.Data.PlayEffectSound(EffectSound.Stage_Clear, 0);
        else
            SoundMng.Data.PlayEffectSound(EffectSound.Stage_Fail, 0);
        _ResultPopupAni[0].gameObject.SetActive(true);
        _ResultPopupAni[1].gameObject.SetActive(true);

        _ResultPopupAni[0].SetTrigger("black");
        if (clear)
        {
            _ResultPopupAni[1].SetTrigger("win");
            int getcoin = Random.Range(_ClearRewardMin[_NowStage - 1], _ClearRewardMax[_NowStage - 1]);
            if (!_NowStageIsNormal)
                getcoin = 0;
            UpgradeMng.Data.CoinUp(getcoin);
            _ResultCoinText.text = "+ "+ string.Format("{0:#,0}", getcoin);
        }
        else
            _ResultPopupAni[1].SetTrigger("lose");
    }

    IEnumerator GameEndAnimationTime(float time)
    {
        yield return new WaitForSeconds(time);
        _GameEndCheck = true;
    }

    public void QuitGame()
    {
        _IngameStart = false;
        _Clear = false;
        _ScreenMng.IngameEnd();
        _UnitMng.EndGame_StopMove();
        GameEnd();
    }

    public void SetOptionPopup(bool b)
    {
        _OptionPopup.SetActive(b);
    }

    void GameEnd()
    {
        
        if(_Clear)
        {
            StageClearNumUpdate(_NowStage, _NowStageIsNormal);
            _GambleMng.StageClear();
        }
        else
        {
            if(!_NowStageIsNormal)
            {
                _NowClearStage_Challange = 0;
                GoogleLogin.Data.SetRank();
                PlayerPrefs.SetInt("nowclearstage_c", _NowClearStage_Challange);
            }
        }
        _NowBoss8Timer = 0.0f;
        _Boss8TimerOn = false;
        SetOptionPopup(false);
        _SceneMng.SceneChange_Lobby();
        StartCoroutine(GameEnd_Cor());
    }
    IEnumerator GameEnd_Cor()
    {
        yield return new WaitForSeconds(0.5f);

        _Boss8_BgAni.SetTrigger("off");
        _UnitMng.EndGame();
        ObjectPoolingMng.Data.EndGame();
    }

    public void GameStart(int stageNumber)
    {
        _NowBoss8Timer = 0.0f;
        _OptionPopup.SetActive(false);
        _Clear = false;
        _NowStageIsNormal = _SceneMng.GetNowStageIsNormal();
        _NowStage = _SceneMng.GetNowSelectMonth() * 5 + stageNumber;
        _UnitSummonMng.IngameStart(_NowStage,_NowStageIsNormal);
        _UnitMng.GameStart(_NowStage);
        _SceneMng.SceneChange_Ingame();
        _ScreenMng.IngameStart();
        _SoldierShapeBuffMng.IngameStart();
        _ResultPopupAni[0].gameObject.SetActive(false);
        _ResultPopupAni[1].gameObject.SetActive(false);
        StartCoroutine(GameStartBoolSet());
        _Boss8_Moon.SetTrigger("off");
    }

    IEnumerator GameStartBoolSet()
    {
        yield return new WaitForSeconds(1.0f);
        int n = (_NowStage-1) / 15;
        if (n < 0)
            n = 0;
        if (n > 3)
            n = 3;
        SoundMng.Data.PlayBgm(n+1);


        for (int i = 0; i < 4; i++)
            _MonthBgs[i].SetActive(false);
        _MonthBgs[n].SetActive(true);
        _IngameStart = true;
    }

    public bool IngameStarted() { return _IngameStart; }
}
