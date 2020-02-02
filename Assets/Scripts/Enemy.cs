using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public GameObject bullet;
  [SerializeField]
  protected GameObject exitPoint;
  [SerializeField]
  protected int health;
  [SerializeField]
  protected float cooldownAttack = 3f;

  protected bool canAttack = true;

  private GameObject targetObj = null;

  public void Attack(GameObject target)
  {
    if (canAttack)
    {
      targetObj = target;
      StartCoroutine("AttackCorout");
    }
  }

  private IEnumerator AttackCorout()
  {
    canAttack = false;

    print("Target" + targetObj);
    GameObject bullObj = (GameObject)Instantiate(bullet, exitPoint.transform);
    bullObj.GetComponent<BulletScript>().SetTarget(targetObj);
    yield return new WaitForSeconds(cooldownAttack);
    canAttack = true;
  }
}
