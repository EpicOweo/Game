using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy {

    public SimpleBulletSpawner spawner;

    public List<AttackPattern> patternPrefabs = new();
    public List<AttackPattern> attackPatterns = new();
    int currentAttackIndex = 0;


    public int attackSpeed = 10;
    public float timeBetweenAttacks;


    void Awake() {
        for(int i = 0; i < patternPrefabs.Count(); i++) {
            AttackPattern pattern = Instantiate(patternPrefabs[i]);
            pattern.gameObject.SetActive(false);
            attackPatterns.Add(pattern);
        }
    }

    protected override void Start() {
        base.Start();
        StartCoroutine(Attack());
    }

    public IEnumerator Attack() {
        float timer = Time.time;
        while(true) {
            
            if(Time.time - timer < attackPatterns[currentAttackIndex].duration) {
                attackPatterns[currentAttackIndex].gameObject.SetActive(true);
                if(!attackPatterns[currentAttackIndex].toggleable
                    || !attackPatterns[currentAttackIndex].toggled) {
                    
                    attackPatterns[currentAttackIndex].toggled = true;
                    attackPatterns[currentAttackIndex].Run();
                }
            } else {

                if(!attackPatterns[currentAttackIndex].persistsBetweenAttacks) {
                    AttackPattern current = attackPatterns[currentAttackIndex];
                    attackPatterns[currentAttackIndex] = Instantiate(patternPrefabs[currentAttackIndex]);
                    attackPatterns[currentAttackIndex].gameObject.SetActive(false);

                    float timeToWait = current.timePersistsAfterStop > 1 ? 1 : 0;
                    StartCoroutine(current.Stop(timeToWait));
                    //yield return new WaitForSeconds(timeToWait);
                }
                

                if(currentAttackIndex == attackPatterns.Count() - 1) {
                    currentAttackIndex = 0;
                } else {
                    currentAttackIndex++;
                }

                timer = Time.time;

            }

            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }
}