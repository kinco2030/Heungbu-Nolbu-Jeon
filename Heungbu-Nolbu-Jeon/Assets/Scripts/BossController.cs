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

    public Transform player;
    public float movementSpeed = 3.0f;
    [SerializeField]
    private float attackRange = 5.0f;
    [SerializeField]
    private float attackCooldown = 5.0f;
    private bool canAttack = true;


    private void Start()
    {
        currentState = BossState.Move;
    }

    private void Update()
    {
        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && canAttack)
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
                // Animation ����
                break;
            case BossState.Move:
                // �÷��̾ ���� ������
                Move();
                break;
            case BossState.BasicAttack:
                // ����
                Attack();
                break;
            case BossState.Skill:
                // skill : ����Ʈ��
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
        Debug.Log("�÷��̾� ����!!!!");
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
