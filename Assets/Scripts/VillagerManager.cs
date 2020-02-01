using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
  [SerializeField]
  private List<GameObject> spawnPoints;
  private List<GameObject> villagers;

  [SerializeField]
  private int numVillagers;
  [SerializeField]
  private GameObject villagerPrefab;

  private int activeVillagers;


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
    villagers = new List<GameObject>();
    for (int i = 0; i < activeVillagers; i++)
    {
      GameObject enemy = (GameObject)Instantiate(villagerPrefab, spawnPoints[i].transform);
      villagers.Add(enemy);
    }
    yield return null;
  }
}
