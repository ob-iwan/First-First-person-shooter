using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class melee : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem boom;

    private Ray ray;
    private RaycastHit hit;

    public float knifeRange = 5f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag.Equals("killable") && hit.distance < knifeRange)
                {
                    boom.Play();
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
