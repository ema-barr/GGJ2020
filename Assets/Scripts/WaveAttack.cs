using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack : MonoBehaviour
{

  [SerializeField]
  private List<GameObject> enemies;

  [SerializeField]
  private float timeDelay;

  private bool attackAvailable;


  // Start is called before the first frame update
  void Start()
    {
    attackAvailable = true;
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
    int nEnemies = enemies.Count;
    int index = Random.Range(0, nEnemies);
    enemies[index].GetComponent<EnemyScript>().Attack();
    yield return new WaitForSeconds(timeDelay * Time.deltaTime);
    attackAvailable = true;
  }
}
