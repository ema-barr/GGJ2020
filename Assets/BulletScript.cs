using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
  private Vector3 changeMovement;
  private Rigidbody2D myRigidbody;

  [SerializeField]
  private int damage = 1;

  private int direction;


  [SerializeField]
  private float speed = 1f;
  [SerializeField]
  private float delayKillBullet = 1f;



  // Start is called before the first frame update
  void Start()
  {
    myRigidbody = this.GetComponent<Rigidbody2D>();
    direction = 1;


    Destroy(gameObject, delayKillBullet);
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    GetMovement();
    if (changeMovement != Vector3.zero)
    {
      Move();
    }


  }

  private void GetMovement()
  {
    changeMovement = Vector3.zero;
    changeMovement.x = 1; // Input.GetAxisRaw("Horizontal");
    changeMovement.y = 0; // Input.GetAxisRaw("Vertical");
  }

  private void Move()
  {
    changeMovement.Normalize();
    myRigidbody.MovePosition(
            transform.position + changeMovement * speed * direction * Time.deltaTime);

    //Play Animation
    changeMovement.x = Mathf.Round(changeMovement.x);
    changeMovement.y = Mathf.Round(changeMovement.y);
    //animator.SetFloat("moveX", changeMovement.x);
    //animator.SetFloat("moveY", changeMovement.y);
    //animator.SetBool("isMoving", true);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      other.GetComponentInParent<Player>().TakeDamage(1);
      Destroy(this.gameObject);
    }
    else if (other.CompareTag("Enemy"))
    {
      other.GetComponent<EnemyScript>().TakeDamage(1);
      Destroy(this.gameObject);
    }
    else if (other.CompareTag("Shield"))
    {
      if (other.GetComponentInParent<Player>().isParrying)
        direction = -1;
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
