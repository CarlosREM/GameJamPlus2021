using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevel1Start : MonoBehaviour
{

    DialogueEvent dialogueOnStart;
    DialogueEvent dialogueOnReturn;

    DialogueEvent SelectedDialog;

    [Header("On Start")]
    [SerializeField] float dialogueStartDelay = 1f;
    [SerializeField] float fadeDuration = 5f;
    [SerializeField] CanvasGroup fadeCanvas;


    // Start is called before the first frame update
    void Start()
    {
        GameInstance instance = GameObject.Find("Game Instance").GetComponent<GameInstance>();

        DialogueEvent[] dialogueEvents = gameObject.GetComponents<DialogueEvent>();

        SelectedDialog = dialogueEvents[instance.lastLevel];

        instance.lastLevel = 1;

        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        yield return new WaitForSecondsRealtime(dialogueStartDelay);

        SelectedDialog.StartDialogue();
        InteractableObject.canInteract = false;

        while (!SelectedDialog.seen)
        {
            yield return new WaitForEndOfFrame();
        }

        float alpha = 1, t;
        while(alpha > 0)
        {
            t = alpha / fadeDuration;
            t = t * t * (3f - 2f * t);

            fadeCanvas.alpha = t;

            alpha -= Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }

        Time.timeScale = 1;
        yield return new WaitForSeconds(0.5f);

        InteractableObject.canInteract = true;

        yield return null;
    }
}
