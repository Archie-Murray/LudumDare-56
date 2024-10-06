using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour {
    public AudioMixer mixer;

    [SerializeField] private Slider master;
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider sfx;

    public void Start() {
        master.onValueChanged.AddListener((float sliderReadout) => SetMasterVolume(sliderReadout));
        bgm.onValueChanged.AddListener((float sliderReadout) => SetBGMVolume(sliderReadout));
        sfx.onValueChanged.AddListener((float sliderReadout) => SetSFXVolume(sliderReadout));
    }

    public void SetMasterVolume(float sliderReadout) {
        mixer.SetFloat("MasterVolume", 20f * Mathf.Log10(sliderReadout));
    }

    public void SetBGMVolume(float sliderReadout) {
        mixer.SetFloat("BGMVolume", 20f * Mathf.Log10(sliderReadout));
    }

    public void SetSFXVolume(float sliderReadout) {
        mixer.SetFloat("SFXVolume", 20f * Mathf.Log10(sliderReadout));
    }
}