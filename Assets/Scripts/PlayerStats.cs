using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int itemsValue = 0;
    public TMP_Text items;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        items.text =  "Coconuts: " + itemsValue;
    }

    public void IncrementCoconuts() {
        itemsValue++;
    }

    public void RemoveCoconuts(int amount) {
        itemsValue = itemsValue - amount;
    }
}
