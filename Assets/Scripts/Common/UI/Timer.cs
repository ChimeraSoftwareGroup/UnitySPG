using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float _gameTimer;
    [SerializeField] Text _scoreText;

    public bool isFinish; 
    private bool _isRunning = false;
    private float _secondsLeft;

    // Update is called once per frame
    void Update()
    {
        if (_isRunning && _secondsLeft > 0)
        {
            IncreaseTimer();
        }
        else if (_secondsLeft <= 0 && _isRunning) {
            GameManager.instance.isMiniGameFinished = true;
           // _scoreText.text = "0:00";
           // _secondsLeft = 20;
        }
      
        //if (_isrunning && !_isgamefinished)
        //{
        //    increasetimer();
        //}


    }

    public void StartTimer()
    {
        _isRunning = true;
        _secondsLeft = _gameTimer;
    }

    public void StopTimer()
    {
        _isRunning = true;
        _secondsLeft = _gameTimer;
    }

    public void SetTimer(float timer)
    {
        
        _gameTimer = timer;
    }

    public float GetTimer()
    {
        return _gameTimer;
    }
    private void IncreaseTimer()
    {
        //incrementation seconds
        //seconds += Time.deltaTime;
        //decompte
        _secondsLeft -= Time.deltaTime;

        float minute = Mathf.FloorToInt(_secondsLeft / 60);
        float sec = Mathf.FloorToInt(_secondsLeft % 60);
        if (sec < 10)
        {
            _scoreText.text = minute + " :0" + sec.ToString();
        }
        else
        {
            _scoreText.text = minute + " : " + sec.ToString();
        }
    }

    private string DisplayTime()
    {
        float minute = Mathf.FloorToInt(_secondsLeft / 60);
        float sec = Mathf.FloorToInt(_secondsLeft % 60);
        return string.Format("{0:00}:{1:00}", minute, sec);
    }

   
   


}
