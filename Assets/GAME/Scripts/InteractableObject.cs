using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InteractableObject : MonoBehaviour
{

    [SerializeField] SpriteRenderer outlineRenderer;

    public void Start()
    {
        outlineRenderer.enabled = false;
    }

    public void OnMouseEnter()
    {
        outlineRenderer.enabled = true;
    }
    public void OnMouseExit()
    {
        outlineRenderer.enabled = false;
    }


    public void OnMouseDown()
    {
        Interaction();
    }

    protected void Interaction()
    {
        Debug.Log("You called?");
    }
}
