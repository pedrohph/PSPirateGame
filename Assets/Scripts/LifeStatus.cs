﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStatus : MonoBehaviour {
    [SerializeField] Vector3 distance = Vector3.up;

    float maxLife;

    Transform healthBar;
    public Transform targetTransform;
    
    // Update is called once per frame
    void Update() {
        if (targetTransform == null) {
            Destroy(gameObject);
        } else {
            transform.position = targetTransform.position + distance;
        }
    }

    public void Setup(Transform target, int maxLife) {
        targetTransform = target;
        this.maxLife = maxLife;
        transform.position = targetTransform.position + distance;
        healthBar = transform.GetChild(0);
    }

    public void UpdateLife(int currentLife) {
        if (healthBar == null) {
            healthBar = transform.GetChild(0);
        }
        if (currentLife < 0) {
            currentLife = 0;
        }
        healthBar.localScale = new Vector3(currentLife / maxLife, 1, 0);
    }
}
