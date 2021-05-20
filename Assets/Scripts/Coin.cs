using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    public Inventory playerInventory;
    public int coinValue;

    // Start is called before the first frame update
    void Start()
    {
        powerUpSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {

            playerInventory.coins += coinValue;
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
