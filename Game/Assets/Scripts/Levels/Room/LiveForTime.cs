using System.Collections;
using MyBox;
using UnityEngine;

public class LiveForTime : RoomCompletionRequirement {


    [SerializeField][ReadOnly] protected float timer = 0;

    Room room;

    void Awake() {
        isTimedReq = true;
        room = GetComponentInParent<Room>();
    }

    void Start() {
        SetPlayerCreatedListener();
        room.onRoomEntered.AddListener(() => {
            StartCoroutine(Run());
        });
    }

    IEnumerator Run() {
        while(true) {
            if(room.active) {
                timer += Time.deltaTime;

                if(completed) break;
                
                yield return null;
            }
        }
    }

    void SetPlayerCreatedListener() {
        Player.newPlayerCreated.AddListener(() => {
            timer = 0;
        });
    }

    public override bool IsCompleted() {
        return timer >= time;
    }
}