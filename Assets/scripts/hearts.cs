using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hearts : MonoBehaviour
{
    public int whichHeart;

    public playerDeath player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (whichHeart == 0 && player.health <= 3)
        {
            Destroy(gameObject);
        }
        if (whichHeart == 1 && player.health <= 2)
        {
            Destroy(gameObject);
        }
        if (whichHeart == 2 && player.health <= 1)
        {
            Destroy(gameObject);
        }
    }
}
