using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZet : MonoBehaviour
{
    public float minY;
    public float maxY;

    [SerializeField]
    private GameObject alertLine;
    [SerializeField]
    private GameObject zetObj;

    private void Awake()
    {
        StartCoroutine("SpawnZet");
    }

    private IEnumerator SpawnZet()
    {
        while (true)
        {
            float positionY = Random.Range(minY, maxY);
            float positionX = Random.Range(0, 2);

            Vector3 newPos = new Vector3(transform.position.x, positionY, transform.position.z);
            GameObject alertClone = Instantiate(alertLine, newPos, Quaternion.identity);

            yield return new WaitForSeconds(1.0f);

            Destroy(alertClone);
            if (positionX == 1)
            {

            }
            GameObject zetClone = Instantiate(zetObj, newPos, Quaternion.identity);
        }
    }
}
