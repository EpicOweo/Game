using System;
using System.Collections;
using UnityEngine;

public class ProjectileCluster : MonoBehaviour {
    
    public float timer;
    bool reactivated = false;

    public float initialAngle;
    public float clusterAngularVelocity = 0;
    [NonSerialized] public float currentAngle = 0;


    // clusters with the same id will have their rotations evenly spaced out 
    public int symmetricalRotationId = -1;

    public int indexInAttack;

    public bool soloCluster = false;
    public bool persistsOnAttackDeletion = true;



    [NonSerialized] public float timeUntilMurder = 3;


    void Awake() {
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }

    void Start() {

        ClusterAttackPattern attackPattern = GetComponentInParent<ClusterAttackPattern>();

        int counter = 0;
        foreach(ProjectileCluster cluster in attackPattern.clusters) {
            if(cluster == this) {
                indexInAttack = counter;
            }
            counter++;
        }
    }

    public void setCurrentAngle() {
        currentAngle = initialAngle;
    }

    IEnumerator DoWhatElectrodeDoes() {
        transform.SetParent(GameObject.Find("The Chopping Block").transform);
        yield return new WaitForSeconds(timeUntilMurder);
        Destroy(gameObject);
    }

    void Update() {

        if(transform.childCount == 0) {
            StartCoroutine(DoWhatElectrodeDoes());
        }

        if(timer <= 0 && !reactivated) {
            foreach(Transform child in transform) {
                if(!child.TryGetComponent<Projectile>(out _)) {
                    child.gameObject.SetActive(true);
                }
            }
            foreach(Projectile child in GetComponentsInChildren<Projectile>(includeInactive: true)) {
                //child.initialDirection = Quaternion.AngleAxis(currentAngle, Vector3.forward) * child.initialDirection;
                child.initialDirection = transform.rotation * child.initialDirection;
                child.transform.gameObject.SetActive(true);
                //child.transform.SetParent(Level.instance.projectiles);
                reactivated = true;
            }
        } else {
            timer -= Time.deltaTime;
        }

        if(reactivated) {
            foreach(Projectile proj in GetComponentsInChildren<Projectile>()) {
                // TODO: allow locking rotating projectiles to a line
            }
        }
    }
}