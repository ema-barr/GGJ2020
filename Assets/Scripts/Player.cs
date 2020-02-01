using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Vector3 changeMovement;
  private Rigidbody2D myRigidbody;

  private bool updatableShield;
  private bool isRepairing;

  [SerializeField]
  private FloatValue playerHealth;
  [SerializeField]
  private Signal playerHealthSignal;
  [SerializeField]
  private Signal shieldHealthSignal;

  [SerializeField]
  private FloatValue shieldHealth;

  [SerializeField]
  private GameObject shield;


  [SerializeField]
  private float delayStartParry;
  [SerializeField]
  private float durationParry;
  [SerializeField]
  private float recoveryParry;

  public bool isParrying;
  public bool parryReady;

  [SerializeField]
  private float speed = 0f;


  // Start is called before the first frame update
  void Start()
  {
    playerHealth.currentValue = playerHealth.initialValue;
    playerHealthSignal.Raise();

    myRigidbody = this.GetComponent<Rigidbody2D>();
    updatableShield = true;
    isParrying = false;
    parryReady = true;
  }

  // Update is called once per frame
  void Update()
  {
    GetMovement();
    if (changeMovement != Vector3.zero)
    {
      Move();
    }

    if (Input.GetKey("z") && parryReady)
    {
      StartCoroutine("ParryCo");
    }

  }


  private IEnumerator ParryCo()
  {
    print("Parrying");
    if (!isParrying)
    {
      parryReady = false;
      yield return new WaitForSeconds(delayStartParry);
      print("Start parry");
      isParrying = true;
      yield return new WaitForSeconds(durationParry);
      isParrying = false;
      print("End parry");
      yield return new WaitForSeconds(recoveryParry);
      parryReady = true;

    }

  }


  private void GetMovement()
  {
    changeMovement = Vector3.zero;
    //changeMovement.x = Input.GetAxisRaw("Horizontal");
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


  /*private IEnumerator ToggleShieldCo()
  {
    if (Input.GetKey("z") && updatableShield)
    {
      updatableShield = false;
      shield.SetActive(!shield.activeSelf);
      isRepairing = shield.activeSelf;
      yield return new WaitForSeconds(0.25f);
      updatableShield = true;
    }
  }*/

  public void TakeDamage(float damage)
  {
    if (!parryReady)
    {
      //I'm parrying or my shield is broken
      if (!isParrying)
      {
        //My parry is not good
        playerHealth.currentValue -= damage;
        playerHealthSignal.Raise();
      }
    }
    else
    {
      //I'm not parrying
      if (shieldHealth.currentValue <= 0)
      {
        //I have no shield
        playerHealth.currentValue -= damage;
        playerHealthSignal.Raise();
        //CheckDeath();
      }
      else if (shieldHealth.currentValue > 0)
      {
        //I'm defending only with my shield
        shieldHealth.currentValue -= damage;
        shieldHealthSignal.Raise();
        if (shieldHealth.currentValue <= 0)
        {
          updatableShield = false;
          shield.SetActive(false);
          parryReady = false;
        }
      }

    }

    print("Shield: " + shieldHealth.currentValue);
    print("Health: " + playerHealth.currentValue);

  }

}
