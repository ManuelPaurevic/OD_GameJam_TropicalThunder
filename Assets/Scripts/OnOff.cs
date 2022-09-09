using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class OnOff : MonoBehaviour
{
    public GameObject panel;
    public bool flag = false;

    public void TurnOnOff(){
        if(flag){
            panel.SetActive(false);
            flag = false;
        }else{
            panel.SetActive(true);
            flag = true;
        }
    }


}
