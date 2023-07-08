using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZetSpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private GameObject alertLinePrefab;
    [SerializeField]
    private GameObject zetPrefab;
    [SerializeField]
    private float minSpawnTime = 1.0f;
    [SerializeField]
    private float maxSpawnTime = 4.0f;

    public AudioSource audioSource;
    public AudioClip zetClip;

    private void Awake()
    {
        StartCoroutine("SpawnZet");
    }

    private IEnumerator SpawnZet()
    {
        while (true)
        {
            float positionY = Random.Range(stageData.LimitMin.y, stageData.LimitMax.y);
            GameObject alertLineClone = Instantiate(alertLinePrefab, new Vector3(0, positionY, 0), Quaternion.identity);

            yield return new WaitForSeconds(1.0f);

            Destroy(alertLineClone);

            // Zet »ý¼º
            Vector3 zetPosition = new Vector3(stageData.LimitMax.x, positionY, 0);
            Instantiate(zetPrefab, zetPosition, Quaternion.identity);
            audioSource.PlayOneShot(zetClip);

            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
