using UnityEngine;
using UnityEngine.UI;

public class SoundSFXManager : MonoBehaviour
{
    static SoundSFXManager instance;
    static AudioSource audioSource;
    static SoundSFXLibrary soundSFXLibrary;
    [SerializeField] Slider soundVolumeSlider;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            soundSFXLibrary = GetComponent<SoundSFXLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        soundVolumeSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void Play(string clipname)
    {
        AudioClip clip = soundSFXLibrary.GetRandomClip(clipname);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void OnValueChanged()
    {
        SetVolume(soundVolumeSlider.value);
    }
}
