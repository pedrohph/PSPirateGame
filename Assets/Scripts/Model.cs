﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public Hud hud;

    int time = 60;
    public int totalScore = 0;
    public PlayerShip player;

    public delegate void OnGameOver();
    public event OnGameOver GameOver;

    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start() {
        time = PlayerPrefs.GetInt("SessionTime", 60);
        player.Destroyed += OnPlayerDestroyed;
        hud.UpdateTime(time);
        InvokeRepeating("DecreaseTime", 1, 1);
    }

    // Update is called once per frame
    void Update() {

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
