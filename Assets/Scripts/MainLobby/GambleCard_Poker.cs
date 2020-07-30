using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleCard_Poker : GambleCard
{

    protected bool _Changing = false;
    protected float _ChangingTime;

    public void CardInit()
    {
        _Changing = false;
        transform.localEulerAngles = new Vector3(0, 180, 0);
        _FrontImage.gameObject.SetActive(false);
    }

    public void ChangeThisCard(Sprite sn)
    {
        _Changing = true;
        _ChangingTime = 0.5f;
        SoundMng.Data.PlayEffectSound(EffectSound.CardFlip, 0);
        StartCoroutine(ChangeThisCard_Ani1(sn, _ChangingTime / 2));
    }
    IEnumerator ChangeThisCard_Ani1(Sprite sn, float time)
    {
        yield return new WaitForSeconds(time);


        _FrontImage.sprite = sn;
    }
    protected void Update()
    {
        base.Update();
        if (_Changing)
        {
            transform.localEulerAngles = Vector3.MoveTowards(transform.localEulerAngles, new Vector3(0, 360, 0), 360 * (1 / _ChangingTime) * Time.smoothDeltaTime);
            if (transform.localEulerAngles.y >= 340.0f)
            {
                _Changing = false;
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else if (transform.localEulerAngles.y > 270.0f)
            {
                _FrontImage.gameObject.SetActive(true);
            }
            else if (transform.localEulerAngles.y >= 90.0f)
            {
                _FrontImage.gameObject.SetActive(false);
            }
        }
    }
}
