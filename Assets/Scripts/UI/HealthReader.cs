using Entity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthReader : MonoBehaviour
{
    [SerializeField] TMP_Text HPText;
    [SerializeField] Health health;


    // Update is called once per frame
    void LateUpdate()
    {
        HPText.text = $"HP: {health.getCurrentHealth}";
    }
}
