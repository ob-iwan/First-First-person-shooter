using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class securityConsole : MonoBehaviour
{
    [SerializeField] List<securityCam> securityCameras;
    [SerializeField] RawImage cameraImage;
    [SerializeField] TextMeshProUGUI activeCameraLabel;
    public int activeCamIndex { get; private set; } = -1;
    public securityCam activeCamera => activeCamIndex < 0 ? null : securityCameras[activeCamIndex];

    // Start is called before the first frame update
    void Start()
    {
        activeCameraLabel.text = "Camere: none";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClicked()
    {
        selectNextCamera();
    }

    void selectNextCamera()
    {
        var previousCamera = activeCamera;
        activeCamIndex = (activeCamIndex + 1) % securityCameras.Count;

        if (previousCamera == null)
            previousCamera.stopWatching(this);

        activeCameraLabel.text = $"camera: {activeCamera.displayName}";

        activeCamera.startWatching(this);
        cameraImage.texture = activeCamera.outputTexture;
    }
}
