using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{
  [SerializeField]
  private Text counter;

  public void UpdateCounter(int n)
  {
    counter.text = "x " + n;
  }
}
