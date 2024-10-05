using Entity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyReader : MonoBehaviour
{
    [SerializeField] TMP_Text HPText;
    [SerializeField] WaveManager WaveManager;

    // Update is called once per frame
    void LateUpdate()
    {
        HPText.text = $"Wave: {WaveManager.currentWave}";
    }
}
