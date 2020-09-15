using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {
    public delegate void PlayerDestroyed();
    public event PlayerDestroyed Destroyed;

    [Header("Player parameters")]
    public int playerHealth;
    int currentHealth;
    public float playerSpeed;
    public int playerDamage;

    [Header("Components")]
    private Animator animator;
    Collider2D shipCollider = null;

    [Header("Cannons")]
    private Transform frontCannon;
    private Transform[] lateralCannon = new Transform[8];
    
    [Header("External Objects")]
    public GameObject bullet;
    public GameObject cannonExplosion;
    public GameObject lifeStatusObject;
    LifeStatus lifeBar;

    Vector3 worldDimensions;

    // Start is called before the first frame update
    void Start() {
        worldDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
        worldDimensions -= Vector3.one * 0.5f;

        shipCollider = gameObject.GetComponent<Collider2D>();
        animator = gameObject.GetComponent<Animator>();

        frontCannon = gameObject.transform.GetChild(0);
        for (int i = 0; i < 6; i++) {
            lateralCannon[i] = gameObject.transform.GetChild(i + 1);
        }

        currentHealth = playerHealth;

        lifeBar = Instantiate(lifeStatusObject).GetComponent<LifeStatus>();
        lifeBar.Setup(transform, currentHealth);
    }

    public void MoveForward() {
        transform.Translate(Vector3.down * Time.deltaTime * playerSpeed);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -worldDimensions.x, worldDimensions.x), Mathf.Clamp(transform.position.y, -worldDimensions.y, worldDimensions.y));
    }

    public void RotateShip(float direction) {
        transform.Rotate(0, 0, direction * -90 * Time.deltaTime);
    }

    public void FrontShoot() {
        Instantiate(cannonExplosion, frontCannon.position, frontCannon.rotation, transform);
        Instantiate(bullet, frontCannon.position, frontCannon.rotation).GetComponent<CannonBall>().Setup(playerDamage, shipCollider);

    }

    public void SideShoot() {
        for (int i = 0; i < 6; i++) {
            Instantiate(cannonExplosion, lateralCannon[i].position, lateralCannon[i].rotation, transform);
            Instantiate(bullet, lateralCannon[i].position, lateralCannon[i].rotation).GetComponent<CannonBall>().Setup(playerDamage, shipCollider);
        }
    }

    public void ReceiveDamage(int damage) {
        currentHealth -= damage;
        lifeBar.UpdateLife(currentHealth);
        animator.SetInteger("Deterioration", (currentHealth * 100 / playerHealth));
        if (currentHealth <= 0) {
            if (Destroyed != null) {
                Destroyed();
            }
        }
    }
}
