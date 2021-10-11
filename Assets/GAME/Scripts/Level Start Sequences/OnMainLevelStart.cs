using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnMainLevelStart : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Animator playerAnim;

    [Header("Start UI")]

    [SerializeField] CanvasGroup canvas;

    [SerializeField] CanvasGroup title;
    [SerializeField] float titleShowDelay;
    [SerializeField] float titleShowDuration;

    bool clickable = false;

    [SerializeField] CanvasGroup promptIcon;
    [SerializeField] float promptShowDelay;
    [SerializeField] float promptShowDuration;

    [Header("On Menu Click")]
    [SerializeField] float fadeoutDuration = 1f;
    [SerializeField] float refocusDuration = 1f;
    [SerializeField] float playerEnterDelay = 1f;
    [SerializeField] Vector2 offsetOnPlay = Vector2.zero;
    [SerializeField] CanvasGroup InventoryUI;
    [SerializeField] float InventoryShowTime;

    [Header("Paintings")]
    [SerializeField] GameObject[] paintingsArray;

    [Header("Thought Arrays")]
    [SerializeField] GameObject thoughtsBegin;
    [SerializeField] GameObject[] thoughts1Fails;
    [SerializeField] GameObject[] thoughts1Completed;

    [Header("Player Stuff")]
    [SerializeField] GameObject baul;
    [SerializeField] GameObject photo;
    [SerializeField] GameObject[] icons;
    [SerializeField] Inventory inventoryMain;

    void Start()
    {
        title.alpha = 0;
        promptIcon.alpha = 0;

        InventoryUI.alpha = 0;

        GameObject.FindWithTag("PostProcessingGlobal")
            .GetComponent<CameraEffects>()
            .SetDepth(130, 0);


        GameInstance instance = GameObject.Find("Game Instance").GetComponent<GameInstance>();
        if (instance.GameStart)
        {
            canvas.alpha = 1;
            instance.GameStart = false;
            InteractableObject.canInteract = false;
            thoughtsBegin.SetActive(true);

            playerAnim.SetBool("InputEnabled", false);
            playerAnim.SetTrigger("Sit");
            StartCoroutine(ShowUI());
        }
        else
        {
            canvas.alpha = 0;
            RemoveBlur();
            CenterCamera();
            PreviousLevelAction(instance);
            StartCoroutine(ShowInventory());
        }
    }

    void Update()
    {
        if (clickable && Input.GetMouseButtonDown(0))
        {
            StopCoroutine(ShowUI());
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

        yield return new WaitForSeconds(refocusDuration);

        yield return new WaitForSeconds(playerEnterDelay);

        playerAnim.SetTrigger("Getup");
        playerAnim.SetBool("InputEnabled", true);
        CenterCamera();
        InteractableObject.canInteract = true;

        StartCoroutine(ShowInventory());

        yield return null;
    }

    void RemoveBlur()
    {
        GameObject.FindWithTag("PostProcessingGlobal")
            .GetComponent<CameraEffects>()
            .SetDepth(50, refocusDuration);
    }

    void CenterCamera()
    {
        GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<RefocusCamera>()
            .SetCenterOffset(offsetOnPlay);
    }


    void PreviousLevelAction(GameInstance instance)
    {
        Debug.Log(instance.lastLevel);
        Vector3 newPosition = Vector3.zero;
        switch(instance.lastLevel)
        {
            case 1:
                newPosition = paintingsArray[0].transform.position;
                if (instance.levelsPassed == 1)
                {
                    paintingsArray[0].SetActive(false);
                    paintingsArray[1].SetActive(true);

                    if (!instance.seenCinematics[0])
                    {
                        baul.SetActive(true);
                        instance.seenCinematics[0] = true;
                    }
                    else
                    {
                        inventoryMain.isFull[0] = true;
                        inventoryMain.objNames[0] = "baul";
                        Instantiate(icons[0], inventoryMain.slots[0].transform, false);
                    }
                }
                break;
            case 2:
                paintingsArray[0].SetActive(false);
                paintingsArray[1].SetActive(true);

                newPosition = paintingsArray[1].transform.position;

                inventoryMain.isFull[0] = true;
                inventoryMain.objNames[0] = "baul";
                Instantiate(icons[0], inventoryMain.slots[0].transform, false);

                if (instance.levelsPassed == 2)
                {
                    paintingsArray[1].SetActive(false);

                    if (!instance.seenCinematics[1])
                    {
                        photo.SetActive(true);
                        instance.seenCinematics[1] = true;
                    }
                }
                break;
            default:
                Debug.Log("Last level invalid");
                instance.lastLevel = 0;
                break;
        }

        if (newPosition != Vector3.zero)
        {
            PlayerControl player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
            player.transform.position = newPosition;
            player.SetTarget(newPosition);
        }

        InteractableObject.canInteract = true;

    }

    IEnumerator ShowInventory()
    {
        float t = 0;
        while (t < 1)
        {
            InventoryUI.alpha = t / InventoryShowTime;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        InventoryUI.alpha = 1;

        yield return null;
    }
}
