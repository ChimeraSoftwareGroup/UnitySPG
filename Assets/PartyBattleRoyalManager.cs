using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PartyBattleRoyalManager : MonoBehaviour
{

    public bool isHosting;
    [SerializeField] GameObject _hostCanvas;
    [SerializeField] GameObject _joinCanvas;
    [SerializeField] GameObject _battleRoyaleChoice;
    [SerializeField] GameObject _errorPage;
    

    public void EnterInBattleRoyaleModeInHost()
    {
        isHosting = true;
        ChoiceRoleBattleRoyale();
    }
    public void EnterInBattleRoyaleModeInPlayer()
    {
        isHosting = false;
        ChoiceRoleBattleRoyale();
    }


    public void Update()
    {
       
    }

    public void ChoiceRoleBattleRoyale()
    {
        if (isHosting)
        {
            _battleRoyaleChoice.SetActive(false);
            _joinCanvas.SetActive(false);
            _hostCanvas.SetActive(true);

        }
        else
        {
            _battleRoyaleChoice.SetActive(false);
            _joinCanvas.SetActive(true);
            _hostCanvas.SetActive(false);
        }
    }

    public void CloseBattleRoyale()
    {
        SceneManager.LoadScene("MenueScene");
    }

    public void GoBackChoiceBattleRoyaleRole()
    {
        _battleRoyaleChoice.SetActive(true);
        _hostCanvas.SetActive(false);
        _joinCanvas.SetActive(false);
    }
    public void ShowError()
    {
        _errorPage.SetActive(true);
    }
    public void HideError()
    {
        _errorPage.SetActive(false);
    }

}
