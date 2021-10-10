using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GirlInteractable : InteractableObject
{
    public Animator player;
    DialogueEvent[] frameDiagEvents;
    DialogueEvent currentDiagEvent;

    [SerializeField] CanvasGroup fadeCanvas;
    [SerializeField] float fadeDuration = 1f;

    int sequenceIndex = 0;

    [SerializeField] DoorObject classDoor;

    Animator girlAnim;

    public new void Start()
    {
        base.Start();
        frameDiagEvents = GetComponents<DialogueEvent>();
        girlAnim = GetComponent<Animator>();
    }

    protected override void DoSequence()
    {
        if (frameDiagEvents[2].seen)
            sequenceIndex = 3;

        else
            classDoor.locked = false;
        
        currentDiagEvent = frameDiagEvents[sequenceIndex];

        StartCoroutine(GirlSequence());
    }

    IEnumerator GirlSequence()
    {
        if (sequenceIndex < 3)
        {
            // first dialog event
            girlAnim.SetTrigger("Talk");
            yield return checkDialogEnd();

            player.SetBool("InputEnabled", false);
            InteractableObject.canInteract = false;

            girlAnim.SetTrigger("Idle");

            yield return new WaitForSeconds(1);

            float alpha = 0, t;
            while (alpha < 1)
            {
                t = alpha / fadeDuration;
                t = t * t * (3f - 2f * t);

                fadeCanvas.alpha = t;

                alpha += Time.unscaledDeltaTime;
                yield return new WaitForEndOfFrame();
            }

            // second dialog
            currentDiagEvent = frameDiagEvents[1];
            yield return checkDialogEnd();

            player.SetBool("InputEnabled", false);
            InteractableObject.canInteract = false;

            alpha = 1; t = 0;
            while (alpha > 0)
            {
                t = alpha / fadeDuration;
                t = t * t * (3f - 2f * t);

                fadeCanvas.alpha = t;

                alpha -= Time.unscaledDeltaTime;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(1);

            // third dialog
            girlAnim.SetTrigger("Happy Talk");
            currentDiagEvent = frameDiagEvents[2];
            yield return checkDialogEnd();

        }
        else
        {
            // if we already talked...
            girlAnim.SetTrigger("Happy Talk");
            yield return checkDialogEnd();

            currentDiagEvent.seen = false;
        }

        girlAnim.SetTrigger("Idle");
        EndSequence();
    }

    IEnumerator checkDialogEnd()
    {
        currentDiagEvent.StartDialogue();

        while (!currentDiagEvent.seen)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    protected override void EndSequence()
    {

        base.EndSequence();
    }


}
