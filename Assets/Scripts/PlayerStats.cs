using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int healthValue = 100;
    public int itemsValue = 0;
    public TMP_Text health;
    public TMP_Text items;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        health.text = "Health: "+ healthValue;
        items.text =  "Coconuts: " + itemsValue;
    }


    /*
    public void IncrementCoconuts() {
        itemsValue++;
        UpdateItems();
    }
    */

}
