using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Enemy {
    
    public Vector2 dir;

    public Sprite sUp;
    public Sprite sDown;
    public Sprite sLeft;
    public Sprite sRight;
    public Sprite sDiagUR;
    public Sprite sDiagDR;
    public Sprite sDiagUL;
    public Sprite sDiagDL;

    public float timeBetweenShots;

    public Projectile bulletPrefab;


    public float shotOffset = 0;


    List<Projectile> pooledBullets = new();
    int amountToPool = 8;

    

    void Awake() {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();


        switch(dir.x) {
            case -1:
                switch(dir.y) {
                    case -1:
                        rend.sprite = sDiagDL;
                        break;
                    case 1:
                        rend.sprite = sDiagUL;
                        break;
                    default:
                        rend.sprite = sLeft;
                        break;
                }
                break;
            case 1:
                switch(dir.y) {
                    case -1:
                        rend.sprite = sDiagDR;
                        break;
                    case 1:
                        rend.sprite = sDiagUR;
                        break;
                    default:
                        rend.sprite = sRight;
                        break;
                }
                break;
            default:
                switch(dir.y) {
                    case -1:
                        rend.sprite = sDown;
                        break;
                    case 1:
                        rend.sprite = sUp;
                        break;
                    default:
                        break;
                }
                break;

        }
    }

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
        /*amountToPool++;

        Projectile tmp = Instantiate(bulletPrefab);
        tmp.gameObject.SetActive(false);
        pooledBullets.Add(tmp);
        return pooledBullets[amountToPool-1];*/
    }


    public IEnumerator Attack() {
        yield return new WaitForSeconds(shotOffset);
        while(true) {
            yield return new WaitForSeconds(timeBetweenShots);
            
            Projectile proj = GetPooledObject();
            if(proj != null){
                proj.gameObject.SetActive(true);
                proj.transform.SetParent(transform);
                proj.initialDirection = dir;
                proj.transform.position = transform.position + (Vector3)dir;
                proj.rb.velocity = Vector2.zero;
                proj.time = 0;
            }
            
        }
    }
    
}