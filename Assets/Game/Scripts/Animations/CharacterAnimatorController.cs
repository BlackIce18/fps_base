using System;
using Unity.Netcode;
using UnityEngine;

public class CharacterAnimatorController : NetworkBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Animator _animator;
    private NetworkVariable<bool> _isGrounded = new(writePerm: NetworkVariableWritePermission.Owner);
    private NetworkVariable<float> _speedX = new(writePerm: NetworkVariableWritePermission.Owner);
    private NetworkVariable<float> _speedY = new(writePerm: NetworkVariableWritePermission.Owner);
    private NetworkVariable<bool> _isRunning = new(writePerm: NetworkVariableWritePermission.Owner);
    private NetworkVariable<bool> _isJumping = new(writePerm: NetworkVariableWritePermission.Owner);
    private void FixedUpdate()
    {
        if (IsOwner)
        {
            _isGrounded.Value = _playerController.IsGrounded;
            _isRunning.Value = _playerController.IsRunning;
            _isJumping.Value = _playerController.IsJumping;
            
            _speedX.Value = _playerController.Velocity.x;
            _speedY.Value = _playerController.Velocity.z;
        }

        _animator.SetBool("OnGround", _playerController.IsGrounded);
        _animator.SetBool("IsRunning", _isRunning.Value);
        _animator.SetBool("Jump", _isJumping.Value);
        _animator.SetFloat("SpeedX", _speedX.Value);
        _animator.SetFloat("SpeedY", _speedY.Value);
    }

    public void Jump()
    {    
        //_animator.Play("Jump");
    }

    public void Running()
    {
        //_animator.Play("Running");
    }
}
