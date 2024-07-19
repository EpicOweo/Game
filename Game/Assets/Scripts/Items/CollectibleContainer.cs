using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CollectibleContainer", order = 1)]
public class CollectibleContainer : ScriptableObject {

    public bool[] colorChips = new bool[6] {
        false, false, false, false, false, false
    };


}