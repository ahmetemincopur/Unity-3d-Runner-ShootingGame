using UnityEngine;
using System.Collections;

public class ZombieSpawn : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f; // Zombilerin spawn olma aralýðý

    void Start()
    {
        StartCoroutine(SpawnZombiesRoutine());
    }

    private IEnumerator SpawnZombiesRoutine()
    {
        while (true)
        {
            SpawnZombie();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnZombie()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
