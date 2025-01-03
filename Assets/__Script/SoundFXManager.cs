using UnityEngine;
using UnityEngine.UI;

public class SoundFXManager : MonoBehaviour
{
    static SoundFXManager instance;
    static AudioSource audioSource;
    static SoundFXLibrary soundSFXLibrary;
    [SerializeField] Slider soundVolumeSlider;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            soundSFXLibrary = GetComponent<SoundFXLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("SoundVolume");
        soundVolumeSlider.value = volume;
        SetVolume(volume);

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
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    public void OnValueChanged()
    {
        SetVolume(soundVolumeSlider.value);
    }
}