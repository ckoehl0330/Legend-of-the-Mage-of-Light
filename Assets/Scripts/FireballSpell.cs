using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody2D;
    public int damageAmount = 2;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Setup(Vector2 moveDirection)
    {
        myRigidbody2D.velocity = moveDirection.normalized * speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>(); 
        Boss boss = other.GetComponent<Boss>();
        
        if (enemy != null)
        {
            enemy.TakeDamage(damageAmount);
        }
        if (boss != null)
        {
            boss.TakeDamage(damageAmount);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Breakable"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Collision"))
        {
            Destroy(this.gameObject);
        }
    }
}
