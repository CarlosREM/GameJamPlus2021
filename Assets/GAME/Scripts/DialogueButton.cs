using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    public DialogueEvent diagEvent = null;

    public void SetEvent(DialogueEvent newEvent)
    {
        diagEvent = newEvent;
    }

    public void ActionOnClick()
    {
        Debug.Log("AAAAAAA");
        if (diagEvent != null)
        {
            diagEvent.nextDialogue();
        }
    }
}
