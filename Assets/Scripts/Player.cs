﻿using System.Collections;
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
  private Signal gameOverSignal;


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

  [SerializeField]
  private List<AudioClip> damageHealthClips;
  [SerializeField]
  private List<AudioClip> damageShieldClips;
  [SerializeField]
  private AudioClip parryClip;
  [SerializeField]
  private AudioClip brokenShieldClip;

  private AudioSource audioSource;



  // Start is called before the first frame update
  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    //playerHealth.currentValue = playerHealth.initialValue;
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

  public void PlayParrySound()
  {
    audioSource.clip = parryClip;
    audioSource.Play();
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
      //I'm bad in parrying or my shield is broken
      if (!isParrying)
      {
        int index = Random.Range(0, damageHealthClips.Count);
        audioSource.clip = damageHealthClips[index];
        audioSource.Play();
        //My parry is not good
        playerHealth.currentValue -= damage;
        playerHealthSignal.Raise();
        if (playerHealth.currentValue <= 0)
        {
          gameOverSignal.Raise();
        }
      }
    }
    else
    {
      //I'm not parrying
      if (shieldHealth.currentValue <= 0)
      {
        int index = Random.Range(0, damageHealthClips.Count);
        audioSource.clip = damageHealthClips[index];
        audioSource.Play();
        //I have no shield
        playerHealth.currentValue -= damage;
        playerHealthSignal.Raise();
        if (playerHealth.currentValue <= 0)
        {
          gameOverSignal.Raise();
        }
        //CheckDeath();
      }
      else if (shieldHealth.currentValue > 0)
      {
        int index = Random.Range(0, damageShieldClips.Count);
        audioSource.clip = damageShieldClips[index];
        audioSource.Play();
        //I'm defending only with my shield
        shieldHealth.currentValue -= damage;
        shieldHealthSignal.Raise();
        if (shieldHealth.currentValue <= 0)
        {
          audioSource.clip = brokenShieldClip;
          audioSource.Play();
          updatableShield = false;
          shield.SetActive(false);
          parryReady = false;
          gameOverSignal.Raise();
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


      if (sprite == shieldRest)
      {
        if (shieldHealth.currentValue / shieldHealth.initialValue > 0.75)
        {
          shieldSprite.sprite = shieldRest;
        }
        else if (shieldHealth.currentValue / shieldHealth.initialValue > 0.30)
        {
          shieldSprite.sprite = shieldRestBroken;
        }
        else
        {
          shieldSprite.sprite = shieldRestDestroyed;
        }
      }
      else if (sprite == shieldParry)
      {
        if (shieldHealth.currentValue / shieldHealth.initialValue > 0.75)
        {
          shieldSprite.sprite = shieldParry;
        }
        else if (shieldHealth.currentValue / shieldHealth.initialValue > 0.30)
        {
          shieldSprite.sprite = shieldParryBroken;
        }
        else
        {
          shieldSprite.sprite = shieldParryDestroyed;
        }
      }
      else if (sprite == shieldRebound)
      {
        if (shieldHealth.currentValue / shieldHealth.initialValue > 0.75)
        {
          shieldSprite.sprite = shieldRebound;
        }
        else if (shieldHealth.currentValue / shieldHealth.initialValue > 0.30)
        {
          shieldSprite.sprite = shieldReboundBroken;
        }
        else
        {
          shieldSprite.sprite = shieldReboundDestroyed;
        }
      }

      else if (sprite == shieldParry) { }

      else if (sprite == shieldRebound) { }




      //        shieldSprite.sprite = sprite;
      if (sprite == shieldRest || sprite == shieldRestBroken || sprite == shieldRestDestroyed)
      {
        shieldSprite.sortingOrder = -1;
      }
      else if (sprite == shieldParry || sprite == shieldRebound || sprite == shieldParryBroken || sprite == shieldParryDestroyed || sprite == shieldReboundBroken || sprite == shieldReboundDestroyed)
      {
        shieldSprite.sortingOrder = 1;
      }



    }


  }
  public void FullHealth()
  {
    playerHealth.currentValue = playerHealth.initialValue;
    playerHealthSignal.Raise();
  }

  public void FullShield()
  {
    shieldHealth.currentValue = shieldHealth.initialValue;
    shieldHealthSignal.Raise();
  }

  public void RepairShield(float amount)
  {
    print("amount" + amount);
    shieldHealth.currentValue += amount;
    shieldHealthSignal.Raise();

  }
}