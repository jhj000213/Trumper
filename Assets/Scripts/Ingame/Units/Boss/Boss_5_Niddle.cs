using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_5_Niddle : MonoBehaviour
{
    [SerializeField] Animator _Animator;
    [SerializeField] SpriteRenderer _Sprite;
    public void Shoot(Vector3 pos,Vector3 size)
    {
        gameObject.SetActive(true);
        transform.localPosition = pos;
        transform.localScale = size;
        StartCoroutine(Destroy(0.8f));
        _Animator.SetTrigger("start");
        _Sprite.flipX = Random.Range(0, 2) == 1 ? true : false;
    }
    

    IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}
