using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class SoundMng : MonoBehaviour
{
    private static SoundMng instance = null;
    public static SoundMng Data
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(SoundMng)) as SoundMng;
                if (instance == null)
                {
                    Debug.Log("no instance");
                }
            }
            return instance;
        }
    }

    [SerializeField] GameObject _EffectSoundObj;
    [SerializeField] Transform _Parent;
    List<EffectSoundObj> _EffectSoundList = new List<EffectSoundObj>();
    int _NowEffectSoundNum;
    const int _MaxEffectSoundNum = 100;

    float _TargetBgmVolume = 0;
    float _MaxBgmVolume = 0.25f;

    bool _BgmOn;
    bool _EffectSoundOn;

    [SerializeField] AudioSource _Bgm;
    [SerializeField] AudioClip[] _BgmClips = new AudioClip[5];


    [SerializeField] GameObject[] _BgmOnCircle;
    [SerializeField] Image[] _BgmOnOffTable;

    [SerializeField] GameObject[] _EffectOnCircle;
    [SerializeField] Image[] _EffectOnOffTable;

    [SerializeField] Sprite[] _OnOffTableSprites;

    private void Awake()
    {
        DictionarySetting();

        _BgmOn = PlayerPrefs.GetInt("bgmon", 1)==1?true:false;
        _EffectSoundOn = PlayerPrefs.GetInt("effectsoundon", 1)==1?true:false;
        //Debug.Log(_BgmOn);
        VolumeSet();

        _TargetBgmVolume = _MaxBgmVolume;
        _Bgm.volume = _TargetBgmVolume;

        for (int i=0;i< _MaxEffectSoundNum; i++)
        {
            GameObject obj = Instantiate(_EffectSoundObj, transform);
            _EffectSoundList.Add(obj.GetComponent<EffectSoundObj>());
            _EffectSoundList[i].gameObject.SetActive(false);
        }
        _NowEffectSoundNum = 0;



        PlayBgm();
    }

    private void Update()
    {
        _Bgm.volume = Mathf.MoveTowards(_Bgm.volume, _TargetBgmVolume, Time.smoothDeltaTime * 0.5f);
    }

    public void OnOffBgm()
    {
        _BgmOn = !_BgmOn;
        VolumeSet();
    }

    void VolumeSet()
    {

        if (_BgmOn)
            _MaxBgmVolume = 0.25f;
        else
            _MaxBgmVolume = 0;
        _TargetBgmVolume = _MaxBgmVolume;
        PlayerPrefs.SetInt("bgmon", _BgmOn ? 1 : 0);
        PlayerPrefs.SetInt("effectsoundon", _EffectSoundOn ? 1 : 0);

        _BgmOnOffTable[0].sprite = _OnOffTableSprites[_BgmOn ? 0 : 1];
        _BgmOnOffTable[1].sprite = _OnOffTableSprites[_BgmOn ? 0 : 1];
        _EffectOnOffTable[0].sprite = _OnOffTableSprites[_EffectSoundOn ? 0 : 1];
        _EffectOnOffTable[1].sprite = _OnOffTableSprites[_EffectSoundOn ? 0 : 1];

        _BgmOnCircle[0].transform.localPosition = new Vector3((_BgmOn ? -1 : 1) * 26.7f, 0, 0);
        _BgmOnCircle[1].transform.localPosition = new Vector3((_BgmOn ? -1 : 1) * 26.7f, 0, 0);
        _EffectOnCircle[0].transform.localPosition = new Vector3((_EffectSoundOn ? -1 : 1) * 26.7f, 0, 0);
        _EffectOnCircle[1].transform.localPosition = new Vector3((_EffectSoundOn ? -1 : 1) * 26.7f, 0, 0);
    }

    public void OnOffEffectSound()
    {
        _EffectSoundOn = !_EffectSoundOn;
        VolumeSet();
    }

    public void PlayBgm(int n = 0)
    {
        _TargetBgmVolume = _MaxBgmVolume;
        _Bgm.clip = _BgmClips[n];
        _Bgm.Play();
    }
    public void StopBgm()
    {
        _TargetBgmVolume = 0;
        //_Bgm.Stop();
        
    }
    public void PlayEffectSound(EffectSound effect, float delay)
    {
        Transform parent = _Parent;
        PlayEffectSound(effect,delay, parent);
    }

    public void PlayEffectSound(EffectSound effect, float delay, Transform parent)
    {
        if(_EffectSoundOn)
        {
            _EffectSoundList[_NowEffectSoundNum].gameObject.SetActive(true);
            _EffectSoundList[_NowEffectSoundNum].Using(_EffectSoundDictionary[effect], delay, parent);

            _NowEffectSoundNum++;
            if (_NowEffectSoundNum >= _MaxEffectSoundNum)
                _NowEffectSoundNum = 0;
        }
    }

    public void GameEnd()
    {
        for(int i=0;i<_MaxEffectSoundNum;i++)
        {
            _EffectSoundList[i].StopAllCoroutines();
            _EffectSoundList[i].transform.parent = transform;
            _EffectSoundList[i].transform.localPosition = Vector3.zero;
            _EffectSoundList[i].gameObject.SetActive(false);
        }
    }
    
}
