using UnityEngine;
using UnityEngine.Events;

public class ColorChip : MonoBehaviour {
    
    public int id = 0;
    public static UnityEvent<int> onChipCollected = new();

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("PlayerCollider")) {
            
            Level.instance.collectibles.colorChips[id] = true;

            gameObject.SetActive(false); // play some kind of animation later
            onChipCollected.Invoke(id);
        }
    }

}
