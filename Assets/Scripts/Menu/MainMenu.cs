using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    int spawnEverySeconds = 1;
    int sessionTime = 60;

    public Animator mainMenuAnimator;
    public Animator optionsAnimator;

    public Button leftArrow;
    public Button rightArrow;

    public int maxSpawnTime = 10;
    public int minSpawnTime = 1;

    public Text textSpawnTime;

    public Text textTimeSession;
    public Slider sliderTimeSession;

    // Start is called before the first frame update
    void Start() {
        spawnEverySeconds = PlayerPrefs.GetInt("SpawnTime", 1);
        sessionTime = PlayerPrefs.GetInt("SessionTime", 60);
        sliderTimeSession.value = sessionTime;
        ChangeSliderValue();
        ArrowsButton(0);
    }

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions() {
        mainMenuAnimator.SetTrigger("Left");
        optionsAnimator.SetTrigger("Left");
    }

    public void CloseOptions() {
        mainMenuAnimator.SetTrigger("Right");
        optionsAnimator.SetTrigger("Right");
    }

    public void ArrowsButton(int direction) {
        spawnEverySeconds += direction;
        if (spawnEverySeconds <= 1) {
            spawnEverySeconds = 1;
            leftArrow.interactable = false;
            rightArrow.interactable = true;
        } else if (spawnEverySeconds >= 10) {
            spawnEverySeconds = 10;
            leftArrow.interactable = true;
            rightArrow.interactable = false;
        } else {
            leftArrow.interactable = true;
            rightArrow.interactable = true;
        }
        textSpawnTime.text = "" + spawnEverySeconds;
        PlayerPrefs.SetInt("SpawnTime", spawnEverySeconds);
    }

    public void ChangeSliderValue() {
        textTimeSession.text = sliderTimeSession.value + " seconds";
        sessionTime = (int)sliderTimeSession.value;
        PlayerPrefs.SetInt("SessionTime", sessionTime);
    }
}
