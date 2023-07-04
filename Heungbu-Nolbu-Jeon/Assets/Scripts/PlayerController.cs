using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    // public GameObject attackEffectPrefab; // ���� ����Ʈ ������
    [SerializeField]
    public Transform attackPoint; // ���� ����
    [SerializeField]
    public float attackRange = 2f; // ���� ����
    public int damageAmount = 10; // ���ݷ�
    public float attackCooldown = 1f; // ���� ��ٿ� �ð�

    private bool canAttack = true; // ���� ������ �������� ����

    public Slider healthSlider;          // Unity UI�� Slider�� ����Ͽ� ü�¹ٸ� ǥ���մϴ�.
    public Transform playerHeadTransform;  // �÷��̾� �Ӹ� ��ġ�� �����ϱ� ���� Transform ������Ʈ�Դϴ�.

    [SerializeField]
    private int maxHealth = 1000;          // �ִ� ü�°��� �����մϴ�.
    [SerializeField]
    private int currentHealth;            // ���� ü�°��� �����մϴ�.

    private RectTransform healthBarRectTransform;  // ü�¹��� RectTransform ������Ʈ�� �����մϴ�.

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
        currentHealth = maxHealth;  // ���� ���� �� ���� ü���� �ִ� ü������ �����մϴ�.
        UpdateHealthBar();

        healthBarRectTransform = healthSlider.GetComponent<RectTransform>();  // ü�¹��� RectTransform ������Ʈ�� �����ɴϴ�.
    }

    void LateUpdate()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(playerHeadTransform.position);  // �÷��̾� �Ӹ� ��ġ�� ��ũ�� ��ǥ�� ��ȯ�մϴ�.
        healthBarRectTransform.position = screenPosition;  // ü�¹��� ��ġ�� �÷��̾� �Ӹ� ��ġ�� �����մϴ�.
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
        // ���� ��ٿ� ����
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
        currentHealth -= damage;  // �÷��̾ ���ظ� �Ծ��� �� ü���� ���ҽ�ŵ�ϴ�.
        StartCoroutine(GetDamage());
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log("�÷��̾� ���");
            GameManager.instance.gameState = "die";
        }
    }

    void UpdateHealthBar()
    {
        healthSlider.value = (float)currentHealth / (float)maxHealth;  // ü�¹��� ���� ���� ü���� �ݿ��մϴ�.
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
            Debug.Log("��Ʈ�� ����");
            TakeDamage(100);
        }
    }
}
