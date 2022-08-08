using UnityEngine;

public class ItemPickupController : MonoBehaviour {
    [SerializeField]
    private PlayerStats playerStats;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == EntityTags.Item) {
            ItemController item = other.GetComponent<ItemController>();
            item.DestroyObject();
            if (playerStats) {
                //playerStats.IncrementCoconuts();
            }
        }
    }
}
