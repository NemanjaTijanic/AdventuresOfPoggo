using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAccepter : Interactable
{
    private AudioSource source;
    public AudioClip pigClip;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        base.Interact();

        source.Stop();
        source.PlayOneShot(pigClip);

        if (QuestManager.instance.questTwoAccepted)
        {
            QuestManager.instance.CompleteQuestTwo();
        }
    }
}
