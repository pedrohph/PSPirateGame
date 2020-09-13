using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour {
    public Text textScore;
    public Text textTime;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void UpdateScore(int totalScore) {
        textScore.text = "" + totalScore;
    }

    public void UpdateTime(float time) {
        textTime.text = "" + time;
    }
}
