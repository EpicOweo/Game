using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestLevel : MonoBehaviour {

    void Start() {
        SetRoomListeners();
    }

    void SetRoomListeners() {
        var room1 = Level.instance.rooms[1];

        /*room1.onRoomEntered.AddListener(() => {
            Level.instance.mainCamera.backgroundColor = new(1, 0, 0, 1);
        });

        room1.onRoomCompleted.AddListener(() => {
            Level.instance.mainCamera.backgroundColor = new(0, 1, 0, 1);
        });*/
    }
}