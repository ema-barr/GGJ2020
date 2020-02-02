using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Enemy
{

  // Start is called before the first frame update
  void Start()
  {
    Debug.Log(exitPoint.transform.position);
  }

  // Update is called once per frame
  void Update()
  {
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


}
