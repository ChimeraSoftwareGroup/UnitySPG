using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _firstPage;
    [SerializeField] GameObject _mainMenuPlacard;
    [SerializeField] GameObject _buttonsMainMenu;
    [SerializeField] GameObject _settingsWindow;
    [SerializeField] GameObject _textClickToStart;
    [SerializeField] GameObject _errorPage;

    public AudioSource audioSource;
    public AudioClip sound;
    public AudioClip pageSound;
    public AudioClip errorSound;

    public void ChangeFirstPageByMainMenue()
    {
        audioSource.PlayOneShot(sound);
        _firstPage.SetActive(false);
        _mainMenuPlacard.SetActive(true);
        Invoke("ShowButtonsMainMenu", 1.5f);
    }

    public void ShowButtonsMainMenu()
    {
        _textClickToStart.SetActive(false);
        _buttonsMainMenu.SetActive(true);
    } 
    public void GoToCoopMenu()
    {
        audioSource.PlayOneShot(sound);
        Invoke("CallCoopMenu", 0.2f);
    }
    public void GoToCredits()
    {
        audioSource.PlayOneShot(sound);
        SceneManager.LoadScene("Credits");
    }  
    public void GoToSettings()
    {
        audioSource.PlayOneShot(pageSound);
        Invoke("CallSettingsWindows", 0.2f);
    }

    public void CallSettingsWindows()
    {
        _settingsWindow.SetActive(true);

    }
    public void CallCoopMenu()
    {
        SceneManager.LoadScene("PotatoMenu");

    }

    public void CloseErrorPage()
    {
        _errorPage.SetActive(false);
        audioSource.PlayOneShot(pageSound);

    }

    public void ShowErrorPage()
    {
        audioSource.PlayOneShot(errorSound);
        _errorPage.SetActive(true);
    }
}
