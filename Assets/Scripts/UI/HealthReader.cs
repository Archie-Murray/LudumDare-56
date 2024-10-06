using Entity;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

using Utilities;

public class HealthReader : MonoBehaviour {
    [SerializeField] TMP_Text HPText;
    [SerializeField] Health health;

    [SerializeField] GameObject[] cakes;

    [SerializeField] private CanvasGroup loseScreen;

    private void Start() {
        health.onDamage += (_) => {
            HPText.text = $"HP: {health.getCurrentHealth}";
            if (health.getCurrentHealth < 40) {
                cakes[0].SetActive(false);
            }
            if (health.getCurrentHealth < 28) {
                cakes[1].SetActive(false);
            }
            if (health.getCurrentHealth < 14) {
                cakes[2].SetActive(false);
            }
        };
        health.onDeath += () => StartCoroutine(DeathSplash());
        HPText.text = $"HP: {health.getMaxHealth}";
    }

    private IEnumerator DeathSplash() {
        loseScreen.FadeCanvas(1f, false, this);
        yield return Yielders.WaitForSeconds(2f);
         UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}