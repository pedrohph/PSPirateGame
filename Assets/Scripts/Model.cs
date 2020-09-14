using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public Hud hud;

    public int time = 60;
    public int totalScore = 0;
    public PlayerShip player;

    public delegate void OnGameOver();
    public event OnGameOver GameOver;


    // Start is called before the first frame update
    void Start() {
        player.Destroyed += OnPlayerDestroyed;
        hud.UpdateTime(time);
        InvokeRepeating("DecreaseTime", 1, 1);
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnEnemyDestroyed(Enemy destroyedEnemy, bool byPlayer) {
        // destroyedEnemy.ReceivedPoint -= OnReceivePoint;
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
    }

    public void OnPlayerDestroyed() {
        FinishGame();
    }
}
