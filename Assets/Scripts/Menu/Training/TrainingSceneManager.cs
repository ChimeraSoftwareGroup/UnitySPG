using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingSceneManager : MonoBehaviour
{
    [SerializeField] GameObject _title;
    [SerializeField] GameObject _closeButton;
    [SerializeField] GameObject _brancheOne;
    [SerializeField] GameObject _brancheTwo;
    [SerializeField] GameObject _brancheThree;

    void Start()
    {

        StartCoroutine(ActiveSceneTraining());
    }

    IEnumerator ActiveSceneTraining()
    {
        _brancheOne.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        _brancheTwo.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        _brancheThree.SetActive(true); 
        yield return new WaitForSeconds(1);
        _title.SetActive(true);
        yield return new WaitForSeconds(1);
        _closeButton.SetActive(true);   
        yield break;
    }
}
