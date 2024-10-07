using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Utilities;

public class WaveManager : MonoBehaviour {
    [SerializeField] public Nanny[] Nannies;
    [SerializeField] Button button;
    [SerializeField] GameObject confetti;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private BGMEmitter emitter;
    [SerializeField] private Image fade;
    public int currentWave;

    // Update on wave state change
    private void Start() {
        button.onClick.AddListener(() => OnWaveStateChange());
    }
    void OnWaveStateChange() {
        emitter.PlayBGM(BGMType.Wave);
        Nannies[currentWave].StartWave();
        if (currentWave < Nannies.Length - 1) { // Do not reset button for last wave
            Nannies[currentWave].OnWaveComplete += () => {
                button.interactable = true;
                emitter.PlayBGM(BGMType.Building);
            };
        } else {
            Nannies[currentWave].OnWaveComplete += Win;
        }
        currentWave++;
        button.interactable = false;
    }
    public void Win() {
        emitter.PlayBGM(BGMType.Building);
        audioSource.Play();
        StartCoroutine(MainMenuTransition());
    }

    private IEnumerator MainMenuTransition() {
        yield return Yielders.WaitForSeconds(0.5f);
        Instantiate(confetti, new Vector3(13f, -1f, 0f), Quaternion.identity);
        yield return Yielders.WaitForSeconds(1f);
        float timer = 0f;
        while (timer < 1f) {
            timer = Mathf.Min(1f, timer + Time.fixedDeltaTime);
            fade.color = Color.Lerp(Color.clear, Color.black, timer);
            yield return Yielders.WaitForFixedUpdate;
        }
        SceneManager.LoadScene(0);
    }
}