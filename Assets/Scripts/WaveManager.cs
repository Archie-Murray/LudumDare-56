using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] public Nanny[] Nannies;
    [SerializeField] Button button;
    [SerializeField] ParticleSystem confety;
    [SerializeField] AudioSource audioSource;
    public int currentWave;

    // Update on wave state change
    private void Start()
    {
        button.onClick.AddListener(() => OnWaveStateChange());
    }
    void OnWaveStateChange()
    {
        Nannies[currentWave].StartWave();
        if (currentWave < Nannies.Length - 1)
        { // Do not reset button for last wave
            Nannies[currentWave].OnWaveComplete += () => button.interactable = true;
        }
        else
        {
            Nannies[currentWave].OnWaveComplete += Win;
        }
        currentWave++;
        button.interactable = false;
    }
    void Win()
    {
        confety.Play(true);
        audioSource.Play();
    }
}
