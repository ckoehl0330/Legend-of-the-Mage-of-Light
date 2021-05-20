using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum DoorType
{
    smallKey,
    challenge,
    bossKey
}

public class Doors : InteractableStuff
{
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsForDoor;
    public BoxCollider2D thisTriggerArea;
    private ControlsMaster controls;

    private void Awake()
    {
        controls = new ControlsMaster();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    void Update()
    {
        float interactInput = controls.Player.Interact.ReadValue<float>();
        if(interactInput > .1f)
        {
            Interact();
        }
          
    }
    public void Interact()
    {
        if (playerInRange && thisDoorType == DoorType.smallKey)
        {
            if (playerInventory.numberOfSmallKeys > 0)
            {
                playerInventory.numberOfSmallKeys--;
                Open();
            }
        }
        if (playerInRange && thisDoorType == DoorType.bossKey)
        {
            if (playerInventory.numberOfBossKeys > 0)
            {
                playerInventory.numberOfBossKeys--;
                Open();
            }
        }
    }
   public void Open()
    {
        doorSprite.enabled = false;
        open = true;
        physicsForDoor.enabled = false;
        thisTriggerArea.enabled = true;
    }
   public void Close()
    {
        doorSprite.enabled = true;
        open = false;
        physicsForDoor.enabled = true;
        thisTriggerArea.enabled = false;
    }
}
