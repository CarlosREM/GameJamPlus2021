using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeacherInteractable : InteractableObject
{
    public Animator player;
    DialogueEvent[] frameDiagEvents;
    DialogueEvent currentDiagEvent;

    int sequenceIndex = 0;

    [SerializeField] DoorObject classDoor;

    public new void Start()
    {
        base.Start();
        frameDiagEvents = GetComponents<DialogueEvent>();
    }

    protected override void DoSequence()
    {
        currentDiagEvent = frameDiagEvents[sequenceIndex];

        switch (sequenceIndex)
        {
            case 0:
                classDoor.locked = false;
                sequenceIndex++;
                break;
            case 1:
                currentDiagEvent.seen = false;
                break;
        }
        currentDiagEvent.StartDialogue();

        StartCoroutine(checkDialogEnd());
    }

    IEnumerator checkDialogEnd()
    {
        while (!currentDiagEvent.seen)
        {
            yield return new WaitForEndOfFrame();
        }

        EndSequence();
    }

    protected override void EndSequence()
    {

        base.EndSequence();
    }


}
