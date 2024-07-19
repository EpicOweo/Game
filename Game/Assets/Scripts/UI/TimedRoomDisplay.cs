using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

public class TimedRoomDisplay : MonoBehaviour {

    private bool flag = false;

    public List<Transform> displays;

    void Update() {
        if(flag) return;

        foreach(var room in Level.instance.rooms) {
            room.onRoomEntered.AddListener(() => {
                if(room.isTimed) {

                    foreach(var child in displays) {
                        child.gameObject.SetActive(true);

                        Clock clock = child.GetComponent<Clock>();

                        if(clock != null) {
                            clock.SetClock(room.time);
                            Debug.Log(room.time);
                            clock.StartClock();
                        }  
                    }

                    room.onRoomCompleted.AddListener(() => {
                        StartCoroutine(FadeToDisable());
                    });

                } else {
                    DisableAll();
                }
            });
        }

        DisableAll();

        flag = true;
    }

    void DisableAll() {
        foreach(var child in displays) {
            child.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeToDisable() {
        //error here?
        float opacity = 1;

        List<Image> images = new();
        foreach(var child in displays) {
            images.Add(child.GetComponent<Image>());
        }

        while(opacity > 0) {
            foreach(var img in images) {
                img.color = new(img.color.r, img.color.g, img.color.b, opacity);
            }
            opacity -= Time.deltaTime;
            yield return null;
        }
        
        
        DisableAll();
    }
}