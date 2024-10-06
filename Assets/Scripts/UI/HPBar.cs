using Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] Canvas bossCanvas;
    [SerializeField] Image Bar;
    [SerializeField] Health Health;

    private void Start()
    {
        Health = bossCanvas.transform.parent.GetComponent<Health>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        bossCanvas.transform.rotation = Quaternion.identity;
        Bar.fillAmount = Health.getPercentHealth;
    }
}
