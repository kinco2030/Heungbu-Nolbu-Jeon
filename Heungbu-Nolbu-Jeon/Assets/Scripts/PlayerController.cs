using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    // public GameObject attackEffectPrefab; // 공격 이펙트 프리팹
    [SerializeField]
    public Transform attackPoint; // 공격 지점
    [SerializeField]
    public float attackRange = 2f; // 공격 범위
    public int damageAmount = 10; // 공격력
    public float attackCooldown = 1f; // 공격 쿨다운 시간

    private bool canAttack = true; // 공격 가능한 상태인지 여부

    public Slider healthSlider;          // Unity UI의 Slider를 사용하여 체력바를 표시합니다.
    public Transform playerHeadTransform;  // 플레이어 머리 위치를 참조하기 위한 Transform 컴포넌트입니다.

    [SerializeField]
    private int maxHealth = 1000;          // 최대 체력값을 설정합니다.
    [SerializeField]
    private int currentHealth;            // 현재 체력값을 저장합니다.

    private RectTransform healthBarRectTransform;  // 체력바의 RectTransform 컴포넌트를 참조합니다.

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = maxHealth;  // 게임 시작 시 현재 체력을 최대 체력으로 설정합니다.
        UpdateHealthBar();

        healthBarRectTransform = healthSlider.GetComponent<RectTransform>();  // 체력바의 RectTransform 컴포넌트를 가져옵니다.
    }

    void LateUpdate()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(playerHeadTransform.position);  // 플레이어 머리 위치를 스크린 좌표로 변환합니다.
        healthBarRectTransform.position = screenPosition;  // 체력바의 위치를 플레이어 머리 위치로 설정합니다.
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector2(h, v) * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            Attack();
        }

        if (h != 0 || v != 0)
            anim.SetBool("isMove", true);
        else
            anim.SetBool("isMove", false);

        if (h < 0)
            sprite.flipX = false;
        else if (h > 0)
            sprite.flipX = true;
    }

    private void Attack()
    {
        // 공격 쿨다운 시작
        StartCoroutine(AttackCooldown());

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Boss"));
        foreach (Collider2D enemy in hit)
        {
            if (enemy != null)
            {
                if (enemy.gameObject.CompareTag("Boss"))
                {
                    GameManager.instance.BossGetDamage();
                }
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
        anim.SetBool("isAttack", false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // 플레이어가 피해를 입었을 때 체력을 감소시킵니다.
        StartCoroutine(GetDamage());
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log("플레이어 사망");
            GameManager.instance.gameState = "die";
        }
    }

    void UpdateHealthBar()
    {
        healthSlider.value = (float)currentHealth / (float)maxHealth;  // 체력바의 값에 현재 체력을 반영합니다.
    }

    private IEnumerator GetDamage()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
        //StopCoroutine(GetDamage());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(50);
        }

        if (collision.gameObject.CompareTag("Zet"))
        {
            Debug.Log("젭트기 맞음");
            TakeDamage(100);
        }
    }
}
