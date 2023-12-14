using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSystem : MonoBehaviour
{
    public void loadGame()
    {
        SceneManager.LoadScene("main Game");
    }

    public void loadStory()
    {
        SceneManager.LoadScene("Story");
    }
}
