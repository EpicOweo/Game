using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour {

    public List<RoomCompletionRequirement> completionRequirements;
    [SerializeField] [ReadOnly] private int requirementsSatisfied;
    

    public bool isTimed { get; private set; }
    [ConditionalField(nameof(isTimed))] public float time { get; set; }

    public bool active = false;

    [NonSerialized] public UnityEvent onRoomCompleted = new();
    [NonSerialized] public UnityEvent onRoomEntered = new();
    [NonSerialized] public bool completed = false; 


    public List<Door> doors;
    public int roomId;

    void Awake() {
        onRoomEntered.AddListener(() => {
            foreach(var req in completionRequirements) {
                req.gameObject.SetActive(true);
            }
        });
    }

    void Start() {
        foreach(var req in completionRequirements) {
            req.onRequirementCompleted.AddListener(() => {
                requirementsSatisfied++;
            });
            req.onRequirementUncompleted.AddListener(() => {
                requirementsSatisfied--;
            });

            if(req.isTimedReq) {
                isTimed = true;
                time = req.time;
            }
        }
    }

    void Update() {
        if(requirementsSatisfied == completionRequirements.Count) {
            onRoomCompleted.Invoke();
        }
    }

}