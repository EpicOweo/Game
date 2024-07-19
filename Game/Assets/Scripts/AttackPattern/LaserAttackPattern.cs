using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserAttackPattern : AttackPattern {


    List<Laser> lasers = new();


    public enum RotationPivot {
        NONE, SELF_CENTER, POINT
    }

    public float angVel = 0;


    public RotationPivot pivot = RotationPivot.NONE;
    public Vector2 pivotPoint = new();



    void Awake() {
        lasers = GetComponentsInChildren<Laser>().ToList();
        toggleable = true;
    }

    public override void Run() {
        base.Run();

        foreach(Laser laser in lasers.ToList()) {
            laser.gameObject.SetActive(true);
        }


    }

    void Update() {

        switch(pivot) {
            case RotationPivot.NONE:
                break;

            case RotationPivot.SELF_CENTER:
                transform.Rotate(0, 0, angVel * Time.deltaTime);
                break;

            case RotationPivot.POINT:
                transform.RotateAround(pivotPoint, Vector3.forward, angVel * Time.deltaTime);
                break;

            default:
                break;
        }
    }

    public override IEnumerator Stop(float timeToWait) {
        stopped = true;
        Destroy(gameObject);
        yield return null;
    }
}