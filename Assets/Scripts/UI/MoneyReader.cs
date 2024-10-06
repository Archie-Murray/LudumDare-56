using Entity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyReader : MonoBehaviour
{
    [SerializeField] TMP_Text MoneyTXT;


    // Update is called once per frame
    void Update()
    {
        MoneyTXT.text = $"£{Globals.instance.money}";
    }
}
