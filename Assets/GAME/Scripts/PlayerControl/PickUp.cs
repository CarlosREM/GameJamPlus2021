using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public string itemName;
    public GameObject item;
    public bool destroyObj = false;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadToInventory();
        }
    }

    public void LoadToInventory()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.objNames[i] = itemName;
                inventory.isFull[i] = true;
                Instantiate(item, inventory.slots[i].transform, false);
                
                if (destroyObj) Destroy(gameObject);

                break;
            }
        }
    }

   
}
