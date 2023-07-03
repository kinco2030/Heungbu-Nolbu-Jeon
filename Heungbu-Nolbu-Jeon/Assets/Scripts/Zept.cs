using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zept : MonoBehaviour
{
    public float minSpawnInterval = 1f; // 로켓이 생성되는 최소 간격
    public float maxSpawnInterval = 3f; // 로켓이 생성되는 최대 간격
    public GameObject rocketPrefab; // 로켓 프리팹
    public float minX = -5f; // 로켓이 생성되는 최소 X 좌표
    public float maxX = 5f; // 로켓이 생성되는 최대 X 좌표
    public float spawnHeight = 10f; // 로켓이 생성되는 높이

    private float nextSpawnTime; // 다음 로켓 생성 시간

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
        // 랜덤한 X 좌표로 로켓 생성
        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(spawnX, spawnHeight, 0f);

        // 로켓 프리팹을 인스턴스화하여 생성
        GameObject rocket = Instantiate(rocketPrefab, spawnPosition, Quaternion.identity);

        // 로켓이 떨어지는 방향과 힘을 랜덤으로 설정
        Rigidbody2D rocketRigidbody = rocket.GetComponent<Rigidbody2D>();
        float randomForce = Random.Range(1f, 5f);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 force = randomDirection * randomForce;
        rocketRigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
