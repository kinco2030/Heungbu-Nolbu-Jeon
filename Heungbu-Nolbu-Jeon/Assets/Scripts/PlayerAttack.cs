using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // public GameObject attackEffectPrefab; // ���� ����Ʈ ������
    public Transform attackPoint; // ���� ����
    public float attackRange = 2f; // ���� ����
    public int damageAmount = 10; // ���ݷ�
    public float attackCooldown = 1f; // ���� ��ٿ� �ð�

    private bool canAttack = true; // ���� ������ �������� ����

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // ���� ��ٿ� ����
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
