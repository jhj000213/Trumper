using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleCard_BlackJack : GambleCard
{
    bool _Moving_y;
    float _Target_x;
    float _Target_y;
    float _MoveSpeed_y;

    float _OpenSpeed = 2f;

    public void OpenCard(Sprite sn, int count,float openspeed)
    {
        SoundMng.Data.PlayEffectSound(EffectSound.CardFlip, 0);
        transform.localScale = new Vector3(1, 1, 1);
        _FrontImage.sprite = sn;
        _OpenSpeed = openspeed;
        _Moving = true;
        _Moving_y = true;
        //_MoveTargetPos = new Vector3(-330 + count * 100, 140, 0);
        _Target_x = -330 + count * 150;
        _Target_y = 0;
        _MoveSpeed = Mathf.Abs(transform.localPosition.x - _Target_x)* _OpenSpeed;
        _MoveSpeed_y = Mathf.Abs(transform.localPosition.y - _Target_y) * _OpenSpeed * 2;
        StartCoroutine(NewMove1());
        StartCoroutine(NewMove2());

        //_Lerping = true;
        //_MoveSpeed = 10;
        //_MoveTargetPos = new Vector3(-550, -160, 0);
        //StartCoroutine(Move1(count));
    }

    IEnumerator NewMove1()
    {
        yield return new WaitForSeconds(0.5f/ _OpenSpeed);

        _Target_y = 140;
    }

    IEnumerator NewMove2()
    {
        yield return new WaitForSeconds(1.0f/ _OpenSpeed);

        _Moving = false;
        _Moving_y = false;
        transform.localPosition = new Vector3(_Target_x, _Target_y, 0);
        transform.localEulerAngles = new Vector3(0, 180, 0);
        transform.localScale = new Vector3(1.5f, 1.5f, 1);
        _Opening = true;
    }

    protected override void Update()
    {
        if(_Moving)
        {
            transform.localPosition = Vector3.MoveTowards(
                new Vector3(transform.localPosition.x, transform.localPosition.y, 0),
                new Vector3(_Target_x, transform.localPosition.y, 0), _MoveSpeed * Time.smoothDeltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1.5f, 1.5f, 1), Time.smoothDeltaTime * 0.5f*_OpenSpeed);
        }
        if (_Moving_y)
        {
            transform.localPosition = Vector3.MoveTowards(
                new Vector3(transform.localPosition.x, transform.localPosition.y, 0),
                new Vector3(transform.localPosition.x, _Target_y, 0), _MoveSpeed_y * Time.smoothDeltaTime);
        }
        if (_Opening)
        {
            transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(0, 360, 0), _RotateSpeed * Time.smoothDeltaTime);
            if (transform.localEulerAngles.y >= 340.0f)
            {
                _Opening = false;
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (transform.localEulerAngles.y >= 270.0f)
                _FrontImage.gameObject.SetActive(true);
        }
    }

    IEnumerator Move1(int count)
    {
        yield return new WaitForSeconds(0.5f);

        transform.localPosition = _MoveTargetPos;
        _MoveTargetPos = new Vector3(-330 + count * 100, -160, 0);
        StartCoroutine(Move2(count));
    }

    IEnumerator Move2(int count)
    {
        yield return new WaitForSeconds(0.5f);

        transform.localPosition = _MoveTargetPos;
        _MoveTargetPos = new Vector3(-330 + count * 100, 140, 0);
        StartCoroutine(Move3());
    }
    IEnumerator Move3()
    {
        yield return new WaitForSeconds(0.5f);

        _Lerping = false;
        transform.localPosition = _MoveTargetPos;

        transform.localEulerAngles = new Vector3(0, 180, 0);
        _Opening = true;
    }
}
