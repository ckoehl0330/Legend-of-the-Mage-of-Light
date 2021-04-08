using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody2D;
    public float damageAmount = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidbody2D.velocity = velocity.normalized* speed;
        transform.rotation = Quaternion.Euler(direction);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.TakeDamage(damageAmount);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
