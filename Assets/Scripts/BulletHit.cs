using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Camera cam;
    public float coolDown;
    public bool shootEnable;
    
    private Vector3 _mousePosition;
    private Vector2 _bulletDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        _mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        _bulletDirection = (_mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(_bulletDirection.y, _bulletDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        if (Input.GetMouseButtonDown(1) && shootEnable) // 鼠标右键点击
        {
            shootEnable = false;
            Shoot();
            StartCoroutine(JudgeCoolDown());
        }
    }

    private IEnumerator JudgeCoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        shootEnable = true;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles));
    }

    public void SetShootEnable(bool value)
    {
        shootEnable = value;
    }
}
