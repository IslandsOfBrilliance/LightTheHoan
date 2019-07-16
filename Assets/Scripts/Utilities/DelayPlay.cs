using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPlay : MonoBehaviour
{
    public float delay;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        source.Play();
    }
}
