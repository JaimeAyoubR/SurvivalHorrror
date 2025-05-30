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

        flickerSequence.Append(LanternLight.DOIntensity(5.0f, 0.2f))
            .Append(LanternLight.DOIntensity(0.0f, 0.2f).SetLoops(2, LoopType.Yoyo))
            .SetLoops(-1); // Bucle infinito
    }

    // Update is called once per frame
    void Update()
    {
         
    }

 
}
