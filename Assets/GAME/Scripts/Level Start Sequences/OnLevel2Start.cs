using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevel2Start : MonoBehaviour
{
    void Start()
    {
        GameInstance instance = GameObject.Find("Game Instance").GetComponent<GameInstance>();

        instance.lastLevel = 2;
    }
}
