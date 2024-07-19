using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombinedAttackPattern : AttackPattern {
    List<AttackPattern> attackPatterns = new();
    public List<AttackPattern> attackPatternPrefabs;

    public override void Run() {
        base.Run();

        foreach(AttackPattern pattern in attackPatterns.ToList()) {
            if(!pattern.toggleable || !pattern.toggled) {
                pattern.toggled = true;
                pattern.Run();
            }
            
        }
    }

    public override void RunFirstTime()
    {
        base.RunFirstTime();

        foreach(AttackPattern pattern in attackPatternPrefabs.ToList()) {
            if(!pattern.toggleable) { // don't double up on these
                AttackPattern inst = Instantiate(pattern);
                attackPatterns.Add(inst);
                inst.transform.SetParent(transform);
            }
        }
    }

    public override IEnumerator Stop(float timeToWait) {
        stopped = true;
        foreach(AttackPattern pattern in GetComponentsInChildren<AttackPattern>()
            .Where((pattern) => { return pattern != this; } )) {
                
            StartCoroutine(pattern.Stop(timeToWait));
        }
        yield return new WaitUntil(() => { return transform.childCount == 0; });
        Destroy(gameObject);
    }

}