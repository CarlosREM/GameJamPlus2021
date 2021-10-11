using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateInteractable : InteractableObject
{
    AudioSource audioSource;

    [SerializeField] Transform travelPosition;
    [SerializeField] CanvasGroup fadeCanvas;
    [SerializeField] float fadeDuration = 2;

    DialogueEvent diagEvent;

    new void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        diagEvent = GetComponent<DialogueEvent>();
    }

    protected override void DoSequence()
    {
        Inventory playerInv = GameObject.FindWithTag("Player").GetComponent<Inventory>();

        if (playerInv.HasItem("flower"))
        {
            StartCoroutine(EndingSequence());
        }
        else
        {
            diagEvent.StartDialogue();
            EndSequence();
        }
    }

    IEnumerator EndingSequence()
    {
        audioSource.Play();
        PlayerControl player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

        player.transform.position = travelPosition.position;
        player.target = travelPosition.position;

        GameInstance instance = GameObject.Find("Game Instance").GetComponent<GameInstance>();

        instance.lastLevel = 2;
        instance.levelsPassed = 2;

        yield return new WaitForSeconds(2);

        float alpha = 0, t;
        while (alpha < 1)
        {
            t = alpha / fadeDuration;
            t = t * t * (3f - 2f * t);

            fadeCanvas.alpha = t;

            alpha += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2);

        GameObject.Find("Scene Manager").GetComponent<TransitionManager>().ChangeScene("MainLevel");
    }

}
