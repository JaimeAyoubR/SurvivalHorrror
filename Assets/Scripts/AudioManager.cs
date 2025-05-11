using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source----------")]
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXsource;

    [Header("---------Audio Clip----------")]
    //Aqui agregamos todos los clips de Audio que queramos

    //public AudioClip BGM;
    
    public AudioClip footStep;

    //public AudioClip enemyFootStep;
    
    

    private void Start()
    {
       // BGMSource.clip = BGM; 
        //BGMSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXsource.PlayOneShot(clip);
    }

    public void PlaySFXRandom(AudioClip clip,float min,float max)
    {
        float random = Random.Range(min,max);
        SFXsource.pitch = random;
        SFXsource.PlayOneShot(clip);
        ResetPitch();
    }

    private void ResetPitch()
    {
        SFXsource.pitch = 1;
    }
    
}
