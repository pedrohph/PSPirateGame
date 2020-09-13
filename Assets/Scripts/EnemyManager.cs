using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    public Model model;
    public GameObject player;
    Transform playerTransform;
    Vector3 spawnPosition;

    public float spawnTime = 2;
    
    float screenSizeX;
    float screenSizeY;

    // Start is called before the first frame update
    void Start() {

        playerTransform = player.transform;

        PlayerShip playerShip = player.GetComponent<PlayerShip>();
        playerShip.Destroyed += OnPlayerDestroyed;

        Vector3 worldDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
        screenSizeX = worldDimensions.x;
        screenSizeY = worldDimensions.y;

        InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
    }

    private void SpawnEnemies() {
        int enemyId = Random.Range(0, enemies.Count);

        spawnPosition = new Vector3(Random.Range(-screenSizeX, screenSizeX), Random.Range(-screenSizeY, screenSizeY), 0);

        if (Physics2D.CircleCast(spawnPosition, 0.5f, Vector2.zero, 1 << LayerMask.NameToLayer("Island"))) {
            SpawnEnemies();
        } else {
            GameObject newEnemy = Instantiate(enemies[enemyId], spawnPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            newEnemy.GetComponent<Enemy>().playerTransform = playerTransform;

            player.GetComponent<PlayerShip>().Destroyed += newEnemy.GetComponent<Enemy>().OnPlayerDestroyed;
            newEnemy.GetComponent<Enemy>().ReceivedPoint += model.OnReceivePoint;
            newEnemy.GetComponent<Enemy>().Destroyed += model.OnEnemyDestroyed;
        }

        
    }

    private void OnPlayerDestroyed() {
        CancelInvoke("SpawnEnemies");
    }
}
