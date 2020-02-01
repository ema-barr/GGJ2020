﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerScript : MonoBehaviour
{
  [SerializeField]
  private int initialHealth = 1;

  [SerializeField]
  private Signal villagerDeath;

  [SerializeField]
  private ElementToRem villagerToRem;

  private float health = 1;

  // Start is called before the first frame update
  void Start()
  {
    InitializeHealth();
  }

  // Update is called once per frame
  void Update()
  {

  }
  public void TakeDamage(float damage)
  {
    health -= damage;
    if (health <= 0)
    {

      villagerToRem.go = this.gameObject;
      villagerDeath.Raise();
      Destroy(this.gameObject);
    }

  }

  public void InitializeHealth()
  {
    health = (float)initialHealth;
  }
}
