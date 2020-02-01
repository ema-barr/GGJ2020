using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 changeMovement;
    private Rigidbody2D myRigidbody;


    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float delayKillBullet = 1f;



    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody2D>();

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
                transform.position + changeMovement * speed * Time.deltaTime);

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
    if (other.CompareTag("Shield"))
    {
      print("Nulla");
      Vector2 forceVec = -myRigidbody.velocity.normalized * speed;
      myRigidbody.AddForce(forceVec, ForceMode2D.Impulse);
    }
  }



}
