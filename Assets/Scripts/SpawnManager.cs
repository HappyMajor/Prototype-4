using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public int enemyCount;
    private float spawnRange = 9;
    private int waveNumber = 1;

    void Start() {
        SpawnEnemyWave(waveNumber);
    }
    
    void Update() {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0) {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    private Vector3 GenerateSpawnPosition() {
        float spawnPosX = Random.Range(0, spawnRange);
        float spawnPosZ = Random.Range(0, spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    void SpawnEnemyWave(int enemiesToSpawn) {
        SpawnPowerup();
        for(int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnPowerup() {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }
    

}
