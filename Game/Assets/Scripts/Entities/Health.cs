using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;

public class Health : MonoBehaviour
{
    
    public int maxHealth;
    public int currentHealth;
    public int lifeRegen;

    float lifeRegenTimer = 0;
    public float lifeRegenSec = 0;


    public float iFrameTime = 0;
    float iFrameTimer;

    public bool instaKill = false;


    public UnityEvent onKill = new();
    public UnityEvent onDamage = new();

    public bool invulnerable = false;
    bool dead = false;


    public void Start() {
        lifeRegenTimer = Time.time;
        iFrameTimer = Time.time;
    }

    public void Update() {
        if(Time.time - lifeRegenTimer >= lifeRegenSec) {
            Heal(lifeRegen);
            lifeRegenTimer = Time.time;
        }
    }

    public void Reset() {
        currentHealth = maxHealth;
        dead = false;
        iFrameTimer = 0;
    }

    public virtual void Kill() {
        if(!dead) onKill.Invoke();
        if(iFrameTimer != 0) dead = true; // didn't just get reset
    }

    public virtual void Damage(int damage) {
        if(invulnerable) return;
        if(Time.time - iFrameTimer >= iFrameTime) { // not in iframes
            currentHealth -= damage;
            if(currentHealth <= 0)  {
                currentHealth = 0;
                Kill();
            }
            iFrameTimer = Time.time;
            onDamage.Invoke();
        }
    }
    public virtual void Heal(int health, bool overheal = false) {
        currentHealth += health;
        if(!overheal && currentHealth > maxHealth) currentHealth = maxHealth;
    }
    public virtual bool IsDead() { return currentHealth <= 0; }

}
