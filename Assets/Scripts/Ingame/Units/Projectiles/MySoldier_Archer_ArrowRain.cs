using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySoldier_Archer_ArrowRain : MonoBehaviour
{
    [SerializeField] GameObject _Circle;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.5f);
        _Circle.SetActive(false);
    }
}
