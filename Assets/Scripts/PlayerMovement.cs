using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [Header("current player state")]
    public PlayerState currentState;
    
    [Header("Player Inventory")]
    public Inventory playerInventory;
    public SpriteRenderer itemReceived;
    
    [Header("Player Attributes")]
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public VectorValue startingPosition;

    [Header("Player Abilities")]
    public GenericAbility currentAbility;
    public GameObject fireBall;
    private Vector2 facingDirection = Vector2.down;

    [Header("Player Movement, Input and Animation")]
    public float speed;
    private Rigidbody2D myRigidbody2D;
    private Animator anim;
    public InputActionAsset controls;
    private InputAction movement;
    private InputAction attackone;
    Vector2 myMove;
    
    


    // Start is called before the first frame update
    private void Awake()
    {

        var gameplayActionMap = controls.FindActionMap("Player");
        myRigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movement = gameplayActionMap.FindAction("Movement");
        movement.performed += OnMovementChanged;
        movement.canceled += OnMovementChanged;
        movement.Enable();

        attackone = gameplayActionMap.FindAction("Attack");
        attackone.performed += OnAttackChanged;

    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        transform.position = startingPosition.initialValue;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }
    private void OnMovementChanged(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        if (currentState == PlayerState.interact)
        {
            return;
        }
        myRigidbody2D.velocity += direction * speed;
        if (direction.x != 0)
        {
            anim.SetFloat("MoveX", myMove.x);
            anim.SetBool("IsMoving", true);
        }
        else if(direction.y != 0)
        {
            anim.SetFloat("MoveY", myMove.y);
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
        
       
        
        
         
    }
    private void OnAttackChanged(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                StartCoroutine(AttackCo(currentAbility.duration));
                break;
            case InputActionPhase.Canceled:
                StopCoroutine(AttackCo(currentAbility.duration));
                break;

        }

        StartCoroutine(AttackCo(currentAbility.duration));
    } 
    private IEnumerator AttackCo(float abilityDuration)
    {
        anim.SetBool("IsAttacking", true);
        currentState = PlayerState.attack;
        yield return null;
        
        currentAbility.Ability(transform.position, facingDirection, anim, myRigidbody2D);
       
        
        anim.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(abilityDuration);
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
