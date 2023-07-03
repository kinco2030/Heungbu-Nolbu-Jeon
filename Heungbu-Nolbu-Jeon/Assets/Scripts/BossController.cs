using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public enum BossState
    {
        Move,
        BasicAttack,
        Heal
    }

    private BossState currentState;

    Animator anim;
    SpriteRenderer sprite;

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

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 플레이어와의 거리 계산
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
                // 플레이어를 따라 움직임
                if (canAttack == true)
                    Move();
                break;
            case BossState.BasicAttack:
                // 공격
                Attack();
                break;
            case BossState.Heal:
                // 한약마시기
                break;
        }

        if (GameManager.instance.curHp <= 1000.0f && currentState != BossState.Heal && canHeal)
        {
            Debug.Log("스킬! 한약마시기!");
            anim.SetBool("isHeal", true);
            ChangeState(BossState.Heal);
            StartCoroutine(HealCoroutine());
        }

        if (GameManager.instance.curHp == 0)
        {
            // 보스 죽을 때 실행할 로직
            Debug.Log("보스 죽음");
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
        bulletRigid.AddForce(playerDirection * 40, ForceMode2D.Impulse);
    }

    private IEnumerator HealCoroutine()
    {
        while (currentState == BossState.Heal)
        {
            GameManager.instance.curHp += 400;
            canHeal = false;
            GameManager.instance.curHp = Mathf.Clamp(GameManager.instance.curHp, 0, GameManager.instance.bossMaxHp);

            Debug.Log("힐 끝남");
            GameManager.instance.HandleHp();
            yield return new WaitForSeconds(healCooldown);
            anim.SetBool("isHeal", false);
        }

        ChangeState(BossState.Move);
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
        anim.SetBool("isAttack", false);
    }

    public IEnumerator GetDamage()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(1.0f);
        sprite.color = Color.white;
    }

    public void ChangeState(BossState newState)
    {
        currentState = newState;
    }
}
