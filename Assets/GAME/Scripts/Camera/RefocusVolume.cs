using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RefocusVolume : MonoBehaviour
{
    enum RefocusType { Player, Area};


    [SerializeField] RefocusType type;

    [SerializeField] Vector2 focusOffset = Vector2.zero;

    [SerializeField] [Range(0.1f, 2f)]
    float refocusDuration = 1;

    [SerializeField] bool fitToSize = false;

    BoxCollider2D volumeArea;


    void Awake()
    {
        volumeArea = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RefocusCamera cam = Camera.main.GetComponent<RefocusCamera>();

            float camSize = -1;
            if (fitToSize)
                camSize = volumeArea.size.y / 2;

            switch (type)
            {
                case (RefocusType.Player):
                    cam.Refocus(other.transform, focusOffset, camSize, refocusDuration);
                    break;

                case (RefocusType.Area):
                    cam.Refocus(this.transform, focusOffset, camSize, refocusDuration);
                    break;
            }
        }
    }
}
