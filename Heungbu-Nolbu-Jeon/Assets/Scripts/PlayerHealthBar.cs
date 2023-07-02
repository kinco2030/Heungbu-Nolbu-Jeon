using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider healthSlider;          // Unity UI의 Slider를 사용하여 체력바를 표시합니다.
    public Transform playerHeadTransform;  // 플레이어 머리 위치를 참조하기 위한 Transform 컴포넌트입니다.

    [SerializeField]
    private int maxHealth = 1000;          // 최대 체력값을 설정합니다.
    [SerializeField]
    private int currentHealth;            // 현재 체력값을 저장합니다.

    private RectTransform healthBarRectTransform;  // 체력바의 RectTransform 컴포넌트를 참조합니다.

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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // 플레이어가 피해를 입었을 때 체력을 감소시킵니다.
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log("플레이어 사망");
        }
    }

    void UpdateHealthBar()
    {
        healthSlider.value = (float)currentHealth / (float)maxHealth;  // 체력바의 값에 현재 체력을 반영합니다.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(200);
        }
    }
}
