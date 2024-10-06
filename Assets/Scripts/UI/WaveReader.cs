using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveReader : MonoBehaviour
{
    [SerializeField] TMP_Text WaveTxt;
    [SerializeField] WaveManager WaveManager;
    // Update is called once per frame
    void Update()
    {
        WaveTxt.text = $"Wave: {WaveManager.currentWave} / {WaveManager.Nannies.Length}";
    }
}
