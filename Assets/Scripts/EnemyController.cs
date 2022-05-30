using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 50f;
    public float damage = 10f;
    public float attackRange=0.7f;
    public float enemyTrigger = 1.7f;
    private float speed = 5f;
    public Animator animator;
    public Transform playerLocation;
    public LayerMask playerLayer;
    public float moveDistance = 0.5f;
    public float heightDistance = 0.5f;
    Rigidbody2D rigidbody2d;
    bool playerFollow = false;
    void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        rigidbody2d=GetComponent<Rigidbody2D>();
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyTrigger();
        if(playerFollow)
        {
            PlayerFollow();
        }
        
    }
    void EnemyTrigger()
    {
        var detection = Physics2D.OverlapCircle(transform.position, enemyTrigger, playerLayer);
        if(detection!=null)
        {
            playerFollow = true;
        }
    }
    bool attackAble = true;
    float attackRate = 1f;
    float currentTime = 0f;
    void PlayerFollow()
    {
        float playerDistanceX = Vector3.Distance(new Vector3(transform.position.x,0,0) ,new Vector3(playerLocation.position.x,0,0));
        float playerDistanceY = Vector3.Distance(new Vector3(0, transform.position.y, 0), new Vector3(0, playerLocation.position.y, 0));
        if(playerDistanceX > moveDistance)
        {
            Vector2 target=new Vector2(playerLocation.position.x, transform.position.y);
            Vector2 move =Vector2.MoveTowards(rigidbody2d.position, target,speed*Time.deltaTime);
            rigidbody2d.MovePosition(new Vector2(move.x,transform.position.y));
            Debug.Log(playerDistanceX);
        }
        Debug.Log(moveDistance);
        if (playerDistanceX < moveDistance && playerDistanceY < heightDistance) 
        {
            EnemyAttack();
        }
    }
    void EnemyAttack()
    {
            if (Time.time > currentTime)
            {
                currentTime = Time.time + 3.5f / attackRate;
                animator.SetTrigger("attackT");
            }
    }
    public void TakeDamage(float value)
    {
        animator.SetTrigger("takeDamageT");
        health-=value;
        if(health<=0)
        {
            Death();
        }
    }
    void Death()
    {
        animator.SetTrigger("deathT");
        Destroy(gameObject);
    }
    public void Attack()
    {
        var detection = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (detection != null)
        {
            detection.gameObject.GetComponent<PlayerControl>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
