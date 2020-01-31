using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Vector3 changeMovement;
  private Rigidbody myRigidbody;

  [SerializeField]
  private GameObject shield;
  

  [SerializeField]
  private float speed = 0f;


  // Start is called before the first frame update
  void Start()
    {
    myRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      GetMovement();
    if (changeMovement != Vector3.zero)
    {
      Move();
    }

    if (Input.GetButton("ToggleShield")){
      toggleShield();
    }
  }

    private void GetMovement()
  {
    changeMovement = Vector3.zero;
    changeMovement.x = Input.GetAxisRaw("Horizontal");
    changeMovement.y = Input.GetAxisRaw("Vertical");
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

  private void toggleShield()
  {
    shield.SetActive(shield.activeSelf);
  }
}
