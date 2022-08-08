using UnityEngine;
using System.Collections.Generic;

public class ItemPickupController : MonoBehaviour {
    [SerializeField]
    private PlayerStats playerStats;

    private AudioSource pickupItemAudioSrc;

    private void Awake() {

        if (!pickupItemAudioSrc) {
            List<AudioSource> audios = new List<AudioSource>();
            GetComponents<AudioSource>(audios);
            pickupItemAudioSrc = audios[1];
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == EntityTags.Item) {
            ItemController item = other.GetComponent<ItemController>();
            if (Vector2.Distance(transform.position, other.transform.position) < 0.5f) {
                playerStats.IncrementScore(item.getScoreAmount());
                pickupItemAudioSrc.Play();
                item.DestroyObject();
            }
        }
    }
}
