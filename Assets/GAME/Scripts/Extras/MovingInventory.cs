using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInventory : MonoBehaviour
{
    private Inventory inventory;
    public Vector2 target;
    public float speed = 3;
    private bool arrived = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(target);
        Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);

    }
}
