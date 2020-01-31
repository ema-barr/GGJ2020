using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Vector3 changeMovement;
  private Rigidbody myRigidbody;

  private bool updatableShield;

  [SerializeField]
  private GameObject shield;
  

  [SerializeField]
  private float speed = 0f;


  // Start is called before the first frame update
  void Start()
    {
    myRigidbody = this.GetComponent<Rigidbody>();
    updatableShield = true;
    shield.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      GetMovement();
    if (changeMovement != Vector3.zero)
    {
      Move();
    }

    CheckShield();

  }

    private void GetMovement()
  {
    changeMovement = Vector3.zero;
    changeMovement.x = Input.GetAxisRaw("Horizontal");
    changeMovement.y = Input.GetAxisRaw("Vertical");
  }

  private void CheckShield()
  { 
    StartCoroutine("ToggleShieldCo");
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


  private IEnumerator ToggleShieldCo()
  {
    if (Input.GetButton("ToggleShield") && updatableShield)
    {
      updatableShield = false;
      shield.SetActive(!shield.activeSelf);
      yield return new WaitForSeconds(0.25f);
      updatableShield = true;
    }
  }
}
