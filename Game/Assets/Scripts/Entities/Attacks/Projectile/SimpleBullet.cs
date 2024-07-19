using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : Projectile {
    
    protected override void Start() {
        base.Start();
        List<Color> colors = new()
        {
            new(1, 0, 0), // default red
            new(115f/255, 206f/255, 1) // purple ish
        };

        foreach(Transform t in GetComponentsInChildren<Transform>()) {
            if(t.name == "Circle") {
                t.GetComponent<Renderer>().material.SetColor("_Color", colors[Level.instance.colorPreset]);
            }
        }
    }
}