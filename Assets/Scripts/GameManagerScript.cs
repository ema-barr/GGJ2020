using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
  private bool initializationEnemies;
  private bool initializationVillagers;

  [SerializeField]
  private Signal initializationComplete;

  // Start is called before the first frame update
  void Awake()
  {
    initializationEnemies = false;
    initializationVillagers = false;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void OnInitializationEnemiesSignal()
  {
    initializationEnemies = true;
    if (initializationVillagers)
    {
      initializationComplete.Raise();
    }
  }

  public void OnInitializationVillagersSignal()
  {
    initializationVillagers = true;
    if (initializationEnemies)
    {
      initializationComplete.Raise();
    }
  }

  public void OnGameOverSignal()
  {

  }

  public void OnWinSignal()
  {

  }
}
