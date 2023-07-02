using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public enum BossState
    {
        Move,
        BasicAttack,
        Heal,
        Zet     // ����Ʈ�� ��ų 
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

    [SerializeField]
    private float healCooldown = 5.0f;

    private bool canAttack = true;
    private bool canHeal = true;

    private void Start()
    {
        currentState = BossState.Move;
    }

    private void Update()
    {
        // �÷��̾���� �Ÿ� ���
        Vector2 playerDir = (attackPoint.position - player.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, playerDir * (-1), attackRange, LayerMask.GetMask("Player"));
        if (hit.collider != null && canAttack)
        {
            ChangeState(BossState.BasicAttack);
        }
        else
        {
            ChangeState(BossState.Move);
        }

        switch (currentState)
        {
            case BossState.Move:
                // �÷��̾ ���� ������
                if (canAttack == true)
                    Move();
                break;
            case BossState.BasicAttack:
                // ����
                Attack();
                break;
            case BossState.Heal:
                // �Ѿึ�ñ�
                break;
            case BossState.Zet:
                // skill : ����Ʈ��
                break;
        }

        if (GameManager.instance.curHp <= 1000.0f && currentState != BossState.Heal && canHeal)
        {
            Debug.Log("��ų! �Ѿึ�ñ�!");
            ChangeState(BossState.Heal);
            StartCoroutine(HealCoroutine());
        }

        if (GameManager.instance.curHp == 0)
        {
            // ���� ���� �� ������ ����
            Debug.Log("���� ����");
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

    private IEnumerator HealCoroutine()
    {
        while (currentState == BossState.Heal)
        {   
            GameManager.instance.curHp += 400;
            canHeal = false;
            GameManager.instance.curHp = Mathf.Clamp(GameManager.instance.curHp, 0, GameManager.instance.bossMaxHp);

            Debug.Log("�� ����");
            GameManager.instance.HandleHp();
            yield return new WaitForSeconds(healCooldown);
        }

        ChangeState(BossState.Move);
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    public void ChangeState(BossState newState)
    {
        currentState = newState;
    }
}
