using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip pageSound;

    public AudioMixer audioMixer;
    [SerializeField] GameObject _settingsWindow;
  public void SetVolume(float music)
    {
        audioMixer.SetFloat("MusicVolume", music);
    }

    public void SetSound(float sound)
    {
        audioMixer.SetFloat("SoundVolume", sound);
    }
    public void CloseSettingsWindow()
    {
        audioSource.PlayOneShot(pageSound);
        Invoke("CallSettingsWindows", 0.2f);

    }

    public void CallSettingsWindows()
    {
        _settingsWindow.SetActive(false);

    }
}
