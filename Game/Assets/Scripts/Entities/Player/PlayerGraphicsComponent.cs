using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphicsComponent : PlayerComponent
{

    [Serializable] public struct SpriteList {
        public List<Sprite> sprites;
    }
    public List<SpriteList> playerSprites;

    public ParticleSystem walkingParticles;
    


    public override void Init() {
        base.Init();

        Player.instance.onWalk.AddListener((direction) => SetWalkingParticlePos(direction));
        Player.instance.onFlipped.AddListener(ChangeSpriteOnFlip);

        SetSprite(playerSprites[Level.instance.colorPreset].sprites[0]);
    }

    void Update() {
        HandleWalkingParticles();
    }

    public PlayerGraphicsComponent Clone() {
        PlayerGraphicsComponent graphicsComponent = Instantiate(prefab).GetComponent<PlayerGraphicsComponent>();

        return graphicsComponent;
    }

    void SetSprite(Sprite sprite) {
        foreach(Transform obj in GetComponentsInChildren<Transform>()) {
            if(obj.name == "Player Sprite") {
                obj.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
    }

    void ChangeSpriteOnFlip() {
        int index = Player.instance.flipped ? 1 : 0;
        SetSprite(playerSprites[Level.instance.colorPreset].sprites[index]);
    }

    void SetWalkingParticlePos(int direction) {
        walkingParticles.transform.localPosition = new Vector3 (
            -direction * 0.4f,
            walkingParticles.transform.localPosition.y
        );
    }

    void HandleWalkingParticles() {
        if(Player.instance.touchingGround || Player.instance.touchingCeiling) {

            int sign = Player.instance.touchingCeiling ? 1 : -1;
            walkingParticles.transform.localPosition = new Vector3 (
                walkingParticles.transform.localPosition.x,
                sign * 0.4f
            );

            if(!walkingParticles.isPlaying) {
                walkingParticles.Play();
            }
        } else {
            walkingParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}