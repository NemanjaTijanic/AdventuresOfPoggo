using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : Interactable
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

        if (!QuestManager.instance.questOneAccepted)
        {
            QuestManager.instance.AcceptQuestOne();
        }

        if (QuestManager.instance.questOneCompleted)
        {
            QuestManager.instance.AcceptQuestTwo();
        }
    }
}
