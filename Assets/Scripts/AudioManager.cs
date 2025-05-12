using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source----------")]
    public AudioSource BGMSource;
    public AudioSource SFXsource;
    public AudioSource PlayerStepsource;
    public AudioSource EnemyStepsource;

    [Header("---------Audio Clip----------")]
    //Aqui agregamos todos los clips de Audio que queramos

    //public AudioClip BGM;
    
    public AudioClip footStep;

    public AudioClip enemyFootStep;
    
    

    private void Start()
    {
        
       // BGMSource.clip = BGM; 
        //BGMSource.Play();
    }
    public void PlaySFX(AudioSource source,AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlaySFXRandom(AudioSource source,AudioClip clip,float min,float max)
    {
        float random = Random.Range(min,max);
        source.pitch = random;
        source.PlayOneShot(clip);
        //ResetPitch();
    }

    private void ResetPitch()
    {
        SFXsource.pitch = 1;
    }

    public void StopSFX(AudioSource source)
    {
        source.Stop();
    }
    
}
