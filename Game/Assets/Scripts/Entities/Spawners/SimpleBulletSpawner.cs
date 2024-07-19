using UnityEngine;

public class SimpleBulletSpawner : MonoBehaviour {
    public SimpleBullet prefab;

    public void Spawn(Vector3 position, Vector2 velocity) {
        SimpleBullet bullet = Instantiate(prefab, position, Quaternion.identity);
        //bullet.rb.velocity = velocity;
    }
}