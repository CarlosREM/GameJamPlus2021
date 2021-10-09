using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 target;
    public float speed;
    //public ParticleSystem mouseFx;
    private bool canMove;
    private Vector2 mousePosition;
    public GameObject label;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            target = new Vector2(mousePosition.x,mousePosition.y);
            //mouseFx.transform.position = target;
            //mouseFx.Play();

        }

        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Choca con algo");

        if (collision.collider.tag == "Obstacle")
        {
            Debug.Log("Choca con obstaculo");

            target.x = transform.position.x;
            target.y = transform.position.y;

        }else if(collision.collider.tag == "NPC")
        {
            target.x = transform.position.x;
            target.y = transform.position.y;
            label.SetActive(true);
            
        }

        
    }


}


