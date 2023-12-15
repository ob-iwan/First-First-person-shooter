using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class playerDeath : MonoBehaviour
{
    public sceneSystem scene;
    public int health = 4;
    public bool ded = false;

    private bool invincible = false;
    public float invincibleTime = 1.5f;
    private float invincibleReset;

    public PlayerMovement player;

    public TextMeshProUGUI scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        invincibleReset = invincibleTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            ded = true;
        }

        if (invincible)
        {
            invincibleTime -= Time.deltaTime;
        }
        if (invincibleTime < 0)
        {
            invincibleTime = invincibleReset;
            invincible = false;
        }

        if (ded)
        {
            Cursor.lockState = CursorLockMode.None;
            scene.loadDead();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("door") && player.score == 7)
        {
            Cursor.lockState = CursorLockMode.None;
            scene.loadEnd();
        }
        else if (collision.gameObject.CompareTag("door"))
        {
            scoreboard.text = $"{player.score}/7 files collected \r\n\r\n" +
                              $"" +
                              $"NOT ENOUGH FILES COLLECT THEM ALL";
        }

        if (collision.gameObject.CompareTag("enemy") && !invincible)
        {
            health--;
            invincible = true;
        }
        if (collision.gameObject.CompareTag("killable") && !invincible)
        {
            health -= 2;
            invincible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("door"))
        {
            scoreboard.text = $"{player.score}/7 files collected";
        }
    }
}
