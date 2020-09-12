using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField]float bulletSpeed = 10;
    [SerializeField]int bulletDamage;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);   
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }

    public void Setup(int damage, Collider2D creatorCollider) {
        bulletDamage = damage;
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), creatorCollider);
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
