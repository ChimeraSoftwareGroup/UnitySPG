using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    public int countDownTime;
    public Text countDownDisplay;
    public bool isCountdownFinish;
    public void StartCountDown() 
    {
        gameObject.SetActive(true);
        StartCoroutine(CountDownToStart());
    }

    IEnumerator CountDownToStart()
    {
        countDownDisplay.text = "3";
        yield return new WaitForSeconds(1f);
        countDownDisplay.text = "2";
        yield return new WaitForSeconds(1f);
        countDownDisplay.text = "1";
        yield return new WaitForSeconds(1f);
        countDownDisplay.text = "GO !";
        yield return new WaitForSeconds(.2f);
        isCountdownFinish = true;
        gameObject.SetActive(false);
        yield break;
    }
}
