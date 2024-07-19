using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour {

    protected bool locked = false;


    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("PlayerCollider")) {
            Open();
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("PlayerCollider")) {
            Close();
        }
    }

    void Open() {
        PlayOpenAnimation();
    }

    void Close() {
        PlayCloseAnimation();
    }

    protected virtual void PlayOpenAnimation() {}

    protected virtual void PlayCloseAnimation() {}

}