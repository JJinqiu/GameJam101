using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _renderer;
    private bool _isGround;
    private bool _jumpPressed;
    private Color _originColor;
    private int _jumpCount;
    private bool _isSprint;
    private bool _sprintEnabled;
    private int _lastIndex;
    private bool _attackEnable; // 近战攻击

    [SerializeField] private Collider2D circleCollider;
    [SerializeField] private Collider2D boxCollider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform knifeHitBox;
    [SerializeField] private Transform bulletHitBox;

    public LayerMask ground;
    public Transform knife;

    public float walkSpeed;
    public float jumpForce;
    public float coolDown;
    public float attackTime;
    public int jumpCount;
    public int health;
    public int rangeCount;

    public float sprintTime;
    public float sprintSpeed;
    public float durationTime;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _originColor = _renderer.color;
        _sprintEnabled = true;
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(ChangeAbilty());
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _jumpCount > 0)
        {
            _jumpPressed = true;
        }

        if (Input.GetButtonDown("Sprint") && !_isSprint && _sprintEnabled)
        {
            _sprintEnabled = false;
            _isSprint = true;
            StartCoroutine(SprintLoad());
            StartCoroutine(SprintTimeCount());
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        knife.gameObject.SetActive(true);
        StartCoroutine(AttackTime());
    }

    private void FixedUpdate()
    {
        _isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        GroundMovement();
        Jump();
        Sprint();
    }

    private void Sprint() // 冲刺
    {
        if (_isSprint)
        {
            _renderer.color = Color.red;
            if (transform.localScale.x > 0)
            {
                _rigidbody2D.velocity = new Vector2(sprintSpeed, 0);
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(-sprintSpeed, 0);
            }
        }
    }

    private void GroundMovement() // 角色移动
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        _rigidbody2D.velocity = new Vector2(horizontalMove * walkSpeed, _rigidbody2D.velocity.y);
        if (horizontalMove > 0) // 更改左右移动时角色朝向
        {
            transform.localScale = new Vector3(5, 8, 1);
        }
        else if (horizontalMove < 0)
        {
            transform.localScale = new Vector3(-5, 8, 1);
        }
    }

    private void Jump()
    {
        if (_isGround)
        {
            _jumpCount = jumpCount;
        }

        if (_jumpPressed && _isGround)
        {
            _isGround = false;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
            --_jumpCount;
            _jumpPressed = false;
        }
        else if (_jumpPressed && _jumpCount > 0)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
            --_jumpCount;
            _jumpPressed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health -= other.gameObject.GetComponent<Enemy>().GetDamage();
        }
    }

    private void ForbiddenAbility(int index)
    {
        switch (_lastIndex)
        {
            case 2:
                _attackEnable = true;
                break;
            case 3:
                _sprintEnabled = true;
                break;
            case 5:
                bulletHitBox.gameObject.GetComponent<BulletHit>().SetShootEnable(true);
                break;
            case 6:
                jumpCount = 2;
                break;
        }

        _lastIndex = index;
        switch (index)
        {
            case 2: // 攻击
                _attackEnable = false;
                break;
            case 3: // 冲刺
                _sprintEnabled = false;
                break;
            case 5:
                bulletHitBox.gameObject.GetComponent<BulletHit>().SetShootEnable(false);
                break;
            case 6:
                jumpCount = 1;
                break;
        }
    }

    private IEnumerator ChangeAbilty()
    {
        yield return new WaitForSeconds(durationTime);
        Random ran = new Random();
        int index = ran.Next(1, rangeCount);
        ForbiddenAbility(index);
    }


    private IEnumerator SprintLoad() // 冲刺的CD
    {
        yield return new WaitForSeconds(coolDown);
        _sprintEnabled = true;
    }

    private IEnumerator SprintTimeCount() // 记录冲刺时间
    {
        yield return new WaitForSeconds(sprintTime);
        _isSprint = false;
        _renderer.color = _originColor;
    }

    private IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(attackTime);
        knife.gameObject.SetActive(false);
    }
}