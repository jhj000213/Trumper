using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MainGroundSoldierMove : MonoBehaviour
{
    [SerializeField] SpriteAtlas _BodyAtlas;
    [SerializeField] SpriteRenderer _Body;
    [SerializeField] SpriteRenderer[] _FrontHandFoot;
    [SerializeField] SpriteRenderer[] _BackHandFoot;
    [SerializeField] int[] _BodyNum = new int[2];
    string[] _Shape = { "s", "h", "d", "c" };
    Animator _Animator;
    Vector2 _BorderRect1 = new Vector2(-5, -2);
    Vector2 _BorderRect2 = new Vector2(5, 2);

    Vector2 _TargetPos;
    const float _Speed = 0.5f;

    float _Timer;
    bool _Move;
    int _LayerOrder;
    private void Start()
    {
        _LayerOrder = 20000-(int)((transform.localPosition.y + 2) * 3000);
        _Body.sortingOrder = _LayerOrder;
        for (int i = 0; i < _FrontHandFoot.Length; i++)
            _FrontHandFoot[i].sortingOrder = _LayerOrder + (i+1);
        for (int i = 0; i < _BackHandFoot.Length; i++)
            _BackHandFoot[i].sortingOrder = _LayerOrder - (i+1);

        _Animator = GetComponent<Animator>();
        _Timer = Random.Range(3.0f, 16.0f);
        _TargetPos = transform.localPosition;
        if(Random.Range(0,2)==0)
            transform.localEulerAngles = new Vector3(0, 180, 0);
        else
            transform.localEulerAngles = new Vector3(0, 0, 0);
        _Move = false;
        _Animator.SetTrigger("stay");

        if(_Body)
            _Body.sprite = _BodyAtlas.GetSprite(_Shape[Random.Range(0, 4)]+ _BodyNum[Random.Range(0, 2)]);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.localPosition, _TargetPos) < 0.03f)
        {
            _LayerOrder = 20000 - (int)((transform.localPosition.y + 2) * 3000);
            _Body.sortingOrder = _LayerOrder;
            for (int i = 0; i < _FrontHandFoot.Length; i++)
                _FrontHandFoot[i].sortingOrder = _LayerOrder + (i+1);
            for (int i = 0; i < _BackHandFoot.Length; i++)
                _BackHandFoot[i].sortingOrder = _LayerOrder - (i+1);

            transform.localPosition = _TargetPos;
            if(_Move)
            {
                _Animator.SetTrigger("stay");
                _Move = false;
            }
        }
        else
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, _TargetPos, _Speed * Time.smoothDeltaTime);

        _Timer -= Time.smoothDeltaTime;
        if(_Timer<0.0f)
        {
            SetTargetPos();
            _Timer = Random.Range(3.0f, 16.0f);
            _Animator.SetTrigger("walk");
            _Move = true;
        }
    }
    void SetTargetPos()
    {
        float dx;
        float dy;
        Vector3 temp;
        dx = Random.Range(-1.5f, 1.5f);
        dy = Random.Range(-0.5f, 0.5f);
        temp = transform.localPosition + new Vector3(dx, dy, 0);

        if (temp.x < _BorderRect1.x)
            temp.x = _BorderRect1.x;
        if (temp.x > _BorderRect2.x)
            temp.x = _BorderRect2.x;
        if (temp.y < _BorderRect1.y)
            temp.y = _BorderRect1.y;
        if (temp.y > _BorderRect2.y)
            temp.y = _BorderRect2.y;


        _TargetPos = temp;
        if (dx < 0)
            transform.localEulerAngles = new Vector3(0, 180, 0);
        else
            transform.localEulerAngles = new Vector3(0, 0, 0);

    }

}
