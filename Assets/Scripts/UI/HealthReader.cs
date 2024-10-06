using Entity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthReader : MonoBehaviour
{
    [SerializeField] TMP_Text HPText;
    [SerializeField] Health health;

    [SerializeField] GameObject[] cakes;

    // Update is called once per frame
    void LateUpdate()
    {
        HPText.text = $"HP: {health.getCurrentHealth}";
        if(health.getCurrentHealth < 40)
        {
            cakes[0].SetActive(false);
        }
        if (health.getCurrentHealth < 28)
        {
            cakes[1].SetActive(false);
        }
        if (health.getCurrentHealth < 14)
        {
            cakes[2].SetActive(false);
        }
    }
}
