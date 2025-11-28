using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
     [Header("Audio Sources")]
    public AudioSource sfxSource;

      [Header("Audio Clips")]
      public AudioClip hiAudio;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
      void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
      public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }
}
