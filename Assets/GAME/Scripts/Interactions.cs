using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactions : InteractableObject
{
    public Animator player;
    public Button next;
    public TMPro.TextMeshProUGUI dial;
    public string[] dialogues;
    private int dialogueNum = 0;
    

    protected override void Interaction()
    {
        dial.gameObject.SetActive(true);
        next.gameObject.SetActive(true);
        player.SetBool("InputEnabled", false);
        dial.text = dialogues[0];

    }

    public void nextDialogue()
    {
        dialogueNum ++;
        if (dialogueNum == dialogues.Length)
        {
            player.SetBool("InputEnabled", true);
            dial.gameObject.SetActive(false);
            next.gameObject.SetActive(false);
        }
        else
        {
            dial.text = dialogues[dialogueNum];
        }
        
    }
}
