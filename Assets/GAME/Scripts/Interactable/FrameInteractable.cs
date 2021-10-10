using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameInteractable : InteractableObject
{

    [SerializeField] string levelName;

    protected override void Interaction()
    {
        // do stuff...

        GameObject.Find("Scene Manager").GetComponent<TransitionManager>()
            .ChangeScene(levelName);
    }

}
