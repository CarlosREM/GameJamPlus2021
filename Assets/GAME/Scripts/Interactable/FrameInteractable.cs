using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameInteractable : InteractableObject
{

    [SerializeField] string levelName;
    public Animator player;
    DialogueEvent[] frameDiagEvents;
    DialogueEvent diagEvent;

    public new void Start()
    {
        base.Start();
        frameDiagEvents = GetComponents<DialogueEvent>();
    }

    protected override void DoSequence()
    {
        GameInstance instance = GameObject.Find("Game Instance").GetComponent<GameInstance>();

        diagEvent = frameDiagEvents[0];
        if (instance.lastLevel == 1)
            diagEvent = frameDiagEvents[1];

        diagEvent.StartDialogue();
        StartCoroutine(checkDialogEnd());
    }

    IEnumerator checkDialogEnd()
    {
        while (!diagEvent.seen)
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
