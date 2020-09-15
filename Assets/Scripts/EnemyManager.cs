using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    float spawnTime = 2;

    public Model model;
    public GameObject player;

    Transform playerTransform;
    
    Vector3 spawnPosition;
    float screenSizeX;
    float screenSizeY;

    // Start is called before the first frame update
    void Start() {
        model.GameOver += OnGameOver;
        
        Vector3 worldDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
        screenSizeX = worldDimensions.x - 1;
        screenSizeY = worldDimensions.y - 1;

        spawnTime = PlayerPrefs.GetInt("SpawnTime", 1);

        playerTransform = player.transform;
        
        InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
    }

    private void SpawnEnemies() {
        int emergencyExit = 0;
        int enemyId = Random.Range(0, enemies.Count);
        bool created = false;
        do {
            spawnPosition = new Vector3(Random.Range(-screenSizeX, screenSizeX), Random.Range(-screenSizeY, screenSizeY), 0);

            emergencyExit++;
            if (emergencyExit > 8) {
                spawnPosition = new Vector3(screenSizeX, -screenSizeY);
            }

            if (!Physics2D.CircleCast(spawnPosition, 0.5f, Vector2.zero, 1 << LayerMask.NameToLayer("Island")) && !Physics2D.CircleCast(spawnPosition, 2f, Vector2.zero, 1 << LayerMask.NameToLayer("Player"))){                
                GameObject newEnemy = Instantiate(enemies[enemyId], spawnPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                newEnemy.GetComponent<Enemy>().playerTransform = playerTransform;
                newEnemy.GetComponent<Enemy>().Destroyed += model.OnEnemyDestroyed;
                model.GameOver += newEnemy.GetComponent<Enemy>().OnGameOver;
                created = true;
            }else if (emergencyExit > 8) {
                break;
            }
          
        } while (!created);

    }

    private void OnGameOver() {
        CancelInvoke("SpawnEnemies");
    }
}
