using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthSlider;          // Unity UI�� Slider�� ����Ͽ� ü�¹ٸ� ǥ���մϴ�.
    public Transform playerHeadTransform;  // �÷��̾� �Ӹ� ��ġ�� �����ϱ� ���� Transform ������Ʈ�Դϴ�.

    [SerializeField]
    private int maxHealth = 1000;          // �ִ� ü�°��� �����մϴ�.
    [SerializeField]
    private int currentHealth;            // ���� ü�°��� �����մϴ�.

    private RectTransform healthBarRectTransform;  // ü�¹��� RectTransform ������Ʈ�� �����մϴ�.

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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // �÷��̾ ���ظ� �Ծ��� �� ü���� ���ҽ�ŵ�ϴ�.
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log("�÷��̾� ���");
        }
    }

    void UpdateHealthBar()
    {
        healthSlider.value = (float)currentHealth / (float)maxHealth;  // ü�¹��� ���� ���� ü���� �ݿ��մϴ�.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(200);
        }
    }
}
