using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour
{
    public string levelName;
    
    public void ReturnToMain()
    {
        GameObject.Find("Scene Manager").GetComponent<TransitionManager>()
        .ChangeScene(levelName);
    }
}
