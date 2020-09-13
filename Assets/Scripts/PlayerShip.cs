using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {
    public int playerHealth;
    public GameObject bullet;
    public float playerSpeed;
    public int playerDamage;

    private Transform frontCannon;
    private Transform[] lateralCannon = new Transform[8];

    [SerializeField] private Collider2D shipCollider = null;

    public delegate void PlayerDestroyed();
    public event PlayerDestroyed Destroyed;

    public GameObject lifeStatusObject;
    LifeStatus lifeBar;

    // Start is called before the first frame update
    void Start() {
        lifeBar = Instantiate(lifeStatusObject).GetComponent<LifeStatus>();
        lifeBar.Setup(transform, playerHealth);
        frontCannon = gameObject.transform.GetChild(0);
        for (int i = 0; i < 6; i++) {
            lateralCannon[i] = gameObject.transform.GetChild(i + 1);
        }
    }

    // Update is called once per frame
    void Update() {
        //Remover ao fazer o sistema de controles
        if (Input.GetKey(KeyCode.UpArrow)) {
            MoveForward();
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            FrontShoot();
        } else if (Input.GetKeyDown(KeyCode.X)) {
            SideShoot();
        }
        RotateShip(Input.GetAxis("Horizontal"));
    }

    public void MoveForward() {
        transform.Translate(Vector3.down * Time.deltaTime * playerSpeed);
    }

    public void RotateShip(float direction) {
        transform.Rotate(0, 0, direction * -90 * Time.deltaTime);
    }

    public void FrontShoot() {
        Instantiate(bullet, frontCannon.position, frontCannon.rotation).GetComponent<CannonBall>().Setup(playerDamage, shipCollider);
    }

    public void SideShoot() {
        for (int i = 0; i < 6; i++) {
            Instantiate(bullet, lateralCannon[i].position, lateralCannon[i].rotation).GetComponent<CannonBall>().Setup(playerDamage, shipCollider);
        }
    }

    public void ReceiveDamage(int damage) {
        playerHealth -= damage;
        lifeBar.UpdateLife(playerHealth);
        if (playerHealth <= 0) {
            if (Destroyed != null) {
                Destroyed();
            }
            Destroy(gameObject);
        }
    }
}
