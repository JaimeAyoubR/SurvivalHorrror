using UnityEngine;
using Unity.Cinemachine;

public class FlashLight : MonoBehaviour
{
    public GameObject Light;
    public CinemachineVirtualCameraBase camera;
    public Light LightFont;
    public bool isOn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (camera == null)
        {
            camera = FindAnyObjectByType<CinemachineVirtualCameraBase>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveLight();
        PowerLight();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void MoveLight()
    {
        float X = camera.GetComponent<CinemachinePanTilt>().PanAxis.Value;
        float Y = camera.GetComponent<CinemachinePanTilt>().TiltAxis.Value;
        
        Light.transform.rotation = Quaternion.Euler(Y,X,0);
    }

    void PowerLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            LightFont.enabled = isOn;
        }
    }
}
