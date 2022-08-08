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
        items.text =  "Score: " + itemsValue;
    }

    public void IncrementScore(int amount) {
        itemsValue = itemsValue + amount;
    }

    public void RemoveScore(int amount) {
        itemsValue = itemsValue - amount;
    }
}
