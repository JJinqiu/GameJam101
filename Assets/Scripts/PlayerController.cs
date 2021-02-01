using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    private int _dir = 1;
    private bool _isDeath;

    // [SerializeField] private Collider2D circleCollider;
    // [SerializeField] private Collider2D boxCollider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform knifeHitBox;
    [SerializeField] private Transform bulletHitBox;
    [SerializeField] private Transform sight;
    [SerializeField] private Transform sprintObj;
    [SerializeField] private Transform doubleJumpObj;
    [SerializeField] private GameObject gasPrefab;
    [SerializeField] private GameObject textFloat;

    public LayerMask ground;
    public LayerMask twoWaysPlatForm;
    public LayerMask slate;
    public LayerMask commandSlate;
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
    public float layerChangeTime;
    public float invincibleTime;
    public float textFloatTime;


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
        SetText();
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
        }

        if (health <= 0)
        {
            _isDeath = true;
            _rigidbody2D.velocity = new Vector2(0, 0);
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
        if (!_isDeath)
        {
            GroundMovement();
            Jump();
            Sprint();
            SwitchAnimation();
            TwoWaysPlatFormCheck();
        }
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
        _rigidbody2D.velocity = new Vector2(horizontalMove * walkSpeed * _dir, _rigidbody2D.velocity.y);
        if (horizontalMove != 0) // 更改左右移动时角色朝向
        {
            // _animator.SetBool("idle", false);
            transform.localScale = new Vector3(horizontalMove * _dir, 1, 1);
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

        if (other.gameObject.CompareTag("Trigger"))
        {
            TriggerItem item = other.gameObject.GetComponent<TriggerItem>();
            if (item != null)
            {
                item.ColEnter(this);
            }
        }

        if (other.gameObject.CompareTag("Slate"))
        {
            this.transform.parent = other.transform;
        }

        if (other.gameObject.CompareTag("CommandSlate"))
        {
            this.transform.parent = other.transform;
            TriggerItem item = other.gameObject.GetComponent<TriggerItem>();
            if (item != null)
            {
                item.ColEnter(this);
            }
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
        if (other.gameObject.CompareTag("Slate"))
        {
            this.transform.parent = null;
        }

        if (other.gameObject.CompareTag("CommandSlate"))
        {
            this.transform.parent = null;
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
                SetText("听觉攻击lost");
                SoundManager.instance.ForbiddenAudio();
                break;
            case 2: // 攻击
                SetText("近战攻击lost");
                _attackEnable = false;
                break;
            case 3: // 冲刺
                SetText("冲刺lost");
                _sprintEnabled = false;
                break;
            case 4:
                SetText("视觉lost");
                sight.gameObject.SetActive(true);
                break;
            case 5:
                SetText("远程攻击lost");
                _shootEnable = false;
                break;
            case 6:
                SetText("二段跳lost");
                jumpCount = 1;
                break;
        }

        StartCoroutine(ChangeAbilty());
    }

    public void PowerUp1()
    {
        _attackEnable = true;
        _sprintEnabled = true;
        sight.gameObject.SetActive(false);
        rangeCount = 4;
    }

    public void PowerUp2()
    {
        _shootEnable = true;
        jumpCount = 2;
        rangeCount = 6;
    }

    private IEnumerator ChangeAbilty()
    {
        yield return new WaitForSeconds(durationTime);
        int index = Random.Range(1, rangeCount + 1);
        while (index == _lastIndex)
        {
            index = Random.Range(1, rangeCount + 1);
        }

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
        if (damage > 0 && health >= 0) // health判断死亡后不断触发 
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
        yield return new WaitForSeconds(layerChangeTime);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private IEnumerator InvincibleTime() // 无敌时间
    {
        yield return new WaitForSeconds(invincibleTime);
        _hurtEnable = true;
        _renderer.color = _originColor;
    }

    public void SetText(String value = "新青年")
    {
        textFloat.SetActive(true);
        textFloat.GetComponent<TextMesh>().text = value;
        // StartCoroutine(TextFloatTime());
    }

    private IEnumerator TextFloatTime()
    {
        yield return new WaitForSeconds(textFloatTime);
        textFloat.SetActive(false);
    }

    public void SetDirection(bool key) // 设置操纵方向，true正向
    {
        if (key)
        {
            _dir = 1;
        }
        else
        {
            _dir = -1;
        }
    }

    public void RestoreAbility(int condition) // 恢复能力 0：最初始，1：第一次提升，2：第二次提升
    {
        if (condition == 1)
        {
            PowerUp1();
        }

        if (condition == 2)
        {
            PowerUp2();
        }
    }

    public void Death()
    {
        SoundManager.instance.DeathAudio();
        Destroy(gameObject);
    }

    public void Recover(int value)
    {
        health = value;
    }
}