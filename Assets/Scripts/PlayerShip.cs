using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {
    public GameObject bullet;
    public float playerSpeed;
    public int playerDamage;

    private Transform FrontCannon;
    private Transform[] LateralCannon = new Transform[8];

    [SerializeField] private Collider2D shipCollider = null;
    // Start is called before the first frame update
    void Start() {
        FrontCannon = gameObject.transform.GetChild(0);
        for(int i = 0; i<6; i++) {
            LateralCannon[i] = gameObject.transform.GetChild(i + 1);
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
        }else if (Input.GetKeyDown(KeyCode.X)) {
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
        Instantiate(bullet,FrontCannon.position, FrontCannon.rotation).GetComponent<CannonBall>().Setup(playerDamage, shipCollider);
    }

    public void SideShoot() {
        for(int i = 0; i<6; i++) {
            Instantiate(bullet, LateralCannon[i].position, LateralCannon[i].rotation).GetComponent<CannonBall>().Setup(playerDamage, shipCollider);
        }
    }
}
