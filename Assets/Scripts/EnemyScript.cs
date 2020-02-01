using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
  public GameObject bullet;
  [SerializeField]
  private GameObject exitPoint;

  [SerializeField]
  private int health;
  [SerializeField]
  private Signal enemyHealthSignal;


  // Start is called before the first frame update
  void Start()
  {
    Debug.Log(exitPoint.transform.position);
  }

  // Update is called once per frame
  void Update()
  {
    /*if (Input.GetKeyDown("space"))
    {
        Instantiate(bullet,EnTrans);
    }*/
  }

  public void TakeDamage(int damage)
  {
    print("Enemy: " + health);
    health -= damage;
    if (health <= 0)
    {
      Death();

    }
  }

  private void Death()
  {
    GetComponentInParent<WaveAttack>().RemoveEnemy(this.gameObject);
    Destroy(this.gameObject);
  }

  public void Attack()
  {
    Instantiate(bullet, exitPoint.transform);
  }
}
