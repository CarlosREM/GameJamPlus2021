using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloristaInteractable : InteractableObject
{
    public Animator player;
    DialogueEvent[] frameDiagEvents;
    DialogueEvent currentDiagEvent;

    Animator floristaAnim;

    [SerializeField] FlowerGateInteractable gateDoor;

    int sequenceIndex = 0;

    public new void Start()
    {
        base.Start();
        frameDiagEvents = GetComponents<DialogueEvent>();

        floristaAnim = GetComponent<Animator>();
    }

    protected override void DoSequence()
    {
        currentDiagEvent = frameDiagEvents[sequenceIndex];

        if (sequenceIndex == 0)
        {
            sequenceIndex++;
            gateDoor.locked = false;
        }
        else
        {
            currentDiagEvent.seen = false;
        }

        currentDiagEvent.StartDialogue();
        StartCoroutine(checkDialogEnd());
    }

    IEnumerator checkDialogEnd()
    {
        floristaAnim.SetTrigger("Talk");

        while (!currentDiagEvent.seen)
        {
            yield return new WaitForEndOfFrame();
        }

        floristaAnim.SetTrigger("Talk");

        EndSequence();
    }
}
