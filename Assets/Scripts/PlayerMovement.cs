using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    stagger,
    attack,
    interact
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    public Inventory playerInventory;
    public SpriteRenderer itemReceived;
    private Rigidbody2D myRigidbody2D;
    private Animator anim;
    private Vector3 change;
    public GameObject fireBall;
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public VectorValue startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == PlayerState.interact)
        {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimatorAndMove();
        }
    }
    private IEnumerator AttackCo()
    {
        anim.SetBool("IsAttacking", true);
        currentState = PlayerState.attack;
        yield return null;
        CastFireball();
        anim.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(0.5f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }
    public void RaiseItem()
    {
        if(playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                anim.SetBool("ReceivedItem", true);
                currentState = PlayerState.interact;
                itemReceived.sprite = playerInventory.currentItem.itemSprite;
            }
        
        }
        else
        {
            anim.SetBool("ReceivedItem", false);
            currentState = PlayerState.idle;
            itemReceived.sprite = null;
            playerInventory.currentItem = null;
        }
    }
    private void CastFireball()
    {
        Vector2 temp = new Vector2(anim.GetFloat("MoveX"), anim.GetFloat("MoveY"));
        FireballSpell flamebomb = Instantiate(fireBall, transform.position, Quaternion.identity).GetComponent <FireballSpell> ();
        flamebomb.Setup(temp, ChooseArrowDirection());
    }
    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(anim.GetFloat("MoveY"), anim.GetFloat("MoveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
    void UpdateAnimatorAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            anim.SetFloat("MoveX", change.x);
            anim.SetFloat("MoveY", change.y);
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody2D.MovePosition (transform.position + change * speed * Time.deltaTime);

    }
    public void TakeDamage(float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if(currentHealth.RuntimeValue <= 0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
