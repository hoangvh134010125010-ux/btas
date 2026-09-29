using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    void OnEnable()
    {
        if (AudioManager.Instance != null)
        {
            if (bgmSlider != null)
            {
                bgmSlider.value = AudioManager.Instance.bgmVolume;
                bgmSlider.onValueChanged.RemoveAllListeners();
                bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
            }

            if (sfxSlider != null)
            {
                sfxSlider.value = AudioManager.Instance.sfxVolume;
                sfxSlider.onValueChanged.RemoveAllListeners();
                sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
            }
        }
    }
}