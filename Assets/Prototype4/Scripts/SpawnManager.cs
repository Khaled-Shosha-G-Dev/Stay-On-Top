using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]private GameObject[] enemyPrefab;
    [SerializeField]private GameObject[] powerUpPrefab;
    private float spawnRange = 10;
    private int enemyCounter;
    private int enemyNumberWave = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enemyNumberWave);
        SpawnPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCounter = FindObjectsOfType<Enemy>().Length;
        if (enemyCounter == 0)
        {
            enemyNumberWave++;
            SpawnEnemyWave(enemyNumberWave);
            SpawnPowerUp();
        }
    }
    void SpawnEnemyWave(int spawnNum)
    {
        for(int i=0;i<spawnNum;i++)
        {
            int index=Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[index],GeneratePosition(),enemyPrefab[index].transform.rotation);
        }
    }
    private Vector3 GeneratePosition()
    {
        float xSpawnPos = Random.Range(-spawnRange, spawnRange);
        float zSpawnPos = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(xSpawnPos, 0, zSpawnPos);
        return randomPos;
    }
    private void SpawnPowerUp()
    {
        int index=Random.Range(0,powerUpPrefab.Length);
        Instantiate(powerUpPrefab[index],GeneratePosition(),powerUpPrefab[index].transform.rotation); 
    }
}
