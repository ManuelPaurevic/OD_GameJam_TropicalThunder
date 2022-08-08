using UnityEngine;

public class ItemPickupController : MonoBehaviour {
    [SerializeField]
    private PlayerStats playerStats;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == EntityTags.Item) {
            ItemController item = other.GetComponent<ItemController>();
            if (Vector2.Distance(transform.position, other.transform.position) < 0.5f) {
                playerStats.IncrementCoconuts();
                item.DestroyObject();
            }
        }
    }
}
