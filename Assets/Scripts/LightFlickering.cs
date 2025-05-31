using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.GlobalIllumination;

public class LightFlickering : MonoBehaviour
{
    public Light LanternLight;
    void Start()
    {
        // Crear la secuencia solo una vez
        Sequence flickerSequence = DOTween.Sequence();

        flickerSequence.Append(LanternLight.DOIntensity(2.0f, 1.0f))
            .Append(LanternLight.DOIntensity(0.0f, 0.1f))
            .Append(LanternLight.DOIntensity(1.0f, 0.5f))
            .Append(LanternLight.DOIntensity(0.0f, 0.01f).SetLoops(2, LoopType.Yoyo))
            .SetLoops(-1); // Bucle infinito
    }

    // Update is called once per frame
    void Update()
    {
         
    }

 
}
