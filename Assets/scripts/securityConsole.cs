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

    public void onClicked()
    {
        selectNextCamera();
    }

    void selectNextCamera()
    {
        //switch the next cam
        var previousCamera = activeCamera;
        activeCamIndex = (activeCamIndex + 1) % securityCameras.Count;

        //tell the previous cam to stop watching
        if (previousCamera != null)
            previousCamera.stopWatching(this);

        //tell the new cam to start watching
        //and to change cam name to new one
        activeCameraLabel.text = $"camera: {activeCamera.displayName}";
        activeCamera.startWatching(this);
        cameraImage.texture = activeCamera.outputTexture;
    }

    public void onDetected(GameObject target)
    {
        Debug.Log($"Detected {target.name}");
    }

    public void onAllClear()
    {
        Debug.Log("All clear");
    }
}
