using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSelf : MonoBehaviour
{
    [SerializeField] float _RemoveTime;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(_RemoveTime);
        Destroy(gameObject);
    }
}
