using System;
using UnityEngine;
using UnityEngine.Events;

public class SetRoomTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("PlayerCollider")) {
            Room room = GetComponentInParent<Room>();
            room.active = true;
            Level.instance.currentRoom = room;
            room.onRoomEntered.Invoke();
        }
    }
}