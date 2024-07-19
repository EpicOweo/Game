using UnityEngine;
public class ProjectileParticle : MonoBehaviour {

    protected Projectile proj;
    protected ParticleSystem particles;

    void Awake() {
        proj = GetComponentInParent<Projectile>();
        particles = GetComponent<ParticleSystem>();
    }

    void Update() {
        HandleParticles();
    }

    protected virtual void HandleParticles() {}
}