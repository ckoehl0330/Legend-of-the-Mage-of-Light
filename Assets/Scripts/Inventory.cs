using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfSmallKeys;
    public int numberOfBossKeys;
    public int coins;
   
    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd.isSmallKey)
        {
            numberOfSmallKeys++;
        }
        else if (itemToAdd.isBossKey)
        {
            numberOfBossKeys++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }
}
