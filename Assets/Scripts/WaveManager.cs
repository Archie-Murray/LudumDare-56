using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {
    [SerializeField] public Nanny[] Nannies;
    [SerializeField] Button button;
    [SerializeField] GameObject confety;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private BGMEmitter emitter;
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
    void Win() {
        emitter.PlayBGM(BGMType.Building);
        confety.SetActive(true);
        audioSource.Play();
    }
}