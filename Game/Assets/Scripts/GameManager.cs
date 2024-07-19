using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    public Settings.Keybinds keybinds;

    void Awake() {
        instance = this;
        keybinds = new();
    }

    void OnEnable() {
        keybinds.Enable();
    }

    void OnDisable() {
        keybinds.Disable();
    }
    
}
