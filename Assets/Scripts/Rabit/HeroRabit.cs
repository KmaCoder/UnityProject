using UnityEngine;

public class HeroRabit : MonoBehaviour
{
    public static HeroRabit LastRabit;
    
    public float Speed = 5;
    public float JumpSpeed = 5;
    public float MaxJumpTime = 2;
    public float InvulnerableTime = 4;

    private Rigidbody2D _myBody;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private int _layerId;

    private bool _canMove = true;
    private bool _isGrounded;
    private float _jumpTimeDelta;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector3 _initialScale;
    private Transform _initialParent;

    public float InvulnerableTimeLeft { get; private set; }
    public bool IsScaled { get; private set; }
    public bool IsDead { get; private set; }

    void Awake()
    {
        LastRabit = this;
    }
    
    void Start()
    {
        _myBody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _layerId = 1 << LayerMask.NameToLayer("Ground");
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _initialScale = transform.localScale;
        _initialParent = transform.parent;
    }

    void FixedUpdate()
    {
        GroundedCheck();
        if (_canMove)
        {
            MovementsController();
            JumpsController();
        }
    }

    void Update()
    {
        if (InvulnerableTimeLeft > 0)
        {
            InvulnerableTimeLeft -= Time.deltaTime;
            _animator.SetBool("invulnerable", true);
        }
        else
        {
            _animator.SetBool("invulnerable", false);
        }
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

        if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
            SetNewParent(transform, hit.transform);
        else
            SetNewParent(transform, _initialParent);
    }

    public void Die()
    {
        if (IsDead)
            return;
        _canMove = false;
        IsDead = true;
        _myBody.velocity = Vector2.zero;
        _animator.SetTrigger("die");
    }

    public void Revive()
    {
        _canMove = true;
        IsDead = false;
        MakeSmaller();
        InvulnerableTimeLeft = 0;
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _myBody.velocity = Vector2.zero;
        _myBody.angularVelocity = 0;
        _animator.SetTrigger("reset");
    }

    public void MakeBigger()
    {
        if (IsScaled)
            return;
        transform.localScale = _initialScale * 1.5f;
        IsScaled = true;
    }

    public void MakeSmaller()
    {
        if (!IsScaled || InvulnerableTimeLeft > 0)
            return;
        transform.localScale = _initialScale;
        IsScaled = false;
        InvulnerableTimeLeft = InvulnerableTime;
    }

    public void OnDieAnimationEnd()
    {
        Revive();
    }

    public void HitRabbit()
    {
        if (InvulnerableTimeLeft > 0)
            return;
        if(IsScaled)
            MakeSmaller();
        else 
            LevelController.Current.OnRabitDeath(this);
    }

    public void OnKillingOrk()
    {
        Vector2 vel = _myBody.velocity;
        vel.y = 5f;
        _myBody.velocity = vel;
    }

    static void SetNewParent(Transform obj, Transform newParent)
    {
        if (obj.transform.parent != newParent)
        {
            Vector3 pos = obj.transform.position;
            obj.transform.parent = newParent;
            obj.transform.position = pos;
        }
    }
}