using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TreasureChest : InteractableStuff
{
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public SignalSender raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;
    private ControlsMaster controls;

    // Start is called before the first frame update
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
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public void Interact()
    {
        if (!isOpen)
        {
            OpenChest();
        }
        else
        {
            ChestIsAlreadyOpen();
        }
    }
    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        isOpen = true;
        contextOff.Raise();
        anim.SetBool("opened", true);
    }
    public void ChestIsAlreadyOpen()
    {
        dialogBox.SetActive(false);
        playerInventory.currentItem = null;
        raiseItem.Raise();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            contextOn.Raise();
            playerInRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            contextOff.Raise();
            playerInRange = false;
        }
    }
}
