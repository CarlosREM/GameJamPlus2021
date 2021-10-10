using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevel1Start : MonoBehaviour
{

    DialogueEvent dialogueOnStart;
    DialogueEvent dialogueOnReturn;

    // Start is called before the first frame update
    void Start()
    {
        GameInstance instance = GameObject.Find("Game Instance").GetComponent<GameInstance>();

        if (instance.lastLevel != 1)
        {
            instance.lastLevel = 1;
            Time.timeScale = 0;
            StartCoroutine(StartSequence());
        }

        DialogueEvent[] dialogueEvents = gameObject.GetComponents<DialogueEvent>();
        dialogueOnStart = dialogueEvents[0];
        dialogueOnReturn = dialogueEvents[1];
    }

    IEnumerator StartSequence()
    {
        dialogueOnStart.StartDialogue();

        while(dialogueOnStart.seen)
        {
            yield return new WaitForEndOfFrame();
        }

        Time.timeScale = 1;
        yield return null;
    }
}
