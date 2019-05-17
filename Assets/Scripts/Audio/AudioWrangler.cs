using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWrangler : MonoBehaviour
{
    public static AudioWrangler Instance;
    public List<Interactable> audioInteractables;

    AudioSource source;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PlayerInput.A)
            Skip();

    }

    public void Play(AudioClip clip)
    {
        if (!source.isPlaying)
        {
            source.clip = clip;
            source.Play();

            foreach (Interactable interactable in audioInteractables)
                interactable.ChangeInteraction(false);

            StartCoroutine(WaitForClip());
        }
    }

    IEnumerator WaitForClip()
    {
        yield return new WaitUntil(() => !source.isPlaying);
        foreach (Interactable interactable in audioInteractables)
            interactable.ChangeInteraction(true);
    }

    public void Skip()
    {
        if(source.isPlaying)
        {
            source.Stop();
            foreach (Interactable interactable in audioInteractables)
                interactable.ChangeInteraction(true);
        }
    }
}
