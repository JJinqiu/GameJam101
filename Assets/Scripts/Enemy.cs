using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    protected Transform _playerTransform;
    protected Rigidbody2D rb2d;
    protected Animator _animator;

    // Start is called before the first frame update
    protected void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!_playerTransform)
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        SoundManager.instance.CombatHurtAudio();
        health -= damage;
    }

    public int GetDamage()
    {
        return damage;
    }
}