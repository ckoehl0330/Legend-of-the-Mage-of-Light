using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TalkingRock : InteractableStuff
{
    
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public ControlsMaster controls;

    // Start is called before the first frame update
    private void Awake()
    {
        controls.Player.Interact.performed += _ => Interact();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
    
    }
    public void Interact()
    {
        if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }
        else
        {
            dialogBox.SetActive(true);
            dialogText.text = dialog;
        }
    }
  
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            contextOff.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
