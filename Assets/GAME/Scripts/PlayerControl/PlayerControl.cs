using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 target;
    public float speed;
    //public ParticleSystem mouseFx;
    private Vector2 mousePosition;

    void Start()
    {
        target = new Vector2(transform.position.x, transform.position.y);
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

        }

    }




}


