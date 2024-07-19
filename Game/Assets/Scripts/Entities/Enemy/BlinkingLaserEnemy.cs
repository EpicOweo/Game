using System.Collections;
using UnityEngine;

public class BlinkingLaserEnemy : MonoBehaviour
{

    public float timeToWait = 1f;
    public bool on = true;
    public Laser laser;

    void Start()
    {
        StartCoroutine(BlinkLaser());
    }

    IEnumerator BlinkLaser() {
        laser.gameObject.SetActive(on);
        while(true) {
            yield return new WaitForSeconds(timeToWait);
            on = !on;
            laser.gameObject.SetActive(on);
        }
    }
}
