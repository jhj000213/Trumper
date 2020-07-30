using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySoldier_Archer_Arrow : MonoBehaviour
{
    float _Force_X;
    float _Force_Y;
    public void Shoot(Vector2 nowpos,float distancex,float distancey,float flyingtime)
    {
        gameObject.SetActive(true);
        transform.localPosition = nowpos;
        _Force_X = distancex / flyingtime;
        _Force_Y = distancey / flyingtime;
        transform.localEulerAngles = new Vector3(0,0,Mathf.Atan2(distancey,distancex)*Mathf.Rad2Deg);
        StartCoroutine(DestroyArrow(flyingtime));
    }

    private void Update()
    {
        transform.localPosition += new Vector3(_Force_X, _Force_Y, 0) * Time.smoothDeltaTime;
    }

    IEnumerator DestroyArrow(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}
