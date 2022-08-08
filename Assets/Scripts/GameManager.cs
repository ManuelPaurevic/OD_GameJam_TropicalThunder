using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeValue = 90f;
    public TMP_Text timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       TimerRun();
    }

    void TimerRun(){
        if(timeValue > 0){
            timeValue -= Time.deltaTime;
        }else{
            timeValue = 0;
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float time){
        if(time < 0){
            time = 0;
        }else if(time > 0){
            time += 1;
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }




}
