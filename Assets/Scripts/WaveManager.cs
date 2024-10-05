using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Nanny[] Nannies;
    [SerializeField] Button button;
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

}
