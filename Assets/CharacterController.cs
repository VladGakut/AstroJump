using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animation;
    [SerializeField] private float _thrustForce = 5f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private Vector3 _jumpDirection = Vector3.up;
    
    [Header("Ground")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private Vector3 _groundCheckSize = new Vector3(1,0.2f, 1);
    
    [Header("Jetpack Particles")]
    [SerializeField] private ParticleSystem _jetPackThrustEffectLeft;
    [SerializeField] private ParticleSystem _jetPackThrustEffectRights;

    private Vector3 _inputs;
    private bool _isGrounded;
    private bool _isFlying = false;
    private bool _allowToFly = true;
    private FlyingState _flyingState = FlyingState.Grounded;
    
    private enum FlyingState
    {
        Grounded = 0,
        Up = 1,
        Down = 2,
    }

    private float _speed = 5f; // TODO: unnecessary, only for MovePosition
    
    private static readonly int Forward = Animator.StringToHash("Forward");
    private static readonly int Jump = Animator.StringToHash("Jump");

    private void Update()
    {
        _isGrounded = Physics.CheckBox(_groundCheckTransform.position, _groundCheckSize, Quaternion.identity,
            _groundLayer, QueryTriggerInteraction.Ignore);
        
        _flyingState = Input.GetButton("Jump")
            ? FlyingState.Up
            : _isGrounded
                ? FlyingState.Grounded
                : FlyingState.Down;

        if (_flyingState == FlyingState.Down)
        {
            _allowToFly = false;
        }
        else if (_flyingState == FlyingState.Grounded)
        {
            _allowToFly = true;
        }

        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");

        if (_inputs != Vector3.zero)
        {
            transform.forward = _inputs;
        }

        if (_flyingState == FlyingState.Up && _allowToFly)
        {
            //_animation.SetTrigger(Jump);
            //_rigidbody.AddForce(Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            if (!_jetPackThrustEffectLeft.isPlaying)
                _jetPackThrustEffectLeft.Play();
           if (!_jetPackThrustEffectRights.isPlaying)
               _jetPackThrustEffectRights.Play();
        }
        else
        {
            _jetPackThrustEffectLeft.Stop();
            _jetPackThrustEffectRights.Stop();
        }

        _animation.SetFloat(Forward, _inputs.magnitude);
    }

    private void FixedUpdate()
    {
        if (_flyingState == FlyingState.Up && _allowToFly)
        {
            Vector3 localJumpDirection = GetLocalJumpDirection();
            _rigidbody.AddForce(localJumpDirection * _thrustForce, ForceMode.VelocityChange);
        }

        _rigidbody.MovePosition(_rigidbody.position + _inputs * _speed * Time.fixedDeltaTime);
    }

    private Vector3 GetLocalJumpDirection()
    {
        return transform.InverseTransformVector(_jumpDirection);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_groundCheckTransform.position, _groundCheckSize);
    }
}
 