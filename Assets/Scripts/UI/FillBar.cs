using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
  public Image bar;
  public FloatValue value;

  private void Start()
  {
    InitBar();
  }

  public void InitBar()
  {
    float percCurrBar = value.currentValue / value.initialValue;
    bar.fillAmount = percCurrBar;
  }

  public void UpdateBar()
  {
    float percCurrBar = value.currentValue / value.initialValue;
    bar.fillAmount = percCurrBar;
  }
}
