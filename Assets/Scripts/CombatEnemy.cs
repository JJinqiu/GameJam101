using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CombatEnemy : Enemy
{
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    // [SerializeField] private Collider2D leftCollider2D;
    // [SerializeField] private Collider2D rightCollider2D;
    [SerializeField] private Collider2D hitBox;
    
    public float walkSpeed;
    public float sprintSpeed;

    private float _xRightPos;
    private float _xLeftPos;

    private bool _isLeft;
    private bool flag;
    private bool _attackEnable = true;

    public float coolDown;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _xLeftPos = leftPoint.position.x;
        _xRightPos = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        base.Update();
        // bool flag = false;
        // if (_playerTransform)
        // {
        //     if (_playerTransform.position.x < _xRightPos && _playerTransform.position.x > _xLeftPos &&
        //         _playerTransform.position.y < transform.position.y + 1 &&
        //         _playerTransform.position.y > transform.position.y - 1)
        //     {
        //         flag = true;
        //     }
        // }

        // if (!flag)
        // {
        Movement();
        // }
    }

    private void Movement()
    {
        if (_isLeft)
        {
            rb2d.velocity = new Vector2(-walkSpeed, 0);
            if (transform.position.x < _xLeftPos)
            {
                rb2d.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                _isLeft = false;
            }
        }
        else
        {
            rb2d.velocity = new Vector2(walkSpeed, 0);
            if (transform.position.x > _xRightPos)
            {
                rb2d.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                _isLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && _attackEnable)
        {
            SoundManager.instance.CombatAttackAudio();
            _animator.SetTrigger("attack");
            _attackEnable = false;
            StartCoroutine(AttackCoolDown());
        }
    }

    public void OpenCollider()
    {
        hitBox.enabled = true;
    }

    public void CloseCollider()
    {
        hitBox.enabled = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        _attackEnable = true;
    }
}