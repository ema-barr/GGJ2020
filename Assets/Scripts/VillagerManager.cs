﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
  [SerializeField]
  private List<GameObject> spawnPoints;
  [SerializeField]
  public ListGameObjects villagers;

  [SerializeField]
  private int numVillagers;
  [SerializeField]
  private GameObject villagerPrefab;

  [SerializeField]
  private ElementToRem villToRem;

  private int activeVillagers;

  [SerializeField]
  private Signal initializationSignal;


  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine("InitializationCo");

  }

  // Update is called once per frame
  void Update()
  {

  }

  private IEnumerator InitializationCo()
  {
    activeVillagers = Mathf.Min(numVillagers, spawnPoints.Count);
    for (int i = 0; i < activeVillagers; i++)
    {
      GameObject vill = (GameObject)Instantiate(villagerPrefab, spawnPoints[i].transform);
      villagers.list.Add(vill);
    }
    initializationSignal.Raise();
    yield return null;
  }

  public void RemoveVillagerFromList()
  {
    villagers.list.Remove(villToRem.go);
  }

}