using Unity.Cinemachine;
using UnityEngine;

public class Flas : MonoBehaviour
{
   public PlayerMovement  playerMovement;
   public CinemachineVirtualCameraBase  camera;
    void Start()
    {
        if (camera == null)
        {
            camera = FindAnyObjectByType<CinemachineVirtualCameraBase>();
        }

        if (playerMovement == null)
        {
            playerMovement = FindAnyObjectByType<PlayerMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAmplitudeAndGain();
    }

    void ChangeAmplitudeAndGain()
    {
        if (playerMovement.IsMove())
        {
            camera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = 2.0f;
            camera.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = 1.5f;
        }
        else
        {
            camera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = 1;
            camera.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = 1;
        }
    }
}
