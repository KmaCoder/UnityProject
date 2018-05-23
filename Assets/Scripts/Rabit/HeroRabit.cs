using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
    public float Speed = 5;
    public float JumpSpeed = 5;
    public float MaxJumpTime = 2;

    private Rigidbody2D _myBody;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private int _layerId;

    private bool _isGrounded;
    private float _jumpTimeDelta;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    void Start()
    {
        _myBody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _layerId = 1 << LayerMask.NameToLayer("Ground");
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        GroundedCheck();
        MovementsController();
        JumpsController();
    }

    void MovementsController()
    {
        float valueX = Input.GetAxis("Horizontal");
        if (Mathf.Abs(valueX) > 0)
        {
            Vector2 vel = _myBody.velocity;
            vel.x = valueX * Speed;
            _myBody.velocity = vel;
            _sprite.flipX = valueX < 0;
            _animator.SetBool("running", true);
        }
        else
        {
            _animator.SetBool("running", false);
        }
    }

    void JumpsController()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _jumpTimeDelta = MaxJumpTime;
        }

        if (_jumpTimeDelta > 0)
        {
            if (Input.GetButton("Jump"))
            {
                _jumpTimeDelta -= Time.deltaTime;
                Vector2 vel = _myBody.velocity;
                vel.y = JumpSpeed * (_jumpTimeDelta / MaxJumpTime);
                _myBody.velocity = vel;
            }
            else
            {
                _jumpTimeDelta = 0;
            }
        }
    }

    void GroundedCheck()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position + Vector3.up * 0.3f,
            transform.position + Vector3.down * 0.1f, _layerId);
        _isGrounded = hit;
        _animator.SetBool("jump", !_isGrounded);
    }

    public void Die()
    {
        _animator.SetTrigger("die");
    }

    public void Revive()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _myBody.velocity = Vector2.zero;
        _myBody.angularVelocity = 0;
        _animator.SetTrigger("reset");
    }
}