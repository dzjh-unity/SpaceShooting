using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GmaeScript/EnemySpawn")]

public class EnemySpawn : MonoBehaviour
{
    public Transform m_enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(3,5));
            Transform enemy = Instantiate(m_enemyPrefab, transform.position, Quaternion.identity) as Transform;
            enemy.Rotate(new Vector3(0, 90, 0));
        }
    }
}
