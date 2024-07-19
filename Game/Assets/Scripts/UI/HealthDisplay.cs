using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {
    public Health health;

    public Image healthBar;
    float initialBarLength;

    public Image damageBar;

    void Awake() {
        initialBarLength = healthBar.rectTransform.sizeDelta.x;

        Player.newPlayerCreated.AddListener(() => {
            health = Player.instance.health;
            health.onDamage.AddListener(UpdateHealthBar);
        });
    }

    void UpdateHealthBar() {
        float healthPercentage = (float)health.currentHealth / health.maxHealth;
        float newBarLength = initialBarLength * healthPercentage;

        healthBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newBarLength);

        StartCoroutine(ShowDamageBar(newBarLength));
    }

    IEnumerator ShowDamageBar(float newBarLength) {
        yield return new WaitForSeconds(0.75f);
        damageBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newBarLength);
    }
}