using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int enemyHealth;
    public float enemySpeed;
    public int enemyDamage;

    public Transform playerTransform;
    float angleRotation;
    Vector3 targetPosition;
    Vector3 targetVectorDistance;

    [SerializeField] protected Collider2D shipCollider;

    protected bool evading = false;
    protected bool attacking = false;

    [SerializeField] Transform[] evadeDestinationTransform = new Transform[2];


    // Update is called once per frame
    void Update() {
        if (!attacking) {
            Move();
        }
    }

    public void RotateShip(float direction) {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angleRotation - 90), enemySpeed * Time.deltaTime);
    }

    public virtual void Move() {
        if (evading) {
            if (Vector3.Distance(targetPosition, transform.position) <= 3) {
                if (!Physics2D.Linecast(transform.position, playerTransform.position, 1 << LayerMask.NameToLayer("Island"))) {
                    evading = false;
                    targetPosition = playerTransform.position;
                }
            }
            if (Vector3.Distance(targetPosition, transform.position) < 0.2f) {
                evading = false;
                targetPosition = playerTransform.position;
            }
        } else if (Physics2D.Linecast(transform.position, playerTransform.position, 1 << LayerMask.NameToLayer("Island"))) {
            CheckNewWay(-1);
        } else {
            targetPosition = playerTransform.position;
            evading = false;
        }

        targetVectorDistance = transform.position - targetPosition;
        angleRotation = Mathf.Atan2(targetVectorDistance.y, targetVectorDistance.x) * Mathf.Rad2Deg;

        RotateShip(angleRotation);

        transform.Translate(Vector3.down * Time.deltaTime * enemySpeed);
    }

    private void CheckNewWay(int yDirection) {
        if (!evading) {
            evadeDestinationTransform[0].localPosition = new Vector3(-1, yDirection, 0) * Vector3.Distance(transform.position, playerTransform.position) / 2;
            evadeDestinationTransform[1].localPosition = new Vector3(1, yDirection, 0) * Vector3.Distance(transform.position, playerTransform.position) / 2;

            if (Vector3.Distance(evadeDestinationTransform[0].position, playerTransform.position) > Vector3.Distance(evadeDestinationTransform[1].position, playerTransform.position)) {
                Vector3 tempValue = evadeDestinationTransform[0].localPosition;
                evadeDestinationTransform[0].localPosition = evadeDestinationTransform[1].localPosition;
                evadeDestinationTransform[1].localPosition = tempValue;
            }


            if (!Physics2D.Linecast(evadeDestinationTransform[0].position, transform.position, 1 << LayerMask.NameToLayer("Island"))) {
                if (!Physics2D.Linecast(evadeDestinationTransform[0].position, playerTransform.position, 1 << LayerMask.NameToLayer("Island"))) {
                    targetPosition = evadeDestinationTransform[0].position;
                    evading = true;
                    return;
                }
            }
            if (!Physics2D.Linecast(evadeDestinationTransform[1].position, transform.position, 1 << LayerMask.NameToLayer("Island"))) {
                if (!Physics2D.Linecast(evadeDestinationTransform[1].position, playerTransform.position, 1 << LayerMask.NameToLayer("Island"))) {
                    targetPosition = evadeDestinationTransform[1].position;
                    evading = true;
                    return;
                }
            }

            if (yDirection < 0) {
                CheckNewWay(1);
            } else {
                evadeDestinationTransform[0].localPosition = new Vector3(-1, 1, 0);
                targetPosition = evadeDestinationTransform[0].position;
            }
        }
    }

    public virtual void Attack() { }

    public void ReceiveDamage(int damage) {
        enemyHealth -= damage;
        if(enemyHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
