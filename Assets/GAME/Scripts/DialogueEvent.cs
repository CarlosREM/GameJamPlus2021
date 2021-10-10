using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueEvent : MonoBehaviour
{
    public Animator player;
    private DialogueButton next;
    public TMPro.TextMeshProUGUI dialogueUI;
    public string[] dialogues;
    public int dialogueNum = 0;
    public bool seen = false;

    [SerializeField] bool isTrigger = false;

    private void Start()
    {
        next = dialogueUI.transform.Find("Next").GetComponent<DialogueButton>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrigger && seen == false)
        {
            player.GetComponent<PlayerControl>().SetTarget(collision.transform.position);
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        dialogueUI.gameObject.SetActive(true);
        next.gameObject.SetActive(true);
        player.SetBool("InputEnabled", false);
        dialogueUI.text = dialogues[0];
        next.SetEvent(this);
    }

    public void nextDialogue()
    {
        dialogueNum++;
        if (dialogueNum == dialogues.Length)
        {
            player.SetBool("InputEnabled", true);
            dialogueUI.gameObject.SetActive(false);
            next.gameObject.SetActive(false);
            seen = true;
        }
        else if (dialogueNum == dialogues.Length + 1)
        {
            dialogueNum = 1;
            dialogueUI.text = dialogues[dialogueNum];
        }
        else if (dialogueNum < dialogues.Length)
        {
            dialogueUI.text = dialogues[dialogueNum];
        }

    }
}
