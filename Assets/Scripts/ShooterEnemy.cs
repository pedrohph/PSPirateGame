using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy {

    private Transform FrontCannon;

    [SerializeField] Transform sightTransform = null;
    public float visionRange;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start() {
        FrontCannon = gameObject.transform.GetChild(0);
        sightTransform.localPosition = Vector3.down * visionRange;
    }

    public override void Attack() {
        attacking = true;
        Instantiate(bullet, FrontCannon.position, FrontCannon.rotation).GetComponent<CannonBall>().Setup(enemyDamage, shipCollider);
        StartCoroutine(Recharge());
    }

    public override void Move() {
        if (Physics2D.Linecast(transform.position, sightTransform.position, 1 << LayerMask.NameToLayer("Player"))) {
            Attack();
        } else {
            base.Move();
        }
    }

    public IEnumerator Recharge() {
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }
}
