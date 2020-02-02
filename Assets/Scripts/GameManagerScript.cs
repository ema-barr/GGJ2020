using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
  private bool initializationEnemies;
  private bool initializationVillagers;

  [SerializeField]
  private GameObject villagersManager;

  [SerializeField]
  private GameObject player;


  [SerializeField]
  private GameObject panelWin;
  [SerializeField]
  private GameObject panelGameOver;

  [SerializeField]
  private Signal initializationComplete;

  [SerializeField]
  private float secondsToStart = 0.0f;

  private bool initializationCompleteStarted = false;

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
      if (!initializationCompleteStarted)
      {
        initializationCompleteStarted = true;
        StartCoroutine("InitializationCompleteCo");
      }
    }
  }

  public void OnInitializationVillagersSignal()
  {
    initializationVillagers = true;
    if (initializationEnemies)
    {
      if (!initializationCompleteStarted)
      {
        initializationCompleteStarted = true;
        StartCoroutine("InitializationCompleteCo");
      }
    }
  }

  public void OnGameOverSignal()
  {
    //Time.timeScale = 0f;
    panelGameOver.SetActive(true);
    player.GetComponent<Player>().FullHealth();
    player.GetComponent<Player>().FullShield();

  }

  public void OnWinSignal()
  {
    Time.timeScale = 0f;
    panelWin.SetActive(true);
    float nVillagersAlive = villagersManager.GetComponent<VillagerManager>().CountVillagersAlive();
    player.GetComponent<Player>().FullHealth();
    player.GetComponent<Player>().RepairShield(nVillagersAlive);
  }

  private IEnumerator InitializationCompleteCo()
  {
    yield return new WaitForSeconds(secondsToStart);
    initializationComplete.Raise();
  }
}
