using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GambleCard : MonoBehaviour
{
    [SerializeField]
    protected Image _FrontImage;
    protected Image _BackImage;

    protected const float _RotateSpeed = 360.0f;

    protected bool _Choicing;
    protected bool _Opening = false;

    protected bool _Lerping = false;
    protected bool _Moving = false;
    protected Vector3 _MoveTargetPos;
    protected float _MoveSpeed;

    protected bool _Fading = false;
    protected float _NowAlpha;
    protected float _AlphaSpeed;


    private void Start()
    {
        _BackImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OpenCard(Sprite sn)
    {
        transform.localEulerAngles = new Vector3(0, 180, 0);
        _Opening = true;
        _FrontImage.sprite = sn;
    }
    public void LerpCard(Vector3 pos)
    {
        _MoveTargetPos = pos;
        _Lerping = true;
        _MoveSpeed = 7;
        StartCoroutine(MoveStop(1.0f));
    }
    public void MoveCard(Vector3 pos, float speed, float time)
    {
        _MoveTargetPos = pos;
        _Moving = true;
        _MoveSpeed = speed;
        StartCoroutine(MoveStop(time));
    }
    protected IEnumerator LerpStop(float time)
    {
        yield return new WaitForSeconds(time);
        SoundMng.Data.PlayEffectSound(EffectSound.CardFlip, 0);
        _Lerping = false;
        transform.localPosition = _MoveTargetPos;

    }
    protected IEnumerator MoveStop(float time)
    {
        yield return new WaitForSeconds(time);
        SoundMng.Data.PlayEffectSound(EffectSound.CardFlip, 0);
        _Moving = false;
        transform.localPosition = _MoveTargetPos;

    }

    virtual protected void Update()
    {
        if (_Opening)
        {
            transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(0, 360, 0), _RotateSpeed * Time.smoothDeltaTime);
            if(transform.localEulerAngles.y>=340.0f)
            {
                SoundMng.Data.PlayEffectSound(EffectSound.CardFlip, 0);
                _Opening = false;
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (transform.localEulerAngles.y >= 270.0f)
                _FrontImage.gameObject.SetActive(true);
        }

        if(_Lerping)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _MoveTargetPos,Time.smoothDeltaTime*_MoveSpeed);
        }
        else if(_Moving)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _MoveTargetPos, _MoveSpeed*Time.smoothDeltaTime);
        }

        if(_Fading)
        {
            _NowAlpha -= Time.smoothDeltaTime * _AlphaSpeed;
            _BackImage.color = new Color(1, 1, 1, _NowAlpha);
        }

    }
    public bool IsLerping() { return _Lerping; }
    public bool IsMoving() { return _Moving; }
    public bool IsChoicing() { return _Choicing; }
    public bool IsOpening() { return _Opening; }
}
