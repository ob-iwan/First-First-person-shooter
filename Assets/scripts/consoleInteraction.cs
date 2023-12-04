using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class consoleInteraction : MonoBehaviour
{
    public Camera cam;

    private Ray ray;
    private RaycastHit hit;

    private securityConsole console;

    private void Awake()
    {
        console = GameObject.FindGameObjectWithTag("console").GetComponent<securityConsole>();
    }
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("console"))
                {
                    console.onClicked();
                }
            }
        }
    }
}
