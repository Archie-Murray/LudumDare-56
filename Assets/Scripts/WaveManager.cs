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
        Nannies[currentWave].OnWaveComplete += () => button.interactable = true;
        currentWave++;
        button.interactable = false;
    }
    private void LateUpdate()
    {
        if (currentWave == Nannies.Length && Nannies[currentWave].transform.childCount == 0)
        {
            win();
        }
    }
    void win()
    {
        confety.Play(true);
        audioSource.Play();
    }

}
