using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueEvent : MonoBehaviour
{
    public Animator player;
    private DialogueButton next;
    public TMPro.TextMeshProUGUI dial;
    public string[] dialogues;
    public int dialogueNum = 0;
    public bool seen = false;

    [SerializeField] bool isTrigger = false;

    private void Start()
    {
        next = dial.transform.Find("Next").GetComponent<DialogueButton>();
        next.SetEvent(this);
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
        dial.gameObject.SetActive(true);
        next.gameObject.SetActive(true);
        player.SetBool("InputEnabled", false);
        dial.text = dialogues[0];
    }

    public void nextDialogue()
    {
        dialogueNum++;
        if (dialogueNum == dialogues.Length)
        {
            player.SetBool("InputEnabled", true);
            dial.gameObject.SetActive(false);
            next.gameObject.SetActive(false);
            seen = true;
        }
        else if (dialogueNum == dialogues.Length + 1)
        {
            dialogueNum = 1;
            dial.text = dialogues[dialogueNum];
        }
        else if (dialogueNum < dialogues.Length)
        {
            dial.text = dialogues[dialogueNum];
        }

    }
}
