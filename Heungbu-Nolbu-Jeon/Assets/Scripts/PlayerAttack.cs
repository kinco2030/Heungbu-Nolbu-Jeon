using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // public GameObject attackEffectPrefab; // 공격 이펙트 프리팹
    public Transform attackPoint; // 공격 지점
    public float attackRange = 2f; // 공격 범위
    public int damageAmount = 10; // 공격력
    public float attackCooldown = 1f; // 공격 쿨다운 시간

    private bool canAttack = true; // 공격 가능한 상태인지 여부

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // 공격 쿨다운 시작
        StartCoroutine(AttackCooldown());

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Enemy"));
        foreach(Collider2D enemy in hit)
        {
            if (enemy != null)
            {
                if (enemy.gameObject.CompareTag("Enemy"))
                {
                    enemy.gameObject.SetActive(false);
                }
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
