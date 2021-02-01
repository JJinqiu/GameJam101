using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHit : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
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