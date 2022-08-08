using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        UpdateItems();
        UpdateHealth();
    }

    void UpdateHealth()
    {
        if (health != null)
        {
            health.text = string.Format("Health: {0}", healthValue);
        }
    }

    void UpdateItems()
    {
        if (items != null)
        {
            items.text = string.Format("Items:  {0}", itemsValue);
        }
    }

}