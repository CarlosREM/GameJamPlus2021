using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : InteractableObject
{

    [SerializeField] Transform travelPosition;
    [SerializeField] bool FlipXOnTravel = false;

    public bool locked = false;

    AudioSource audioSource;

    new void Start()
    {
        base.Start();

        audioSource = GetComponent<AudioSource>();
    }

    protected override void DoSequence()
    {
        Debug.Log("door...");

        if (!locked)
        {
            PlayerControl player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

            player.transform.position = travelPosition.position;

            player.SetTarget(travelPosition.position);

            audioSource.Play();

            if (FlipXOnTravel)
            {
                bool flipX = player.characterSprite.flipX;
                player.GetComponent<PlayerControl>().characterSprite.flipX = !flipX;
            }
        }

        EndSequence();
    }

}
