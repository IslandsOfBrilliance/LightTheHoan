using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInteractable : Interactable
{
    public AudioClip clip;

    public override void OnSelected()
    {
        base.OnSelected();
        AudioWrangler.Instance.Play(clip);
    }
}
