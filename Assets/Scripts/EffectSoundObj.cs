using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundObj : MonoBehaviour
{
    [SerializeField] AudioSource source;

    public void Using(AudioClip clip, float delay, Transform parent)
    {
        transform.parent = parent.parent;
        transform.localPosition = parent.localPosition;
        source.Stop();
        source.clip = clip;
        //source.Play();
        StartCoroutine(Play(delay));
    }

    IEnumerator Play(float time)
    {
        yield return new WaitForSeconds(time);

        if (source.clip)
        {
            source.Play();
            StartCoroutine(SetFalse(source.clip.length));
        }
        else
            gameObject.SetActive(false);
    }
    IEnumerator SetFalse(float time)
    {
        yield return new WaitForSeconds(time+0.5f);
        gameObject.SetActive(false);
    }
}
