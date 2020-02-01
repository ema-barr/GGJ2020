using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack : MonoBehaviour
{

  [SerializeField]
  private List<GameObject> spawnPoints;
  private List<GameObject> enemies;

  [SerializeField]
  private float timeDelay;
  [SerializeField]
  private GameObject enemyPrefab;

  [SerializeField]
  private int numEnemies;

  private int activeEnemies;

  [SerializeField]
  private Signal initializationEnemiesSignal;

  [SerializeField]
  private ListGameObjects listVillagers;

  private bool attackAvailable;




  // Start is called before the first frame update
  void Start()
  {
    attackAvailable = false;
    StartCoroutine("InitializationCo");
  }

  private IEnumerator InitializationCo()
  {
    activeEnemies = Mathf.Min(numEnemies, spawnPoints.Count);
    enemies = new List<GameObject>();
    for (int i = 0; i < activeEnemies; i++)
    {
      GameObject enemy = (GameObject)Instantiate(enemyPrefab, spawnPoints[i].transform);
      enemies.Add(enemy);
    }
    initializationEnemiesSignal.Raise();
    yield return null;
  }

  // Update is called once per frame
  void Update()
  {

    if (attackAvailable)
    {
      StartCoroutine("AttackCo");
    }
  }

  private IEnumerator AttackCo()
  {
    attackAvailable = false;
    activeEnemies = enemies.Count;
    int index = Random.Range(0, activeEnemies);
    print(index);
    if (listVillagers.list.Count > 0)
    {
      int indexVillAttack = Mathf.Min(index, listVillagers.list.Count - 1);
      enemies[index].GetComponent<EnemyScript>().Attack(listVillagers.list[indexVillAttack]);
      yield return new WaitForSeconds(timeDelay * Time.deltaTime);

    }
    attackAvailable = true;
  }

  public void RemoveEnemy(GameObject enemy)
  {
    enemies.Remove(enemy);
    if (enemies.Count == 0)
    {
      StopCoroutine("AttackCo");
      print("Win");
      attackAvailable = false;
    }
  }

  public void InitializationComplete()
  {
    attackAvailable = true;
  }
}
