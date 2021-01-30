using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _renderer;
    private Animator _animator;
    private bool _isGround;
    private bool _isTwoWaysPlatForm;
    private bool _jumpPressed;
    private Color _originColor;
    private int _jumpCount;
    private bool _isSprint;
    private bool _sprintEnabled;
    private int _lastIndex;
    private bool _attackEnable; // 近战攻击
    private bool _shootEnable;
    private bool _hurtEnable = true;

    [SerializeField] private Collider2D circleCollider;
    [SerializeField] private Collider2D boxCollider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform knifeHitBox;
    [SerializeField] private Transform bulletHitBox;
    [SerializeField] private Transform sight;
    [SerializeField] private Transform sprintObj;
    [SerializeField] private Transform doubleJumpObj;
    [SerializeField] private GameObject gasPrefab;

    public LayerMask ground;
    public LayerMask twoWaysPlatForm;
    public Transform knife;

    public float walkSpeed;
    public float jumpForce1;
    public float jumpForce2;
    public float coolDown;
    public float attackTime;
    public int jumpCount;
    public int health;
    public int rangeCount;

    public float sprintTime;
    public float sprintSpeed;
    public float durationTime;
    public float LayerChangeTime;
    public float invincibleTime;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _originColor = _renderer.color;
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
            SoundManager.instance.SprintAudio();
            _sprintEnabled = false;
            _isSprint = true;
            _renderer.enabled = false;
            sprintObj.gameObject.SetActive(true);
            StartCoroutine(SprintLoad());
            StartCoroutine(SprintTimeCount());
        }

        if (Input.GetMouseButtonDown(0) && _attackEnable)
        {
            SoundManager.instance.KnifeAudio();
            _attackEnable = false;
            _animator.SetTrigger("knife_attack");
            StartCoroutine(AttackTime());
            // Attack();
        }

        if (health <= 0)
        {
            SoundManager.instance.DeathAudio();
            _animator.SetTrigger("die");
        }

        bulletHitBox.gameObject.GetComponent<BulletHit>().SetShootEnable(_shootEnable);
    }

    public void Shoot()
    {
        bulletHitBox.gameObject.GetComponent<BulletHit>().Shoot();
    }

    private void FixedUpdate()
    {
        _isTwoWaysPlatForm = Physics2D.OverlapCircle(groundCheck.position, 0.1f, twoWaysPlatForm);
        _isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground) || _isTwoWaysPlatForm;
        GroundMovement();
        Jump();
        Sprint();
        SwitchAnimation();
        TwoWaysPlatFormCheck();
    }

    public void Attack()
    {
        knife.gameObject.SetActive(true);
    }

    public void CloseAttack()
    {
        knife.gameObject.SetActive(false);
    }

    private void Sprint() // 冲刺
    {
        if (_isSprint)
        {
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

    private void SwitchAnimation()
    {
        _animator.SetFloat("running", Mathf.Abs(_rigidbody2D.velocity.x));

        if (_isGround)
        {
            _animator.SetBool("falling", false);
        }
        else if (!_isGround && _rigidbody2D.velocity.y > 0)
        {
            _animator.SetBool("jumping", true);
            _animator.SetBool("falling", false);
        }
        else if (_rigidbody2D.velocity.y < 0)
        {
            _renderer.enabled = true;
            doubleJumpObj.gameObject.SetActive(false);
            _animator.SetBool("falling", true);
            _animator.SetBool("jumping", false);
        }
    }

    public void ChangeHurting()
    {
        _renderer.color = Color.green;
        StartCoroutine(InvincibleTime());
    }

    private void GroundMovement() // 角色移动
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        _rigidbody2D.velocity = new Vector2(horizontalMove * walkSpeed, _rigidbody2D.velocity.y);
        if (horizontalMove != 0) // 更改左右移动时角色朝向
        {
            // _animator.SetBool("idle", false);
            transform.localScale = new Vector3(horizontalMove, 1, 1);
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
            SoundManager.instance.JumpAudio();
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce1);
            --_jumpCount;
            _jumpPressed = false;
        }
        else if (_jumpPressed && _jumpCount > 0)
        {
            Instantiate(gasPrefab, groundCheck.position, quaternion.Euler(0));
            _renderer.enabled = false;
            doubleJumpObj.gameObject.SetActive(true);
            SoundManager.instance.JumpAudio();
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce2);
            --_jumpCount;
            _jumpPressed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && _hurtEnable)
        {
            Hurt(other.gameObject.GetComponent<Enemy>().GetDamage());
        }

        if (other.gameObject.CompareTag("Depot1"))
        {
            PowerUp1();
        }

        if (other.gameObject.CompareTag("Depot2"))
        {
            PowerUp2();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            TriggerItem item = other.gameObject.GetComponent<TriggerItem>();
            if (item != null)
            {
                item.ColEnter(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            TriggerItem item = other.gameObject.GetComponent<TriggerItem>();
            if (item != null)
            {
                item.ColExit(this);
            }
        }
    }
    
    private void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void ForbiddenAbility(int index)
    {
        if (_lastIndex == 1 || index == 1)
        {
            SoundManager.instance.ResetAudio();
        }

        switch (_lastIndex)
        {
            case 1:
                SoundManager.instance.ResetAudio();
                break;
            case 2:
                _attackEnable = true;
                break;
            case 3:
                _sprintEnabled = true;
                break;
            case 4:
                sight.gameObject.SetActive(false);
                break;
            case 5:
                _shootEnable = true;
                break;
            case 6:
                jumpCount = 2;
                break;
        }

        _lastIndex = index;
        switch (index)
        {
            case 1:
                SoundManager.instance.ForbiddenAudio();
                break;
            case 2: // 攻击
                _attackEnable = false;
                break;
            case 3: // 冲刺
                _sprintEnabled = false;
                break;
            case 4:
                sight.gameObject.SetActive(true);
                break;
            case 5:
                _shootEnable = false;
                break;
            case 6:
                jumpCount = 1;
                break;
        }

        StartCoroutine(ChangeAbilty());
    }

    private void PowerUp1()
    {
        _attackEnable = true;
        _sprintEnabled = true;
        sight.gameObject.SetActive(false);
        rangeCount = 4;
    }

    private void PowerUp2()
    {
        _shootEnable = true;
        jumpCount = 2;
        rangeCount = 6;
    }

    private IEnumerator ChangeAbilty()
    {
        yield return new WaitForSeconds(durationTime);
        int index = Random.Range(1, rangeCount + 1);
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
        _renderer.enabled = true;
        sprintObj.gameObject.SetActive(false);
    }

    private IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(attackTime);
        _attackEnable = true;
        knife.gameObject.SetActive(false);
    }

    public void Hurt(int damage)
    {
        if (damage > 0)
        {
            health -= damage;
            _hurtEnable = false;
            _animator.SetTrigger("hurting");
            // _rigidbody2D.velocity = new Vector2(-20, _rigidbody2D.velocity.y);
            SoundManager.instance.HurtAudio();
        }
    }

    void TwoWaysPlatFormCheck()
    {
        if (_isTwoWaysPlatForm && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            gameObject.layer = LayerMask.NameToLayer("TwoWaysPlatForm");
            StartCoroutine(LayChange());
        }
    }

    private IEnumerator LayChange()
    {
        yield return new WaitForSeconds(LayerChangeTime);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private IEnumerator InvincibleTime() // 无敌时间
    {
        yield return new WaitForSeconds(invincibleTime);
        _hurtEnable = true;
        _renderer.color = _originColor;
    }
}