using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerControl : MonoBehaviour
{

    public Animator animator;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float damage = 10f;
    public float health = 100f;
    public float attackRange = 0.1f;
    public float offsetX = 0.5f;
    public LayerMask enemyLayer;
    Rigidbody2D _rigidbody;
    public Transform attackPosition;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        enemyLayer = LayerMask.GetMask("Enemy");
    }
    void Update()
    {
        positionC = backGround.position.x;
        PlayerMove();
        PlayerJump();
        Attack();
    }
    float positionC;
    void PlayerMove()
    {
        float xAxis = Input.GetAxis("Vertical");
        float yAxis = Input.GetAxis("Horizontal");
        if (yAxis != 0)
        {
            transform.position+=(new Vector3(yAxis,0)*moveSpeed*Time.deltaTime);
            transform.rotation = yAxis < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            animator.SetBool("RunB", true);

        }
        else
        {
            animator.SetBool("RunB", false);
        }
    }
    bool isDoubleJump = true;
    bool isGround = true;
    bool isJump = false;
    void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGround==false&&isDoubleJump||isGround)
            {
                _rigidbody.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);
                isDoubleJump = isGround;
                animator.SetTrigger("JumpStartT");
                isJump = true;
            }
            isGround = false;
        }
    }
    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("AttackT");
        }
    }
    public void PlayerAttack()
    {
        Collider2D [] enemys = Physics2D.OverlapCircleAll(new Vector2(attackPosition.position.x,attackPosition.position.y), attackRange,enemyLayer);
        foreach(var enemy in enemys)
        {
            enemy.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }
    public void TakeDamage(float value)
    {
        animator.SetTrigger("TakeDamageT");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Game");
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
    void PlayerDeath()
    {
        Debug.Log("Player death");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isDoubleJump = true;
            isGround = true;
            if(isJump)
            {
                animator.SetTrigger("JumpFinishT");
            }
        }
    }
}
