using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnMainLevelStart : MonoBehaviour
{
    [Header("Start UI")]

    [SerializeField] CanvasGroup canvas;

    [SerializeField] CanvasGroup title;
    [SerializeField] float titleShowDelay;
    [SerializeField] float titleShowDuration;

    bool clickable = false;

    [SerializeField] CanvasGroup promptIcon;
    [SerializeField] float promptShowDelay;
    [SerializeField] float promptShowDuration;

    [Header("On Click")]
    [SerializeField] float fadeoutDuration = 1f;
    [SerializeField] float refocusDuration = 1f;



    void Start()
    {
        title.alpha = 0;
        promptIcon.alpha = 0;

        GameObject.FindWithTag("PostProcessingGlobal")
            .GetComponent<CameraEffects>()
            .SetDepth(300, 0);

        if (GameInstance.Instance.GameStart)
        {
            canvas.alpha = 1;
            GameInstance.Instance.GameStart = false;
            StartCoroutine(ShowUI());
        }
        else
        {
            canvas.alpha = 0;
            RemoveBlur();
        }
    }

    void Update()
    {
        if (clickable && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(HideMenu());
            clickable = false;
        }
    }

    IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(titleShowDelay);

        float t = 0;
        while (t < titleShowDuration)
        {
            title.alpha = t / titleShowDuration;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        title.alpha = 1;

        clickable = true;

        yield return new WaitForSeconds(promptShowDelay);

        t = 0;
        while (t < promptShowDuration)
        {
            promptIcon.alpha = t / promptShowDuration;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        promptIcon.alpha = 1;

        yield return null;
    }

    IEnumerator HideMenu()
    {
        float t = 1;
        while (t > 0)
        {
            canvas.alpha = t / fadeoutDuration;
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        canvas.alpha = 0;

        RemoveBlur();
        yield return null;
    }

    void RemoveBlur()
    {
        GameObject.FindWithTag("PostProcessingGlobal")
            .GetComponent<CameraEffects>()
            .SetDepth(50, refocusDuration);
    }
}
