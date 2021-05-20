using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int maxHealth = 20;
    public string enemyName;
    public int baseAttack;
    public int currentHealth;
    public BossHealthBar healthBar;


    [Header("Effects")]
    public GameObject deathEffect;
    public LootTable thisLoot;

    [Header("Signals")]
    public SignalSender roomSignals;
    // Start is called before the first frame update
    private void Awake()
    {
        healthBar.GetComponent<BossHealthBar>();
        healthBar.gameObject.SetActive(true);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.TakeDamage(baseAttack);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
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
        healthBar.gameObject.SetActive(false);
        MakeLoot();
        this.gameObject.SetActive(false);

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
    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}
