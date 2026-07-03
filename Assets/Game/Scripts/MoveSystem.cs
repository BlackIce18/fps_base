using System;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody _rigidbody;
    private bool _isGrounded;
    [SerializeField] private Transform model;
    [SerializeField] private Camera _camera;
    private float inputX;
    private float inputY;
    private void Start()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        var camForward = _camera.transform.forward;
        var camRight = _camera.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        
        camForward.Normalize();
        camRight.Normalize();

        Vector3 movement = camForward * inputY + camRight * inputX;
        Vector3 targetVelocity = movement * _speed;
        Vector3 velocity = _rigidbody.linearVelocity;

        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        velocity.y = _rigidbody.linearVelocity.y;
        //_rigidbody.linearVelocity = velocity;
        _rigidbody.linearVelocity = movement * _speed * Time.fixedDeltaTime;
    }

    private void Jump()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        IsGroundedUpdate(other, true);
    }

    private void OnCollisionExit(Collision other)
    {
        IsGroundedUpdate(other, false);
    }

    private void IsGroundedUpdate(Collision other, bool value)
    {

        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = value;
        }
    }
}
