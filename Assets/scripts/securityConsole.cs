using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class securityConsole : MonoBehaviour
{
    [SerializeField] List<securityCam> securityCameras;
    public int activeCamIndex { get; private set; } = -1;
    public securityCam activeCamera => activeCamIndex < 0 ? null : securityCameras[activeCamIndex];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
