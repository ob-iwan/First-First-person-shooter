using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSystem : MonoBehaviour
{
    public void loadGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void loadStory()
    {
        SceneManager.LoadScene("Story");
    }

    public void loadHowTo()
    {
        SceneManager.LoadScene("howToPlay");
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void loadDead()
    {
        SceneManager.LoadScene("dead");
    }

    public void loadEnd()
    {
        SceneManager.LoadScene("theEnd");
    }

    public void loadSurprise()
    {
        SceneManager.LoadScene("surprise");
    }
}
