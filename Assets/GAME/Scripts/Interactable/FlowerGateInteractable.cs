using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGateInteractable : InteractableObject
{
    public bool locked = true;

    [SerializeField] CanvasGroup fadeCanvas;
    [SerializeField] float fadeDuration;
    [SerializeField] Animator player;

    [SerializeField] GameObject flowerPickup;

    AudioSource audioSource;

    [SerializeField] AudioClip closedSound;
    [SerializeField] AudioClip openSound;

    new void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void DoSequence()
    {
        if (locked)
        {
            audioSource.clip = closedSound;
        }
        else
        {
            audioSource.clip = openSound;
            player.SetBool("InputEnabled", false);

            StartCoroutine(Transition());
        }

        audioSource.Play();

        if (locked)
        {
            EndSequence();
        }
    }

    IEnumerator Transition()
    {
        float alpha = 0, t;
        while (alpha < 1)
        {
            t = alpha / fadeDuration;
            t = t * t * (3f - 2f * t);

            fadeCanvas.alpha = t;

            alpha += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }

        flowerPickup.SetActive(true);

        yield return new WaitForSeconds(2);

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

        player.SetBool("InputEnabled", true);

        EndSequence();

        gameObject.SetActive(false);
        yield return null;
    }

}
