using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
  public Image shieldHealthBar;
  public FloatValue shieldHealth;

  // Start is called before the first frame update
  void Start()
    {
    InitHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public void InitHealth()
  {
    float percCurrHealth = shieldHealth.currentValue / shieldHealth.initialValue;
    shieldHealthBar.fillAmount = percCurrHealth;
  }

  public void UpdateHealth()
  {
    float percCurrHealth = shieldHealth.currentValue / shieldHealth.initialValue;
    shieldHealthBar.fillAmount = percCurrHealth;
  }
}
