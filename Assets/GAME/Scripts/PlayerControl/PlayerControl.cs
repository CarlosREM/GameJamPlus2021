using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Vector2 target;
    public float speed;
    //public ParticleSystem mouseFx;
    private Vector2 mousePosition;

    [SerializeField] bool noInput = false;

    Animator animator;
    [SerializeField] SpriteRenderer characterSprite;

    void Start()
    {
        target = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!noInput && Input.GetMouseButtonDown(0))
        {
            target = mousePosition;

            animator.SetBool("isMoving", true);

            //mouseFx.transform.position = target;
            //mouseFx.Play();

            characterSprite.flipX = target.x < transform.position.x;
        }

        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);

        // para profundidad
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 10);

        if (Equals((Vector2) transform.position, target))
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void SetTarget(Vector2 newTarget)
    {
        target = newTarget;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            target = transform.position;
        }

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }


}


