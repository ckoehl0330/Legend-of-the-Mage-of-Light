using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health;
    public FloatValue maxHealth;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Vector2 homePos;

    [Header("Effects")]
    public GameObject deathEffect;
    public LootTable thisLoot;

    [Header("Signals")]
    public SignalSender roomSignals;
    // Start is called before the first frame update
    private void Awake()
    {
        health = maxHealth.initialValue;
        
    }
    private void OnEnable()
    {
        transform.position = homePos;
        health = maxHealth.initialValue;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.TakeDamage(baseAttack);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            health = 0f;
            IsDead();
        }


    }
    public void IsDead()
    {
     
        
        
         DeathEffect();
         if (roomSignals != null)
         {
                roomSignals.Raise();
                Debug.Log("EnemyHealth.cs: Death room signal raised");
         }
         MakeLoot();
         this.gameObject.SetActive(false);
        
    }
    private void MakeLoot()
    {
        if(thisLoot != null)
        {
            PowerUp current = thisLoot.LootPowerUp();
            if(current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}
