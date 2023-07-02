using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private enum BossState
    {
        Idle,
        Move,
        BasicAttack,
        Skill
    }

    private BossState currentState;

    public GameObject bulletObj;

    public Transform player;
    public float movementSpeed = 3.0f;

    [SerializeField]
    private float attackRange = 10.0f;
    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private Transform attackPoint;

    private bool canAttack = true;


    private void Start()
    {
        currentState = BossState.Move;
    }

    private void Update()
    {
        // 플레이어와의 거리 계산
        Vector2 playerDir = (attackPoint.position - player.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, playerDir * (-1), attackRange, LayerMask.GetMask("Player"));
        if (hit.collider != null && canAttack)
        {
            ChangeState(2);
        }
        else
        {
            ChangeState(1);
        }

        switch (currentState)
        {
            case BossState.Idle:
                // Animation 실행
                break;
            case BossState.Move:
                // 플레이어를 따라 움직임
                if (canAttack == true)
                    Move();
                break;
            case BossState.BasicAttack:
                // 공격
                Attack();
                break;
            case BossState.Skill:
                // skill : 제ㅂ트기
                break;
        }
    }

    private void Move()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 movement = direction * movementSpeed * Time.deltaTime;
        transform.position += movement;
    }

    private void Attack()
    {
        StartCoroutine(AttackCooldown());
        
        Vector2 playerDirection = (player.position - attackPoint.position).normalized;
        GameObject bullet = Instantiate(bulletObj, attackPoint.position, attackPoint.rotation);
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bulletRigid.AddForce(playerDirection * 20, ForceMode2D.Impulse);
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    public void ChangeState(int stateNum)
    {
        if (stateNum == 0)
            currentState = BossState.Idle;
        else if (stateNum == 1)
            currentState = BossState.Move;
        else if (stateNum == 2)
            currentState = BossState.BasicAttack;
        else if (stateNum == 3)
            currentState = BossState.Skill;
    }
}
