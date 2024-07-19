using UnityEngine;

public class AttachToPlayer : MonoBehaviour {
    void Update() {
        transform.localPosition = Player.instance.transform.position;
    }
}