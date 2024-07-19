using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuadCannon : Enemy {

    public Vector2 initialDir;

    public float timeBetweenShots;
    public float shotPauseTime = 0.3f;

    public float degreesPerShot = 0;

    public float timer = 0;

    public Projectile bulletPrefab;

    List<Projectile> pooledBullets = new();
    int amountToPool = 20;

    protected override void Start() {
        base.Start();

        Projectile tmp;
        for(int i = 0; i < amountToPool; i++) {
            tmp = Instantiate(bulletPrefab);
            tmp.gameObject.SetActive(false);
            tmp.transform.SetParent(transform);
            pooledBullets.Add(tmp);
        }

        StartCoroutine(Attack());
    }

    public Projectile GetPooledObject() {
        for(int i = 0; i < amountToPool; i++) {
            if(!pooledBullets[i].gameObject.activeSelf) {
                return pooledBullets[i];
            }
        }

        return null;
    }

    public IEnumerator Attack() {

        List<Vector2> dirs = new() {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        while(true) {
            Vector3 initial = transform.localEulerAngles;
            while(timer < timeBetweenShots) {
                transform.localEulerAngles = Vector3.Lerp(
                    initial,
                    initial+Vector3.forward*degreesPerShot,
                    timer / timeBetweenShots
                );
                timer += Time.deltaTime;
                yield return null;
            }
            transform.localEulerAngles = initial+Vector3.forward*degreesPerShot;

            yield return new WaitForSeconds(shotPauseTime);

            foreach(Vector2 dir in dirs) {
                Projectile proj = GetPooledObject();
                if(proj != null){
                    proj.gameObject.SetActive(true);
                    proj.transform.SetParent(Level.instance.projectiles);
                    proj.initialDirection = transform.rotation*dir;
                    proj.transform.position = transform.position + transform.rotation*(Vector3)dir;
                    proj.rb.velocity = Vector2.zero;
                    proj.time = 0;
                }
            }

            yield return new WaitForSeconds(shotPauseTime);
            
            timer = 0;
            
        }
    }
    
}