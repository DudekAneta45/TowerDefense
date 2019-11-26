using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    [Range(0.1f, 120f)] [SerializeField] float secondBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text spawnedEnemies;
    [SerializeField] AudioClip spawnedEnemySFX;
    int numberOfSpawnEnemy = 0;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine("SpawnsEnemies");
        spawnedEnemies.text = numberOfSpawnEnemy.ToString();
    }

    IEnumerator SpawnsEnemies()
    {
        while(true) // forever
        {
            AddScore();
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);

            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParentTransform;

            yield return new WaitForSeconds(secondBetweenSpawns);
        }
    }

    private void AddScore()
    {
        numberOfSpawnEnemy++;
        spawnedEnemies.text = numberOfSpawnEnemy.ToString();
    }
}
