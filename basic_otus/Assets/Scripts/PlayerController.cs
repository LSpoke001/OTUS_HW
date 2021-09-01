using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Collider2D _collider2D;
    public Collider2D _playerCollider;
    public GameObject _restartCanvas;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool isJumpWall = false;
    private Vector2 jumpAngle = new Vector2(20f, 10f);

    public LayerMask ground;
    public LayerMask wall;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _restartCanvas.SetActive(false);
    }
    
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0.0f)
        {
            _rigidbody2D.velocity = new Vector2(-5f, _rigidbody2D.velocity.y);
            _spriteRenderer.flipX = true;
            _animator.SetBool("isRunning", true);
        } 
        else if (hDirection > 0.0f)
        {
            _rigidbody2D.velocity = new Vector2(5f, _rigidbody2D.velocity.y);
            _spriteRenderer.flipX = false;
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
            _animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _collider2D.IsTouchingLayers(ground) )
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 5f);
        }
        
        // Прыжок от стены
        
        if (Input.GetKeyDown(KeyCode.Space) && _playerCollider.IsTouchingLayers(wall) && !_collider2D.IsTouchingLayers(ground))
        {
            isJumpWall = true;
        }
        
        // Уничтожение игрока при падение
        
        if (transform.position.y < -3f)
        {
            Destroy(gameObject);
            _restartCanvas.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        // Прыжок от стены
        if (isJumpWall)
        {
            _rigidbody2D.velocity = new Vector2( _rigidbody2D.velocity.x, 7f);
            isJumpWall = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bonus"))
        {
            Destroy(other.gameObject);
        }
    }
}
