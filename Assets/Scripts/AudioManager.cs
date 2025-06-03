using UnityEngine;


public enum SoundType
{
    PASOS,
    PASOS_ENEMY,
    PUERTA,
    ENEMY,
    LINTERNA, 
    AMBIENTE,
    RECOLECTAR_ITEM
    //AUDIO CLIPS QUE QUIERAS
}


[RequireComponent (typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Tooltip("Esta lista tiene que ser llenada en orden de acuerdo con el enum de arriba en la clase AudioManager")]
    [SerializeField] private AudioClip[] soundList;

    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioSource soundSourcePoint;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public static void PlaySFX(SoundType clip, float volume = 1f)
    {   
        instance.audioSource.PlayOneShot(instance.soundList[(int)clip], volume);
    }

    public static void PlaySFXRandom(SoundType clip, float minValue, float maxValue, float volume = 1f)
    {
        float random = Random.Range(minValue,maxValue);
        instance.audioSource.pitch = random;
        PlaySFX(clip, volume);
        ResetPitch();
    }
    
    public void PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1f)
    {
        AudioSource audioSource = Instantiate(soundSourcePoint, position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        //DESTROY
    }

    private static void ResetPitch()
    {
        instance.audioSource.pitch = 1;
    }

    public static void StopSFX()
    {
        instance.audioSource.Stop();
    }
    
}
