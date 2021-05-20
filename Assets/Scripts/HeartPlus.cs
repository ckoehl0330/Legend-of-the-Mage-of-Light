using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPlus : PowerUp
{
    public FloatValue heartContainers;
    public FloatValue playerHealth;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            heartContainers.RuntimeValue += 1;
            playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2;
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
