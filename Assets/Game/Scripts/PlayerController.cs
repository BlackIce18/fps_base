using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Header("Movement")]
    public float speed = 4f;
    public float gravity = -20f;
    public float jumpHeight = 4f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isRunning;
    private bool _isJumping;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Camera _camera;
    
    [SerializeField] private CharacterAnimatorController _animator;

    public Vector3 Velocity => _velocity;
    public bool IsGrounded => _isGrounded;
    public bool IsRunning => _isRunning;
    public bool IsJumping => _isJumping;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Gravity();
        Move();
    }
    
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        var camForward = _camera.transform.forward;
        var camRight = _camera.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        
        camForward.Normalize();
        camRight.Normalize();

        Vector3 movement = camForward * z + camRight * x;
        Vector3 targetVelocity = movement * speed;
        _velocity = _rb.linearVelocity;
        
        if (Mathf.Abs(targetVelocity.x) > 0 || Mathf.Abs(targetVelocity.z) > 0)
        {
            if (_isGrounded)
            {
                _isRunning = true;
            }
        }
        else
        {
            _isRunning = false;
        }
        
        if ((Input.GetAxis("Jump") > 0) && _isGrounded)
        {
            _velocity.y = jumpHeight;
            _isJumping = true;
        }
        
        _velocity.x = targetVelocity.x;
        _velocity.z = targetVelocity.z;
        _rb.linearVelocity = _velocity;
    }

    void Gravity()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!_isGrounded)
        {
            _velocity.y = -2f;
            _animator.Jump();
            _isJumping = true;
        }
        else
        {
            _isJumping = false;
        }

        _velocity.y += gravity * Time.deltaTime;

        _velocity = _velocity * Time.deltaTime;
    }
}
