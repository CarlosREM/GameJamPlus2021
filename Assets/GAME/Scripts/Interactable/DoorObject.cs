using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : InteractableObject
{

    [SerializeField] Transform travelPosition;
    [SerializeField] bool FlipXOnTravel = false;

    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip lockedSound;

    public bool locked = false;

    AudioSource audioSource;

    new void Start()
    {
        base.Start();

        audioSource = GetComponent<AudioSource>();
    }

    protected override void DoSequence()
    {
        if (!locked)
        {
            PlayerControl player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

            player.transform.position = travelPosition.position;

            player.SetTarget(travelPosition.position);

            if (FlipXOnTravel)
            {
                bool flipX = player.characterSprite.flipX;
                player.GetComponent<PlayerControl>().characterSprite.flipX = !flipX;
            }

            audioSource.clip = openSound;
        }
        else
        {
            audioSource.clip = lockedSound;
        }

        audioSource.Play();

        EndSequence();
    }

}
