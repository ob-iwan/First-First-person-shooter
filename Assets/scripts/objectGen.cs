using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectGen : MonoBehaviour
{
    public GameObject objectThing;
    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject Object = Instantiate(objectThing);
        }
    }
}
