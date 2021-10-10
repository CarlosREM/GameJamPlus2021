using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableObject : MonoBehaviour
{

    [SerializeField] SpriteRenderer outlineRenderer;

    [SerializeField] float playerCheckTimeWindow = 5f;

    public void Start()
    {
        transform.position = new Vector3(transform.position.x,
                                        transform.position.y,
                                        transform.position.y / 10);
        outlineRenderer.gameObject.SetActive(false);
    }

    public void OnMouseEnter()
    {
        outlineRenderer.gameObject.SetActive(true);
        Debug.Log("Mouse Entered");
    }
    public void OnMouseExit()
    {
        outlineRenderer.gameObject.SetActive(false);
        Debug.Log("Mouse Exit");
    }

    public void OnMouseOver()
    {
        Debug.Log("Mouse Over");

    }

    public void OnMouseDown()
    {
        Debug.Log("Mouse Down");

        StartCoroutine(SetPlayerTarget());
        StartCoroutine(PollPlayerPosition());
    }
    
    IEnumerator SetPlayerTarget()
    {
        yield return new WaitForSeconds(0.01f);

        PlayerControl player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        player.SetTarget(transform.position);

        yield return null;
    }

    IEnumerator PollPlayerPosition()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        bool playerHere = false;
        float t = playerCheckTimeWindow;
        while (t > 0)
        {
            t -= Time.deltaTime;

            if (player.position == transform.position)
            {
                playerHere = true;
                break;
            }    

            yield return new WaitForEndOfFrame();
        }

        if (playerHere)
            Interaction();

        yield return null;
    }

    protected virtual void Interaction()
    {
        Debug.Log("Interacting...");
    }

    
}
