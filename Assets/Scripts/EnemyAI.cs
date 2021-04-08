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

    [Header("Effects")]
    public GameObject deathEffect;

    [Header("Signals")]
    public SignalSender roomSignals;
    // Start is called before the first frame update
    private void Awake()
    {
        health = maxHealth.initialValue;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            health = 0f;
            DeathEffect();
            if (roomSignals != null)
            {
                roomSignals.Raise();
                Debug.Log("EnemyHealth.cs: Death room signal raised");
            }
            this.gameObject.SetActive(false);
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
