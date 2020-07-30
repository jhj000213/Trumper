using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleCard_Choice : GambleCard
{

    public void CardInit()
    {
        //_Lerping = false;
        _Choicing = false;
        _Opening = false;
        transform.localEulerAngles = new Vector3(0, 0, 0);
        _FrontImage.gameObject.SetActive(false);
    }

    public void ChoiceCard(float y)
    {
        _MoveTargetPos = new Vector3(transform.localPosition.x, transform.localPosition.y + y, 0);
        _Lerping = true;
        _Choicing = true;
        _MoveSpeed = 10;
        StartCoroutine(LerpStop(0.5f));
    }

    public void AlphaCard()
    {
        _MoveSpeed = 10;
        _NowAlpha = 1;
        _AlphaSpeed = 1;
        _Fading = true;
        StartCoroutine(AlphaStop(1.0f));
    }

    IEnumerator AlphaStop(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
        _Fading = false;
        _BackImage.color = new Color(1, 1, 1, 1);
    }
}
