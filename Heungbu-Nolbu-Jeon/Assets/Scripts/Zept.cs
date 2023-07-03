using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zept : MonoBehaviour
{
    public float minSpawnInterval = 1f; // ������ �����Ǵ� �ּ� ����
    public float maxSpawnInterval = 3f; // ������ �����Ǵ� �ִ� ����
    public GameObject rocketPrefab; // ���� ������
    public float minX = -5f; // ������ �����Ǵ� �ּ� X ��ǥ
    public float maxX = 5f; // ������ �����Ǵ� �ִ� X ��ǥ
    public float spawnHeight = 10f; // ������ �����Ǵ� ����

    private float nextSpawnTime; // ���� ���� ���� �ð�

    private void Start()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnRocket();
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    private void SpawnRocket()
    {
        // ������ X ��ǥ�� ���� ����
        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(spawnX, spawnHeight, 0f);

        // ���� �������� �ν��Ͻ�ȭ�Ͽ� ����
        GameObject rocket = Instantiate(rocketPrefab, spawnPosition, Quaternion.identity);

        // ������ �������� ����� ���� �������� ����
        Rigidbody2D rocketRigidbody = rocket.GetComponent<Rigidbody2D>();
        float randomForce = Random.Range(1f, 5f);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 force = randomDirection * randomForce;
        rocketRigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
