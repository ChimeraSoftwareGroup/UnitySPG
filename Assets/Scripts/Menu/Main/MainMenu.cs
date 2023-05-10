using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{


    [Header("Menu Elements")]
    [SerializeField] GameObject _firstPage;
    [SerializeField] GameObject _buttonsMainMenu;
    [SerializeField] GameObject _textSPG;
    [SerializeField] GameObject _buttonGameMode;
    [SerializeField] GameObject _buttonOther;
    [SerializeField] GameObject _buttonCredits;
    [SerializeField] GameObject _buttonHead;
    [SerializeField] GameObject _settingsWindow;
    [SerializeField] GameObject _textClickToStart;
    [SerializeField] GameObject _errorPage;
    [SerializeField] GameObject _legalPage;
    [SerializeField] GameObject _contactPage;
    [SerializeField] GameObject _AudioManager;
    //[SerializeField] GameObject _choiceBattleRoyale;

    [Header("Animations")]
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _brAnimation;
    [SerializeField] GameObject _coopAnimation;
    [SerializeField] GameObject _TrainingAnimation;
    [SerializeField] GameObject _explosionBr;
    [SerializeField] GameObject _explosionCoop;
    [SerializeField] GameObject _explosionTraining;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip sound;
    public AudioClip pageSound;
    public AudioClip errorSound;
    public AudioClip explosionBrSound; 
    public AudioClip slapSound;
    public AudioClip metalSound;
    public AudioClip starsIntro;

    private void Start()
    {

        audioSource.PlayOneShot(starsIntro, 0.5f);
        Invoke("ActiveMusic", 2f);
        _animator = GetComponent<Animator>();
        _animator.SetBool("isOpenContact", false);
        //StartCoroutine(SPGApi.TestApi((string res, bool isSuccess) => { Debug.Log(res); Debug.Log(isSuccess); }));
    }

    private void ActiveMusic()
    {
        _AudioManager.SetActive(true);
    }
    public void ChangeFirstPageByMainMenue()
    {
        audioSource.PlayOneShot(sound);
        _textSPG.SetActive(false);
        _firstPage.SetActive(false);
        _buttonCredits.SetActive(true);
        Invoke("ShowGameButtons", 0.5f);
        Invoke("ShowOtherButtons", 1.8f);
        Invoke("ShowHeadButton", 1.8f);

    }
    public void OnClick()
    {
        // Change la taille du bouton
        transform.localScale += new Vector3(1f, 1f, 1f);
    }
    public void ShowGameButtons()
    {
        _buttonGameMode.SetActive(true);
    }
    public void ShowOtherButtons()
    {
        _buttonOther.SetActive(true);
    }
    public void ShowHeadButton()
    {
        _buttonHead.SetActive(true);
    }

    public void ShowButtonsMainMenu()
    {
        _textClickToStart.SetActive(false);
        _buttonsMainMenu.SetActive(true);
    }
    public void GoToCoopMenu()
    {
        audioSource.PlayOneShot(sound);
        _coopAnimation.SetActive(true);
        audioSource.PlayOneShot(slapSound);

        Invoke("CoopExplosion", 0.4f);
        Invoke("CallCoopMenu", 1f);
    }

    public void CoopExplosion()
    {
        _explosionCoop.SetActive(true);

    }
    public void GoToCredits()
    {
        audioSource.PlayOneShot(sound);
        SceneManager.LoadScene("Credits");
    }
    public void GoToTraining()
    {
        audioSource.PlayOneShot(sound);
        _TrainingAnimation.SetActive(true);
        Invoke("TrainingExplosion", 0.5f);
        Invoke("CallTrainingMenu", 1.2f);
    }

    public void TrainingExplosion()
    {
        _explosionTraining.SetActive(true);
        audioSource.PlayOneShot(metalSound);
    }
    public void CallTrainingMenu()
    {
        SceneManager.LoadScene(3);

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
        OnClick();
    }

    public void ShowLegalPage()
    {
        audioSource.PlayOneShot(pageSound);
        _legalPage.SetActive(true);
    }
    public void CloseLegalPage()
    {
        audioSource.PlayOneShot(pageSound);
        _legalPage.SetActive(false);
    }
    public void ShowContactPage()
    {
        audioSource.PlayOneShot(sound);
        _contactPage.SetActive(true);
        _animator.Play("Contact");
        _animator.SetBool("isOpenContact", false);
    }
    public void CloseContactPage()
    {
        audioSource.PlayOneShot(sound);
        _contactPage.SetActive(false);
    }

    public void ShowChoiceBattleRoyal()
    {
        //audioSource.PlayOneShot(sound);
        audioSource.PlayOneShot(explosionBrSound);

        _brAnimation.SetActive(true);
        _explosionBr.SetActive(true);
        Invoke("SceneBR", 1f);

    }

    public void SceneBR()
    {
        SceneManager.LoadScene("BattleRoyalMenu");
       
    }

    public void TestNewGame()
    {
        SceneManager.LoadScene("SanicScene");

    }
    //public void CloseChoiceBattleRoyal()
    //{
    //    audioSource.PlayOneShot(pageSound);
    //    _choiceBattleRoyale.SetActive(false);
    //}

    public void EnterInBattleRoyaleMode()
    {
        SceneManager.LoadScene("BattleRoyalMenu");
    }

    public void ShowDocUser()
    {
        Application.OpenURL("http://www.chimera-software.s3-website.eu-west-3.amazonaws.com/");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
