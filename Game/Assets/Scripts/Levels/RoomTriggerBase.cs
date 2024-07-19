using Unity.VisualScripting;
using UnityEngine;

public class RoomTriggerBase : MonoBehaviour {
    public Transform fow;

    void Awake() {
        if(fow != null) {
            fow.gameObject.SetActive(true);
        }
        
        var reveal = GetComponentInChildren<RevealRoomTrigger>();
        var set = GetComponentInChildren<SetRoomTrigger>();
        var darkness = GetComponentInChildren<FOWDarkness>(includeInactive: true);

        if(reveal != null) {
            reveal.GetComponent<SpriteRenderer>().enabled = false;
        }
        if(darkness != null) {
            darkness.gameObject.SetActive(true);
        }
        
    }
}