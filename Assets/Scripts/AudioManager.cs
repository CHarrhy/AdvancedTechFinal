using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private AudioSource audioSource;

    public float musicVolume = 0.6f; // Adjust the volume here (0.0f to 1.0f)

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.volume = musicVolume; // Set the volume
        audioSource.loop = true;
        audioSource.Play();

        DontDestroyOnLoad(gameObject);
    }
}
