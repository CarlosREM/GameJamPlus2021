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
        if (diagEvent != null)
        {
            diagEvent.nextDialogue();
        }
    }
}
