using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerParent;
    public Transform enemyParent;
    
    public Transform ringCenter;
    public float spawnDistance = 3f;
    public float lateralSpacing = 1.5f;
    void Start()
    {
        
        
        
        
        LevelManager.Instance.GenerateLevelStats(LevelManager.Instance.currentLevel);
        int enemyCount = LevelManager.Instance.extraEnemies;
        string mode = PlayerPrefs.GetString("GameMode", "1v1");

        switch (mode)
        {
            case "1v1":
                SpawnPlayers(1);
                SpawnEnemies(enemyCount);
                break;
            case "1v2":
                SpawnPlayers(1);
                SpawnEnemies(2);
                break;
            case "2v2":
                SpawnPlayers(2);
                SpawnEnemies(2);
                break;
        }
    }
    private List<Transform> spawnedPlayers = new List<Transform>();
    public void SpawnPlayers(int count)
    {
        Vector3 center = ringCenter.position;
        Vector3 forward = ringCenter.forward;
        Vector3 basePos = center - forward * spawnDistance;
        Vector3 right = ringCenter.right;

        for (int i = 0; i < count; i++)
        {
            float offset = (i - (count - 1) / 2f) * lateralSpacing;
            Vector3 spawnPos = basePos + right * offset;
            Quaternion rotation = Quaternion.LookRotation(forward);

            GameObject playerObj = Instantiate(playerPrefab, spawnPos, rotation, playerParent);
            spawnedPlayers.Add(playerObj.transform);
        }
    }

    public void SpawnEnemies(int count)
    {
        Vector3 center = ringCenter.position;
        Vector3 forward = ringCenter.forward;
        Vector3 basePos = center + forward * spawnDistance;
        Vector3 right = ringCenter.right;

        for (int i = 0; i < count; i++)
        {
            float offset = (i - (count - 1) / 2f) * lateralSpacing;
            Vector3 spawnPos = basePos + right * offset;

            Quaternion rotation = Quaternion.LookRotation(-forward);
            GameObject enemyObj = Instantiate(enemyPrefab, spawnPos, rotation, enemyParent);

            EnemyAI enemyAI = enemyObj.GetComponent<EnemyAI>();

            int playerIndex = i % spawnedPlayers.Count;
            enemyAI.target = spawnedPlayers[playerIndex];
        }
    }
}
