using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRooms : Rooms
{
    
    public Boss[] bosses;
    
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < bosses.Length; i++)
            {
                ChangeActivation(bosses[i], true);
            }
            vCam.SetActive(true);
        }
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
      
            for (int i = 0; i < bosses.Length; i++)
            {
                ChangeActivation(bosses[i], false);
            }
            vCam.SetActive(false);
        }
    }
   
}
