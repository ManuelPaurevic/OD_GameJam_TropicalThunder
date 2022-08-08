using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadMainMenu(){
        SceneManager.LoadScene("Title");
    }
    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }
    public void ExtiProgram(){
        Application.Quit();
    }
}
