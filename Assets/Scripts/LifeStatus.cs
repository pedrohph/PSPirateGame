using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStatus : MonoBehaviour {
    public Transform targetTransform;
    [SerializeField] Vector3 distance = Vector3.up;

    float maxLife;

   Transform healthBar;

    // Update is called once per frame
    void Update() {
        healthBar = transform.GetChild(0);
        if(targetTransform == null) {
            Destroy(gameObject);
        } else {
            transform.position = targetTransform.position + distance;
        }
    }

    public void Setup(Transform target, int maxLife) {
        targetTransform = target;
        this.maxLife = maxLife;
        transform.position = targetTransform.position + distance;
    }

    public void UpdateLife(int currentLife) {
        healthBar.localScale = new Vector3(currentLife/ maxLife,1,0);
    }
}
