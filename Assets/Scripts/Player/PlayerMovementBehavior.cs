using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBehavior : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    private PlayerInput _playerInput;

    public LayerMask longGrassLayer;

    public float speed = 5f;

    private Vector2 _input;
    private string currentState;

    private bool _isMoving;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        _playerInput = new PlayerInput();

        //Move Controls performed
        _playerInput.Land.Move.performed += ctx => _input = ctx.ReadValue<Vector2>();
        _playerInput.Land.Move.performed += ctx => _isMoving = true;

        //Move controls canceled
        _playerInput.Land.Move.canceled += ctx => _input = Vector2.zero;
        _playerInput.Land.Move.canceled += ctx => _isMoving = false;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Fixes diagnal input
        if(_input.x != 0)
        {
            _input.y = 0;
        }


        if (_input != Vector2.zero)
        {

            animator.SetFloat("moveX", _input.x);
            animator.SetFloat("moveY", _input.y);
        }
        
        rb.velocity = new Vector2(_input.x * speed, _input.y * speed);

        animator.SetBool("isMoving", _isMoving);

        CheckForEncounters();
    }


    //Checks for an encounter while the player is moving in grass
    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, longGrassLayer) != null && _isMoving)
        {
            if (Random.Range(1, 500) <= 10) //random value can be changed
            {
                Debug.Log("Encounter!");
            }
        }
    }
}
