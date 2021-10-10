using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public string[] objNames;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool checkIfPlayerHasIt(string objName)
    {
        for (int i=0; i<objNames.Length; i++)
        {
            if (objName == objNames[i])
            {
                return true;
            }
        }
        return false;
    }
}
