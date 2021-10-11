using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckProps : MonoBehaviour
{
    public GameInstance gameStats;
    public GameObject selector;
    public GameObject bag;
    public GameObject photo;

    void Start()
    {
        gameStats = GameObject.Find("Game Instance").GetComponent<GameInstance>();
        Debug.Log(gameStats.levelsPassed);
        if(gameStats.levelsPassed == 1)
        {
            bag.SetActive(true);

        }
        else if(gameStats.levelsPassed == 2)
        {
            bag.SetActive(true);
            selector.SetActive(true);
            photo.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
