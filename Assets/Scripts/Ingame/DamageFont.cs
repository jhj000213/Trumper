using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class DamageFont : MonoBehaviour
{
    //[SerializeField] SpriteAtlas _FontAtlas;
    //
    //[SerializeField] SpriteRenderer[] _ObjSprites = new SpriteRenderer[5];
    //[SerializeField] GameObject[] _FontObj = new GameObject[5];
    //
    //[SerializeField] SpriteRenderer _MaxObj;
    //[SerializeField] SpriteRenderer _MissObj;

    [SerializeField] TextMesh _DamageText;

    Vector3 _TargetPos;
    bool _EnemyHit;
    float _Alpha;

    private void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _TargetPos, Time.smoothDeltaTime);

        if(_Alpha>0)
        {
            _Alpha -= Time.smoothDeltaTime;
            //for (int i = 0; i < 5; i++)
            //    _ObjSprites[i].color = new Color(1, 1, 1, _Alpha);
            _DamageText.color = new Color(_DamageText.color.r, _DamageText.color.g, _DamageText.color.b, _Alpha);
            //_MaxObj.color = new Color(1, 1, 1, _Alpha);
            //_MissObj.color = new Color(1, 1, 1, _Alpha);
        }
    }

    public void SetDamage(int num,Vector3 pos,bool enemyhit,bool onedead=false)
    {
        transform.localScale = new Vector3(0.65f, 0.65f, 1);
        _Alpha = 1;
        _EnemyHit = enemyhit;
        pos += new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.15f, 0.15f), 0);
        transform.localPosition = pos;
        _TargetPos = pos + new Vector3(0, 3, 0);
        gameObject.SetActive(true);
        _DamageText.gameObject.SetActive(true);
        if (onedead)
        {
            //_MaxObj.gameObject.SetActive(true);
            _DamageText.text = "Max!";
        }
        else if(num==0)
        {
            //_MissObj.gameObject.SetActive(true);
            _DamageText.text = "Miss";
        }
        else
        {
            _DamageText.text = num.ToString();

            //int range = 1;
            //if (num > 9999)
            //    range = 5;
            //else if (num > 999)
            //    range = 4;
            //else if (num > 99)
            //    range = 3;
            //else if (num > 9)
            //    range = 2;

            //for (int i = 0; i < range; i++)
            //{
            //    int tn = (num % (int)Mathf.Pow(10, i + 1)) / (int)(Mathf.Pow(10, i));
            //    _FontObj[i].SetActive(true);
            //    _ObjSprites[i].sprite = _FontAtlas.GetSprite(tn.ToString());
            //    _FontObj[i].transform.localPosition = new Vector3((0.55f * (4 - i)) - (0.275f * (5 - range)) - 1.1f, 0, 0);//0.55f = 숫자간격, 0.275f = 간격 절반
            //}
        }
        StartCoroutine(SetActiveFalse(1.0f));
    }
    
    public void IsHeal()
    {
        _DamageText.color = new Color(0.6705883f,1, 0.1098039f);
        if (_DamageText.text == "Miss")
            _DamageText.text = "";
    }

    IEnumerator SetActiveFalse(float time)
    {
        yield return new WaitForSeconds(time);

        //for(int i=0;i<5;i++)
        //    _FontObj[i].SetActive(false);
        //_MissObj.gameObject.SetActive(false);
        //_MaxObj.gameObject.SetActive(false);
        _DamageText.color = new Color(1, 1, 1);
        _DamageText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
