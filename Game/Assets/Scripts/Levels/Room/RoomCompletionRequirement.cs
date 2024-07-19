using System;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public abstract class RoomCompletionRequirement : MonoBehaviour {

    [NonSerialized] public UnityEvent onRequirementCompleted = new();
    [NonSerialized] public UnityEvent onRequirementUncompleted = new();
    [ReadOnly] public bool completed = false;
    public bool updateAfterCompletion = false;
    
    public bool isTimedReq { get; protected set; }
    public float time = 15;


    void Awake() {
        gameObject.SetActive(false);
    }

    protected virtual void Update() {
        if(!updateAfterCompletion && !completed) {
            if(IsCompleted()) {
                if(!completed) onRequirementCompleted.Invoke();
                completed = true;
            }
        } else if(updateAfterCompletion) {
            if(IsCompleted()) {
                if(!completed) onRequirementCompleted.Invoke();
                completed = true;
            } else {
                if(completed) onRequirementUncompleted.Invoke();
                completed = false;
            }
        }
    }

    public abstract bool IsCompleted();
}