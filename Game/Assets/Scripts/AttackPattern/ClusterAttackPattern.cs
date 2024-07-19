using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ClusterAttackPattern : AttackPattern {

    public List<ProjectileCluster> clusterPrefabs;
    [NonSerialized] public List<ProjectileCluster> clusters = new();
    [NonSerialized] public Dictionary<int, ProjectileCluster> lastOfEachIdInstantiated = new();
    [NonSerialized] public List<float> initialClusterTimers = new();
    [NonSerialized] public List<float> initialClusterAngVels = new();
    [NonSerialized] public List<int> initialClusterRotationIds = new();
    [NonSerialized] public List<Quaternion> initialClusterRotations = new();
    [NonSerialized] public List<Quaternion> lastClusterRotations = new();
    [NonSerialized] public List<ProjectileCluster> soloInstances = new();
    

    public Dictionary<int, int> rotationIdsCount = new();
    public Dictionary<int, int> rotationIdsInstantiatedCount = new();


    public GameObject theChoppingBlock;


    bool firstFrame = true;

    void Awake() {
        clusters = GetComponentsInChildren<ProjectileCluster>().ToList();
        foreach(ProjectileCluster cluster in clusters) {
            initialClusterTimers.Add(cluster.timer);
            initialClusterRotations.Add(cluster.transform.rotation);
            initialClusterAngVels.Add(cluster.clusterAngularVelocity);
            initialClusterRotationIds.Add(cluster.symmetricalRotationId);

            lastClusterRotations.Add(cluster.transform.rotation);

            if(cluster.soloCluster) {
                soloInstances.Add(cluster);
            } else {
                soloInstances.Add(null);
            }

            // count how many of each rotation id there is
            if(rotationIdsCount.ContainsKey(cluster.symmetricalRotationId)) {
                rotationIdsCount[cluster.symmetricalRotationId] += 1;
            } else{
                rotationIdsCount.Add(cluster.symmetricalRotationId, 1);
                rotationIdsInstantiatedCount.Add(cluster.symmetricalRotationId, 0);
            }
        }
        foreach(ProjectileCluster cluster in clusters) {
            rotationIdsInstantiatedCount[cluster.symmetricalRotationId] += 1;
        }
        foreach(ProjectileCluster cluster in clusters) {
            float count = rotationIdsInstantiatedCount[cluster.symmetricalRotationId];
            float total = rotationIdsCount[cluster.symmetricalRotationId];

            if(count == total) {
                if(lastOfEachIdInstantiated.ContainsKey(cluster.symmetricalRotationId)) {
                    lastOfEachIdInstantiated[cluster.symmetricalRotationId] = cluster;
                } else {
                    lastOfEachIdInstantiated.Add(cluster.symmetricalRotationId, cluster);
                }
                
                /*Debug.Log("added " + cluster.symmetricalRotationId);*/
            }
        }
    }

    public override IEnumerator Stop(float timeToWait) {
        //StartCoroutine(DoWhatElectrodeDoes(timeToWait));
        stopped = true;
        foreach(ProjectileCluster cluster in soloInstances.ToList()) {
            if(cluster != null) {
                soloInstances[cluster.indexInAttack] = null;
                Destroy(cluster.gameObject);
            }
        }
        yield return new WaitUntil(() => { return transform.childCount == 0; });
        Destroy(gameObject);
    }

    public override void Run() {
        base.Run();
        
        if(firstFrame) {
            foreach(ProjectileCluster cluster in clusters.ToList()) {
                //if(!cluster.soloCluster) cluster.gameObject.SetActive(false);
                if(!cluster.soloCluster) Destroy(cluster.gameObject);
            }
            firstFrame = false;
        } else {
            lastClusterRotations.Clear();
            for(int i = 0; i < clusters.Count(); i++) {
                lastClusterRotations.Add(clusters[i].transform.rotation);
            }
        }

        foreach(int key in rotationIdsInstantiatedCount.Keys.ToList()) {
            rotationIdsInstantiatedCount[key] = 0;
        }

        clusters.Clear();

        

        for(int i = 0; i < clusterPrefabs.Count(); i++) {
            if(soloInstances[i] != null) {
                clusters.Add(soloInstances[i]);
            } else {
                clusters.Add(Instantiate(clusterPrefabs[i], transform));
            }
            clusters[i].timer = initialClusterTimers[i];
            clusters[i].clusterAngularVelocity = initialClusterAngVels[i];
            clusters[i].symmetricalRotationId = initialClusterRotationIds[i];
        }
        for(int i = 0; i < clusterPrefabs.Count(); i++) {

            rotationIdsInstantiatedCount[clusters[i].symmetricalRotationId] += 1;

            float count = rotationIdsInstantiatedCount[clusters[i].symmetricalRotationId];
            float total = rotationIdsCount[clusters[i].symmetricalRotationId];
            
            float ratio = clusters[i].symmetricalRotationId > 0 ? count / total : 1;

            ProjectileCluster last = lastOfEachIdInstantiated[clusters[i].symmetricalRotationId];

            Quaternion initial = last.transform.rotation;

            Quaternion final = Quaternion.AngleAxis(clusters[i].clusterAngularVelocity, Vector3.forward)
                * last.transform.rotation;

            

            /*Debug.Log(clusters[i].symmetricalRotationId);
            Debug.Log(initial.z);
            Debug.Log(final.z);
            Debug.Log(Quaternion.Slerp(initial, final, ratio).z);
            Debug.Log(Quaternion.Angle(initial, final));
            Debug.Log("");*/


            /*clusters[i].transform.rotation =
                Quaternion.AngleAxis(clusters[i].clusterAngularVelocity, Vector3.forward)
                * lastClusterRotations[i];*/

            clusters[i].transform.rotation = Quaternion.Slerp(initial, final, ratio);
        }

        for(int i = 0; i < clusterPrefabs.Count(); i++) {
            float count = rotationIdsInstantiatedCount[clusters[i].symmetricalRotationId];
            float total = rotationIdsCount[clusters[i].symmetricalRotationId];

            if(count == total) {
                lastOfEachIdInstantiated[clusters[i].symmetricalRotationId] = clusters[i];
            }
        }

        /*if(transform.childCount == 0) {
            Destroy(gameObject);
        }*/
    }
}