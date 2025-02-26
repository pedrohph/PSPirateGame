﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy {

    private Transform frontCannon;

    [Header("Shooter Parameters")]
    public float rechargeTime;
    public float visionRange;

    public Transform sightTransform = null;

    [Header("Shooter Objects")]
    public GameObject bullet;
    public GameObject cannonExplosion;
    
    // Start is called before the first frame update
    void Start() {
        frontCannon = gameObject.transform.GetChild(0);
        sightTransform.localPosition = Vector3.down * visionRange;
    }

    public override void Attack() {
        attacking = true;
        Instantiate(cannonExplosion, frontCannon.position, frontCannon.rotation, transform);
        GameObject newBullet = Instantiate(bullet, frontCannon.position, frontCannon.rotation);
        newBullet.GetComponent<CannonBall>().Setup(enemyDamage, shipCollider);
        newBullet.layer = LayerMask.NameToLayer("EnemyBullet");
        StartCoroutine(Recharge());
    }

    public override void Move() {
        if (Physics2D.Linecast(transform.position, sightTransform.position, 1 << LayerMask.NameToLayer("Player"))) {
            Attack();
        } else {
            base.Move();
        }
    }

    private IEnumerator Recharge() {
        yield return new WaitForSeconds(rechargeTime);
        attacking = false;
    }
}
