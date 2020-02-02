using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
  [SerializeField]
  private string levelName;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Restart()
  {
    SceneManager.LoadScene(levelName);
    Time.timeScale = 1f;

  }
}
