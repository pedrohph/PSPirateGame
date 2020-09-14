using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : Enemy
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.GetComponent<PlayerShip>() != null) {
            collision.gameObject.GetComponent<PlayerShip>().ReceiveDamage(enemyDamage);
            Explode(false);
        }
    }
}
