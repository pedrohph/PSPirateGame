using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Model model;
    PlayerShip player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerShip>();
        model.GameOver += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0) {
            player.MoveForward();
        }
        player.RotateShip(Input.GetAxis("Horizontal"));


        if (Input.GetButtonDown("Fire1")) {
            player.FrontShoot();
        } else if (Input.GetButtonDown("Fire2")) {
           player.SideShoot();
        }
        
    }

    public void OnGameOver() {
        enabled=false;
    }
}
