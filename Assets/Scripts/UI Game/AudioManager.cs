//using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    [Header("Audio Clips")]
    public AudioClip bgmClip;
    public AudioClip coinSFX;
    public AudioClip hitSFX;
    public AudioClip enemyDieSFX;
    public AudioClip jumpSFX;
    [HideInInspector] public float bgmVolume = 1f;
    [HideInInspector] public float sfxVolume = 1f;
    void Awake()
    {
        if (Instance == null)
        {
           Instance = this;
           DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetBGMVolume(bgmVolume);
        SetSFXVolume(sfxVolume);

        if (bgmClip != null && bgmSource != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }
    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        if (bgmSource != null) bgmSource.volume = bgmVolume;
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
    }
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        if (sfxSource != null) sfxSource.volume = sfxVolume;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
