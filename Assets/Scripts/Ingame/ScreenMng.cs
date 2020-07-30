using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenMng : MonoBehaviour
{
    Vector2 _TouchStartPos;
    [SerializeField] GameObject _Camera;
    const float _CameraMax = 38.4f;
    const float _CameraZ = -10;
    bool _SlideOn = false;

    //touch
    int _SlidingTouchFingerid;
    Touch _TempTouchs;
    Vector2 _TouchPos;
    bool _TouchOn;


    bool _GameStart;
    Vector3 _GameEndTargetPos;

    bool _CameraMove;

    private void Update()
    {
        if (_CameraMove)
        {
            if (Mathf.Abs(_Camera.transform.localPosition.x - _GameEndTargetPos.x) >= 0.1f)
                _Camera.transform.localPosition = Vector3.Lerp(_Camera.transform.localPosition, _GameEndTargetPos, 0.1f);
            else
            {
                _Camera.transform.localPosition = _GameEndTargetPos;
                _CameraMove = false;
            }

        }
        else if (_GameStart)
        {
#if !UNITY_EDITOR && UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            for(int i=0;i<Input.touchCount;i++)
            {
                _TempTouchs = Input.GetTouch(i);
                _TouchPos = new Vector2(_TempTouchs.position.x * (1920.0f / Screen.width), _TempTouchs.position.y * (1080.0f / Screen.height));
                if(_TempTouchs.phase == TouchPhase.Began)
                {
                    if (!_SlideOn && _TouchPos.y>=350.0f)
                    {
                        _TouchStartPos = _TouchPos;
                        _SlidingTouchFingerid = _TempTouchs.fingerId;
                        _SlideOn = true;
                    }
                }
                else if(_TempTouchs.phase == TouchPhase.Moved)
                {
                    if (_SlideOn && _SlidingTouchFingerid == _TempTouchs.fingerId)
                    {
                        Vector3 pos = _TouchStartPos - _TouchPos;
                        pos.y = 0;
                        _Camera.transform.localPosition += pos * 0.02f;
                        _TouchStartPos = _TouchPos;
                        if (_Camera.transform.localPosition.x <= 0)
                            _Camera.transform.localPosition = new Vector3(0, _Camera.transform.localPosition.y, _CameraZ);
                        else if (_Camera.transform.localPosition.x >= _CameraMax)
                            _Camera.transform.localPosition = new Vector3(_CameraMax, _Camera.transform.localPosition.y, _CameraZ);
                    }
                }
                else if(_TempTouchs.phase == TouchPhase.Ended || _TempTouchs.phase == TouchPhase.Canceled)
                {
                    if (_SlidingTouchFingerid == _TempTouchs.fingerId)
                    {
                        _TouchStartPos = Vector2.zero;
                        _SlideOn = false;
                    }
                }
            }
        }

#else
            Vector2 _NowTouchPos = new Vector2(Input.mousePosition.x * (1920.0f / Screen.width), Input.mousePosition.y * (1080.0f / Screen.height));

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    _TouchStartPos = _NowTouchPos;
                    _SlideOn = true;
                }
                else
                    _SlideOn = false;
            }
            else if (Input.GetMouseButton(0))
            {
                if (_SlideOn)
                {
                    Vector3 pos = _TouchStartPos - _NowTouchPos;
                    pos.y = 0;
                    _Camera.transform.localPosition += pos * 0.02f;
                    _TouchStartPos = _NowTouchPos;
                    if (_Camera.transform.localPosition.x <= 0)
                        _Camera.transform.localPosition = new Vector3(0, _Camera.transform.localPosition.y, _CameraZ);
                    else if (_Camera.transform.localPosition.x >= _CameraMax)
                        _Camera.transform.localPosition = new Vector3(_CameraMax, _Camera.transform.localPosition.y, _CameraZ);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (_NowTouchPos.y > 350.0f)
                {
                    _TouchStartPos = Vector2.zero;
                    _SlideOn = false;
                }
            }
#endif
        }
    }

    public void IngameStart()
    {
        _GameStart = true;
        _CameraMove = false;
        _Camera.transform.localPosition = new Vector3(0,_Camera.transform.localPosition.y, _CameraZ);
    }

    public void CameraMove(bool clear)
    {
        _CameraMove = true;
        if (clear)
            _GameEndTargetPos = new Vector3(_CameraMax, _Camera.transform.localPosition.y, _CameraZ);
        else
            _GameEndTargetPos = new Vector3(0, _Camera.transform.localPosition.y, _CameraZ);
    }

    public void IngameEnd()
    {
        _GameStart = false;
    }
}
