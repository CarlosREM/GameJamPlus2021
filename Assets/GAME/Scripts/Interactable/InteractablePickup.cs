using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PickUp))]
public class InteractablePickup : InteractableObject
{

    PickUp pickupComponent;

    public new void Start()
    {
        base.Start();

        pickupComponent = GetComponent<PickUp>();
    }

    protected override void DoSequence()
    {
        pickupComponent.LoadToInventory();
        EndSequence();
    }

}
