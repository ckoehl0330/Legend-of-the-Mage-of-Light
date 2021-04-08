using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonChallengeRooms : OverworldRooms
{
    public Doors[] doors;
    public TreasureChest[] chests;

    //public SignalListener enemyUpdate;
    //public Vector3 transportPlayer;
   // public bool movePlayer;

    public int mobsToKill;
    int mobsRemain;

    // Start is called before the first frame update
    void Start()
    {
        HideChests();
        mobsRemain = mobsToKill;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckEnemies()
    {
        Debug.LogWarning("Check Enemies Called");
        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    if (enemies[i].gameObject.activeInHierarchy )
        //    {
        //        //Debug.Log("enemy active: " + i);
        //        return;
        //    }

        //}
        mobsRemain--;
        if (mobsRemain > 0)
        {


            Debug.Log("Mobs Remaining = " + mobsRemain);
            return;
        }

        OpenDoors();
        ShowChests();

    }


    public void CloseDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Close();

        }
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
    }

    public void HideChests()
    {
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].gameObject.SetActive(false);
        }
    }

    public void ShowChests()
    {
        Debug.Log("ShowChests()");
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].gameObject.SetActive(true);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // activate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }

            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            CloseDoors();
            vCam.SetActive(true);
        }


    }

}
