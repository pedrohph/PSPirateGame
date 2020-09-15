using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public delegate void OnGameOver();
    public event OnGameOver GameOver;

    int time = 60;
    int totalScore = 0;
    [Header("Player information")]
    public PlayerShip player;

    [Header("GUI")]
    public Hud hud;
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start() {
        player.Destroyed += OnPlayerDestroyed;

        time = PlayerPrefs.GetInt("SessionTime", 60);
        hud.UpdateTime(time);
        InvokeRepeating("DecreaseTime", 1, 1);
    }

    public void OnEnemyDestroyed(Enemy destroyedEnemy, bool byPlayer) {
        if (byPlayer) {
            totalScore++;
            hud.UpdateScore(totalScore);
        }
        destroyedEnemy.Destroyed -= OnEnemyDestroyed;
    }

    public void DecreaseTime() {
        time--;
        hud.UpdateTime(time);
        if(time <= 0) {
            FinishGame();
        }
    }

    public void FinishGame() {
        if(GameOver != null) {
            GameOver();
        }
        CancelInvoke("DecreaseTime");
        Invoke("openGameOverScreen",1);
    }

    public void OnPlayerDestroyed() {
        FinishGame();
    }

    public void openGameOverScreen() {
        gameOverScreen.GetComponent<GameOverMenu>().setScore(totalScore);
        gameOverScreen.SetActive(true);
    }
}
