using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeValue = 90f;
    public TMP_Text timer;
    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject endMenu;

    public TMP_Text finalValue;
    public int ScoreValue;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused) {
            TimerRun();
        }
    }

    private void TimerRun(){
        if(timeValue > 0){
            timeValue -= Time.deltaTime;
        }else{
            timeValue = 0;
            GameOver();
        }

        DisplayTime(timeValue);
    }

    private void OnPause() {
        if (isPaused) {
            UnPause();
        } else {
            Pause();
        }
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

    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void UnPause(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GameOver(){
        ScoreValue = PlayerStats.itemsValue;
        endMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        finalValue.text = ScoreValue.ToString();
    }
}
