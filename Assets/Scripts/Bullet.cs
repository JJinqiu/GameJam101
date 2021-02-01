using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float destroyDistance;

    private Rigidbody2D _rb2d;
    private Vector3 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.velocity = transform.right * speed;
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - _startPosition).sqrMagnitude;
        if (distance > destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }

        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("TwoWaysPlatform"))
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Trigger"))
        {
            TriggerItem item = other.gameObject.GetComponent<TriggerItem>();
            if (item != null)
            {
                item.ColEnter();
            }
        }
    }
}