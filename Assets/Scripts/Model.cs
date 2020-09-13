using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {
    public Hud hud;

    public float time = 60;
    public int totalScore = 0;
    public PlayerShip player;

    // Start is called before the first frame update
    void Start() {
        player.Destroyed += OnPlayerDestroyed;
        hud.UpdateTime(time);
        InvokeRepeating("DecreaseTime", 1, 1);
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnEnemyDestroyed(Enemy destroyedEnemy) {
        destroyedEnemy.ReceivedPoint -= OnReceivePoint;
        destroyedEnemy.Destroyed -= OnEnemyDestroyed;
    }

    public void OnReceivePoint() {
        totalScore++;
        hud.UpdateScore(totalScore);
    }

    public void DecreaseTime() {
        time--;
        hud.UpdateTime(time);
    }

    public void OnPlayerDestroyed() {
        CancelInvoke("DecreaseTime");
    }
}
