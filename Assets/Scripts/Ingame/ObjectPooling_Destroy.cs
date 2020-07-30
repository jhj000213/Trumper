using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling_Destroy : MonoBehaviour
{
    [SerializeField] float _DelayTime;
    float _NowTime;

    void Update()
    {
        _NowTime += Time.smoothDeltaTime;

        if (_NowTime >= _DelayTime)
        {
            _NowTime = 0.0f;
            gameObject.SetActive(false);
        }

    }

    public void SetTime(float time)
    {
        _DelayTime = time;
    }
}
