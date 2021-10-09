using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance { get; private set; }

    public bool GameStart = false;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameInstance");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        GameInstance.Instance = this;
    }
}
