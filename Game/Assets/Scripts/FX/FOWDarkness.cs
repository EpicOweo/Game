using System;
using Unity.VisualScripting;
using UnityEngine;

public class FOWDarkness : MonoBehaviour {

    public GameObject fow1;
    public GameObject fow2;
    public float fadeRate = 1;

    SpriteRenderer sr1;
    SpriteRenderer sr2;

    int currentIncreasingSR = 1;
    float maxAlpha = 0.75f;

    [NonSerialized] public bool stopped;

    void Awake() {
        sr1 = fow1.GetComponent<SpriteRenderer>();
        sr2 = fow2.GetComponent<SpriteRenderer>();

        sr2.transform.position = sr1.transform.position;
        sr2.transform.localScale = sr1.transform.localScale;

        sr1.color = new(1, 1, 1, maxAlpha);
        sr2.color = new(1, 1, 1, 0);
    }

    void Update() {
        if(stopped) return;

        float sr1_a = sr1.color.a;
        float sr2_a = maxAlpha - sr1_a;
        
        float delta = Time.deltaTime*fadeRate;

        if(currentIncreasingSR == 1) {
            if(sr1_a + delta > maxAlpha) { // over 255 alpha
                float excess = (sr1_a + delta) - maxAlpha;
                sr1_a -= excess;
                currentIncreasingSR = 2;
            } else {
                sr1_a += delta;
            }

        } else {
            if(sr2_a + delta > maxAlpha) { // over 255 alpha
                float excess = (sr2_a + delta) - maxAlpha;
                sr2_a -= excess;
                currentIncreasingSR = 1;
            } else {
                sr2_a += delta;
            }
            sr1_a = maxAlpha-sr2_a;
        }

        sr1.color = new(1, 1, 1, sr1_a);
        sr2.color = new(1, 1, 1, maxAlpha-sr1_a);
    }
}