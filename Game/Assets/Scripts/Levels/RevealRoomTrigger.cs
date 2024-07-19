using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RevealRoomTrigger : MonoBehaviour {
    
    public float timeToFade = 2;

    void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("PlayerCollider")) {
            StartCoroutine(FadeOutFOW());
            StartCoroutine(FadeOutDarkness());
        }
    }

    IEnumerator FadeOutDarkness() {
        RoomTriggerBase parent = GetComponentInParent<RoomTriggerBase>();
        float timer = Time.time;

        List<SpriteRenderer> spriteRenderers = new();

        foreach(FOWDarkness child in parent.GetComponentsInChildren<FOWDarkness>()) {
            child.stopped = true;
            foreach(SpriteRenderer rend in child.GetComponentsInChildren<SpriteRenderer>()) {
                spriteRenderers.Add(rend);
            }
        }

        List<float> initialAlphas = new();

        foreach(SpriteRenderer rend in spriteRenderers) {
            initialAlphas.Add(rend.color.a);
        }

        while(Time.time - timer > timeToFade) {

            int i = 0;
            foreach(SpriteRenderer rend in spriteRenderers) {
                rend.color = new (
                    rend.color.r,
                    rend.color.g,
                    rend.color.b,
                    initialAlphas[i]*(Time.time - timer)/timeToFade
                );
                i++;
            }

            yield return null;
        }

        foreach(FOWDarkness child in parent.GetComponentsInChildren<FOWDarkness>()) {
            child.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeOutFOW() {

        RoomTriggerBase parent = GetComponentInParent<RoomTriggerBase>();

        List<ParticleSystem> particlesList = parent.fow.GetComponentsInChildren<ParticleSystem>().ToList();
        List<float> initialAlphas = new();

        foreach(ParticleSystem particles in particlesList) {
            initialAlphas.Add(particles.main.startColor.color.a*255);
        }

        float timeToFade = 2;
            
        for(int i = 0; i < particlesList.Count(); i++) {
            ParticleSystem.MainModule main = particlesList[i].main;

            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[main.maxParticles];
            particlesList[i].GetParticles(particles);

            for(int j = 0; j < particles.Count(); j++) {
                particles[j].remainingLifetime = timeToFade;
            }

            particlesList[i].SetParticles(particles, particles.Length, 0);
            particlesList[i].Stop(true, stopBehavior: ParticleSystemStopBehavior.StopEmitting);
            


            ParticleSystem.ColorOverLifetimeModule colLifetime = particlesList[i].colorOverLifetime;
            Color initial = main.startColor.color;
            Color final = new Color (
                initial.r,
                initial.g,
                initial.b
            );

            colLifetime.enabled = true;
            Gradient gradient = new();
            GradientColorKey[] colorKeys = new GradientColorKey[2];
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            colorKeys[0] = new(initial, 0);
            colorKeys[1] = new(final, 1);
            float percent = particlesList[i].time / (particlesList[i].time + timeToFade);
            alphaKeys[0] = new(1, percent);
            alphaKeys[1] = new(0, 1);
            gradient.colorKeys = colorKeys;
            gradient.alphaKeys = alphaKeys;
            colLifetime.color = gradient;
        }

        yield return new WaitForSeconds(timeToFade);
        parent.fow.gameObject.SetActive(false);
        yield return null;
    }
}
