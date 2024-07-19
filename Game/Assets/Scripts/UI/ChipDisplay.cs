using UnityEngine;
using UnityEngine.UI;

public class ChipDisplay : MonoBehaviour {
    
    public Image display;
    public Image[] chips;

    void Start() {
        ColorChip.onChipCollected.AddListener((id) => {
            chips[id].gameObject.SetActive(true);
        });
    } 
}