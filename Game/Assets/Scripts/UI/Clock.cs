using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {
    public List<Sprite> sprites;
    private Image image;

    private float totalTime = 5;
    private float timeElapsed = 0;


    void Awake() {
        image = GetComponent<Image>();
    }

    public void SetClock(float time) {
        totalTime = time;
        timeElapsed = 0;
    }

    public void StartClock() {
        StartCoroutine(RunClock());
    }

    private IEnumerator RunClock() {

        while(timeElapsed < totalTime) {
            image.sprite = sprites[GetClockState()];
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        image.sprite = sprites[0];

    }

    private int GetClockState() {

        float percent = timeElapsed / totalTime;
        int state = Mathf.FloorToInt(percent * 8);

        return state;
    }

}