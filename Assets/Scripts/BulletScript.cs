using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
  private Vector3 changeMovement;
  private Rigidbody2D myRigidbody;
  private Vector2 targetPoint = Vector2.zero;

  [SerializeField]
  private int damage = 1;



  [SerializeField]
  private float speed = 1f;
  [SerializeField]
  private float delayKillBullet = 1f;

  public GameObject target = null;

  private Vector3 direction;



  // Start is called before the first frame update
  void Start()
  {
    myRigidbody = this.GetComponent<Rigidbody2D>();

    //DO SHIT HERE, FOR FUCK SAKE
    //damage = 
    Destroy(gameObject, delayKillBullet);
  }

  // Update is called once per frame
  void Update()
  {
    if (target != null)
    {
      MoveToTarget();
    }



  }

  public void SetTarget(GameObject targetObj)
  {
    target = targetObj;
    direction = target.transform.position - transform.position;
  }

  private void MoveToTarget()
  {
    myRigidbody.velocity = direction.normalized * speed;

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
        transform.rotation = Quaternion.AngleAxis(0, Vector3.back); ;
    if (other.CompareTag("Player"))
    {
      other.GetComponentInParent<Player>().TakeDamage(1);
      Destroy(this.gameObject);
    }
    else if (other.CompareTag("Enemy"))
    {
      if (other.GetComponent<EnemyScript>() != null)
      {
        other.GetComponent<EnemyScript>().TakeDamage(1);
      }
      else if (other.GetComponent<Crossbowman>())
      {
        other.GetComponent<Crossbowman>().TakeDamage(1);
      }
      Destroy(this.gameObject);
    }
    else if (other.CompareTag("Shield"))
    {
      if (other.GetComponentInParent<Player>().isParrying)
        direction = transform.position - target.transform.position;
      else
      {
        other.GetComponentInParent<Player>().TakeDamage(damage);
        Destroy(this.gameObject);

      }

    }
    else if (other.CompareTag("Villager"))
    {
      other.GetComponentInParent<VillagerScript>().TakeDamage(damage);
      Destroy(this.gameObject);
    }
  }



}
