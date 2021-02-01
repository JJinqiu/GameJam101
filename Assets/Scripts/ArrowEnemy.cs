using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnemy : Enemy
{
    public float radius;
    public float coolDownTime;
    public GameObject bulletPrefab;

    private bool _attackEnable;
    
    // Start is called before the first frame update
    void Start()
    {
            base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        // bool flag = false;
        if (_playerTransform)
        {
            float distance = (transform.position - _playerTransform.position).sqrMagnitude;
            if (distance < radius)
            {
                SoundManager.instance.ArrowFindAudio();
                if (transform.position.x > _playerTransform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (!_attackEnable)
                {
                    _attackEnable = true;
                    StartCoroutine(CoolDownTime());
                }
            }
        }
    }
    

    private void Attack()
    {
        SoundManager.instance.ArrowAttackAudio();
        Vector3 _bulletDirection = (_playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(_bulletDirection.y, _bulletDirection.x) * Mathf.Rad2Deg;
        Vector3 eulerAngles = new Vector3(0, 0, angle);
        Instantiate(bulletPrefab, transform.position + new Vector3(0, -0.6f, 0), Quaternion.Euler(eulerAngles));
    }

    private IEnumerator CoolDownTime()
    {
        yield return new WaitForSeconds(coolDownTime);
        _animator.SetTrigger("attack");
        _attackEnable = false;
    }
}
