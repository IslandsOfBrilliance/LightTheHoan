using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DelayedLoop : MonoBehaviour
{
    public AudioClip audioClip;
    [Range(0f, 30f)]
    public float minDelay;
    [Range(0f, 1000f)]
    public float maxDelay;

    AudioSource source;
    bool playingAudio;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = audioClip;
    }

    IEnumerator PlayAudio()
    {
        playingAudio = true;
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);
        source.Play();
        playingAudio = false;
    }

    private void Update()
    {
        if (!source.isPlaying && !playingAudio)
            StartCoroutine(PlayAudio());
    }
}
