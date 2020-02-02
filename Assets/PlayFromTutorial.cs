using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFromTutorial : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameplayTest");
    }
}
