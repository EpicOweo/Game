using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPattern : MonoBehaviour {

    public float duration = 5;
    public float timePersistsAfterStop = 5;
    public bool persistsBetweenAttacks = false;

    public bool toggleable = false;
    [NonSerialized] public bool toggled = false;


    protected bool stopped = false;

    protected bool firstTime = true;


    public virtual void Run() {
        if(stopped) return;
        if(firstTime) {
            RunFirstTime();
            firstTime = false;
        }
    }

    public virtual void RunFirstTime() {

    }

    public virtual IEnumerator Stop(float timeToWait) { yield return null; }


    public IEnumerator DoWhatElectrodeDoes(float timeUntilMurder) {
        transform.SetParent(GameObject.Find("The Chopping Block").transform);
        yield return new WaitForSeconds(timeUntilMurder);
        Destroy(gameObject);
    }   
}