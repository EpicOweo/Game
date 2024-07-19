using System;
using System.Collections;
using System.Reflection.Emit;
using Settings;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using SerializeReferenceEditor;
using System.Collections.Generic;
using UnityEngine.Events;

public class Player : Entity {

    public static Player instance;

    public struct PlayerData {
        public PlayerInputComponent _inputComponent;
        public PlayerGraphicsComponent _graphicsComponent;
        public PlayerPhysicsComponent _physicsComponent;
    }


    public PlayerInputComponent inputComponent;
    public PlayerGraphicsComponent graphicsComponent;
    public PlayerPhysicsComponent physicsComponent;


    [NonSerialized] public bool touchingCeiling;
    [NonSerialized] public bool touchingGround;

    public BoxCollider2D boxCollider;

    public bool flipped = false;

    [NonSerialized] public UnityEvent onFlipped = new();
    [NonSerialized] public UnityEvent<int> onWalk = new();
    [NonSerialized] public static UnityEvent newPlayerCreated = new();


    void Awake() {
        instance = this;
    }

    protected override void Start() {
        newPlayerCreated.Invoke();
    }

    public void CleanUp() {
        inputComponent.CleanUp();
        graphicsComponent.CleanUp();
        physicsComponent.CleanUp();
    }

    public PlayerData GetPlayerData() {
        return new PlayerData() {
            _inputComponent = inputComponent,
            _graphicsComponent = graphicsComponent,
            _physicsComponent = physicsComponent
        };
    }
    public void SetPlayerData(PlayerData playerData) {
        Destroy(inputComponent.gameObject);
        Destroy(graphicsComponent.gameObject);
        Destroy(physicsComponent.gameObject);

        inputComponent = playerData._inputComponent;
        graphicsComponent = playerData._graphicsComponent;
        physicsComponent = playerData._physicsComponent;

        inputComponent.transform.SetParent(transform);
        graphicsComponent.transform.SetParent(transform);
        physicsComponent.transform.SetParent(transform);
        
        inputComponent.transform.localPosition = Vector3.zero;
        graphicsComponent.transform.localPosition = Vector3.zero;
        physicsComponent.transform.localPosition = Vector3.zero;

        inputComponent.Init();
        graphicsComponent.Init();
        physicsComponent.Init();
    }

}