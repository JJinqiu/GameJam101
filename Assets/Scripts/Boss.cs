using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public Animator amim;
    public GameObject bulletPrefab;
    public Transform recoverplace;
    int status = 0;
    int c = 0;
    enum BossActionType { acttack,recover,die,dead,wait};
    BossActionType BossAction= BossActionType.wait;
    Vector3 RiseTarget;
    Vector3 PlayerPosition;
    
    
    private void OnTriggerEnter2D(Collider2D other)//撞到主角
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().Hurt(damage/2);
        }
    }

    protected new void Update()
    {
        if (BossAction == BossActionType.wait)
        {
            if ((transform.position - GameObject.FindWithTag("Player").transform.position).sqrMagnitude < 4)
            {
                Debug.Log((transform.position - GameObject.FindWithTag("Player").transform.position).sqrMagnitude);
                BossAction = BossActionType.acttack;
            }
        }
        if (health <= 0&& BossAction != BossActionType.die)
        {
            c = 0;
            status = 0;
            BossAction = BossActionType.die;
            amim.SetBool("die", true);
        }
        if (health > 0&&health <= 24&& BossAction != BossActionType.recover)
        {
            c = 0;
            status =0;
            BossAction = BossActionType.recover;
            damage = 0;//取消伤害
            GetComponent<Rigidbody2D>().gravityScale = 0;//取消重力
            GetComponent<Collider2D>().isTrigger = true;//取消碰撞体
        }
    }

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if(BossAction == BossActionType.acttack)
        {
            //BOSS调整朝向
            if (transform.position.x - GameObject.FindWithTag("Player").transform.position.x < 0)//玩家在右边
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            switch (status)
            {
                case 0://过2s开始跳
                    c++;
                    if (c >= 100)
                    {
                        c = 0;
                        status = 1;
                        amim.SetInteger("status", 1);
                    }
                    break;
                case 1://下蹲动画
                    c++;
                    if (c >= 30)
                    {
                        c = 0;
                        status = 2;
                        GetComponent<Rigidbody2D>().gravityScale = 0;//取消重力
                        GetComponent<Collider2D>().isTrigger = true;//取消碰撞体
                        RiseTarget = transform.position + new Vector3(0, 3, 0);//获取BOSS当前位置上方
                    }
                    break;
                case 2://爬升
                    c++;
                    transform.position = Vector2.MoveTowards(transform.position, RiseTarget, 0.1f);
                    if (c >= 50)
                    {
                        c = 0;
                        status = 3;
                        PlayerPosition = GameObject.FindWithTag("Player").transform.position;//锁定玩家
                    }
                    break;
                case 3://俯冲
                    c++;
                    transform.position = Vector2.MoveTowards(transform.position, PlayerPosition, 0.3f);
                    if (c >= 30)
                    {
                        c = 0;
                        status = 4;
                        GetComponent<Rigidbody2D>().gravityScale = 10;//重力
                        GetComponent<Collider2D>().isTrigger = false;//碰撞体
                        amim.SetInteger("status", 2);//开始落地动画
                    }
                    break;
                case 4://落地动画
                    c++;
                    if (c >= 30)
                    {
                        c = 0;
                        status = 5;
                        amim.SetInteger("status", 3);//开始站立动画
                    }
                    break;
                case 5://一会后开始射击动画
                    c++;
                    if (c >= 50)
                    {
                        c = 0;
                        status = 6;
                        amim.SetInteger("status", 4);//开始射击动画
                    }
                    break;
                case 6://射击动画
                    c++;
                    if (c >= 30)
                    {
                        c = 0;
                        status = 0;
                        amim.SetInteger("status", 5);//开始站立动画
                        Vector3 _bulletDirection = (_playerTransform.position - transform.position).normalized;
                        float angle = Mathf.Atan2(_bulletDirection.y, _bulletDirection.x) * Mathf.Rad2Deg;
                        Vector3 eulerAngles = new Vector3(0, 0, angle);
                        Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.2f, 0), Quaternion.Euler(eulerAngles));
                    }
                    break;
                default: break;
            }
        }
        else if(BossAction == BossActionType.recover)
        {
            switch (status)
            {
                case 0:
                    transform.position = Vector2.MoveTowards(transform.position, recoverplace.position, 0.3f);//朝回血点移动
                    if ((transform.position- recoverplace.position).sqrMagnitude<0.1)
                    {
                        status = 1;
                        amim.SetBool("startrecover", true);//开始回血动画
                    }
                    break;
                case 1://回血动画
                    c++;
                    if (c >= 50)//1s回一滴
                    {
                        health+=1;
                        c = 0;
                        if (health >= 72)
                        {
                            health = 72;
                            amim.SetBool("startrecover", false);//关闭回血动画
                            GetComponent<Rigidbody2D>().gravityScale = 10;//重力
                            GetComponent<Collider2D>().isTrigger = false;//碰撞体
                            damage = 2;//恢复伤害
                            BossAction = BossActionType.acttack;
                            status = 0;
                        }
                    }
                    break;
                default: break;
            }
        }
        else if (BossAction == BossActionType.die)
        {
            c++;
            if (c >= 100)
            {
                Destroy(gameObject);
            }
        }
    }
}
