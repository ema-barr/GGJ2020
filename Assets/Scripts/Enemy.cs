using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField]
  private AudioClip arrow;

  [SerializeField]
  private List<AudioClip> damageSounds;

  public GameObject bullet;
  [SerializeField]
  protected GameObject exitPoint;
  [SerializeField]
  protected int health;
  [SerializeField]
  protected float cooldownAttack = 3f;

  [SerializeField]
  protected float timeBeforeDeath = 0.05f;

  protected bool canAttack = true;

  private GameObject targetObj = null;

  private bool deathStarted = false;



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
    AudioSource audioSource = GetComponent<AudioSource>();
    audioSource.clip = arrow;
    audioSource.Play();

    print("Target" + targetObj);
    GameObject bullObj = (GameObject)Instantiate(bullet, exitPoint.transform);
    bullObj.GetComponent<BulletScript>().SetTarget(targetObj);
    yield return new WaitForSeconds(cooldownAttack);
    canAttack = true;
  }

  public void TakeDamage(int damage)
  {
    print("Enemy: " + health);

    int indexSound = Random.Range(0, damageSounds.Count);
    print(damageSounds[indexSound]);
    AudioSource audioSource = GetComponent<AudioSource>();
    audioSource.clip = damageSounds[indexSound];
    audioSource.Play();

    health -= damage;
    if (health <= 0)
    {
      if (!deathStarted)
      {
        deathStarted = true;
        StartCoroutine("DeathCo");
      }


    }
  }

  private IEnumerator DeathCo()
  {
    yield return new WaitForSeconds(timeBeforeDeath);
    GetComponentInParent<WaveAttack>().RemoveEnemy(this.gameObject);
    Destroy(this.gameObject);
  }

}
