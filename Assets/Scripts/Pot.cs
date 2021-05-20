using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator anim;
    public LootTable thisLoot;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Smash()
    {
        StartCoroutine(SmashCo());
    }
    private IEnumerator SmashCo()
    {
        anim.SetBool("IsDestroyed", true);
        yield return new WaitForSeconds(1f);
        MakeLoot();
        gameObject.SetActive(false);
    }
    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            PowerUp current = thisLoot.LootPowerUp();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
