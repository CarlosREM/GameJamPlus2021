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

    protected override void Interaction()
    {
        DoSequence();
    }

    protected void DoSequence()
    {
        frameDiagEvent.StartDialogue();
        StartCoroutine(checkDialogEnd());
    }

    IEnumerator checkDialogEnd()
    {
        Debug.Log("Imma check when the dialog ends...");
        while (!frameDiagEvent.seen)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("It ended");

        EndSequence();
    }

    protected void EndSequence()
    {
        GameObject.Find("Scene Manager").GetComponent<TransitionManager>()
        .ChangeScene(levelName);
        canInteract = true;
    }

}
