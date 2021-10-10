using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameInteractable : InteractableObject
{

    [SerializeField] string levelName;
    public Animator player;
    DialogueEvent frameDiagEvent;

    public new void Start()
    {
        base.Start();
        frameDiagEvent = GetComponent<DialogueEvent>();
    }

    protected override void DoSequence()
    {
        frameDiagEvent.StartDialogue();
        StartCoroutine(checkDialogEnd());
    }

    IEnumerator checkDialogEnd()
    {
        while (!frameDiagEvent.seen)
        {
            yield return new WaitForEndOfFrame();
        }

        EndSequence();
    }

    protected override void EndSequence()
    {
        base.EndSequence();

        GameObject.Find("Scene Manager").GetComponent<TransitionManager>()
        .ChangeScene(levelName);
    }

}
