using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundUI : BaseUI
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider sfxSlider;

    protected override void Awake()
    {
        base.Awake();
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        gameObject.SetActive(true);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        gameObject.SetActive(false);
    }

    public void SetMasterVolume(float input)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(input) * 20);
    }
    public void SetBGMVolume(float input)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(input) * 20);
    }
    public void SetSFXVolume(float input)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(input) * 20);
    }

}
