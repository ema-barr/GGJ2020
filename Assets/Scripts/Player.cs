using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
  private Sprite shieldParry;
    [SerializeField]
    private Sprite shieldParryBroken;
    [SerializeField]
    private Sprite shieldParryDestroyed;

//there's no art for this
    [SerializeField]
    private Sprite shieldCharging;
    //there's no art for this
    [SerializeField]
    private Sprite shieldChargingBroken;
    //there's no art for this
    [SerializeField]
    private Sprite shieldChargingDestroyed;

    [SerializeField]
    private Sprite shieldRebound;
    [SerializeField]
    private Sprite shieldReboundBroken;
    [SerializeField]
    private Sprite shieldReboundDestroyed;
    [SerializeField]
    private Sprite shieldRest;
    [SerializeField]
    private Sprite shieldRestBroken;
    [SerializeField]
    private Sprite shieldRestDestroyed;


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

  private SpriteRenderer shieldSprite;


  // Start is called before the first frame update
  void Start()
  {
    playerHealth.currentValue = playerHealth.initialValue;
    playerHealthSignal.Raise();

    myRigidbody = this.GetComponent<Rigidbody2D>();

    shieldSprite = GetComponentInChildren<SpriteRenderer>();

        updatableShield = true;
    isParrying = false;
    parryReady = true;
        ChangeShieldSprite(shieldRest);
    }

  // Update is called once per frame
  void Update()
  {
    GetMovement();
    if (changeMovement != Vector3.zero)
    {
      Move();
    }

    if (Input.GetKey("space") && parryReady)
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
            //Rebound (should be "Charging", but ain't got no art for that) shield sprite che ho commentato perche sti cazzi
//            ChangeShieldSprite(shieldRebound);

            yield return new WaitForSeconds(delayStartParry);
      print("Start parry");
      isParrying = true;
            //Parrying shield sprite
            ChangeShieldSprite(shieldParry);

      yield return new WaitForSeconds(durationParry);
      isParrying = false;
            //Rebound shield sprite che ho commentato perche sti cazzi
//            ChangeShieldSprite(shieldRebound);

            print("End parry");
      yield return new WaitForSeconds(recoveryParry);
      parryReady = true;
            //resting shield sprite
            ChangeShieldSprite(shieldRest);

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
        if (playerHealth.currentValue <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

  }

    public void ChangeShieldSprite(Sprite sprite)
    {
        shieldSprite.sprite = sprite;
        if (sprite == shieldRest || sprite == shieldRestBroken || sprite == shieldRestDestroyed)
        {
            shieldSprite.sortingOrder = -1;
        }
        if (sprite == shieldParry || sprite == shieldRebound || sprite == shieldParryBroken || sprite == shieldParryDestroyed || sprite == shieldReboundBroken || sprite == shieldReboundDestroyed)
        {
            shieldSprite.sortingOrder = 1;
        }


    }
}
