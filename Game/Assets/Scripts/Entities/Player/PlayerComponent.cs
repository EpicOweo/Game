using UnityEngine;

public class PlayerComponent : MonoBehaviour {
    
    public GameObject prefab;


    protected virtual void Start() {
        Init();
    }

    public virtual void CleanUp() {}
    public virtual void Init() {}
}