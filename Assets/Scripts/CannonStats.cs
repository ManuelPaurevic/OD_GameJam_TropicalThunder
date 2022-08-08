using System.Collections.Generic;
using UnityEngine;

public class CannonStats : MonoBehaviour {

    private GameObject player;
    private Vector3 targetPlayer;
    [SerializeField] private float speed = 2f;
    private Vector3 direction;

    public void Initialize(PlayerStats _playerStats) {
        playerStats = _playerStats;
    }

    private PlayerStats playerStats;

    [SerializeField]
    private int CoconutsToRemoveOnHit;

    public bool aimAtPlayer = true;

    private AudioSource playerHitAudioSrc;
    private AudioSource cannonFiredAudioSrc;

    // Start is called before the first frame update
    void Start() {

        if (!playerHitAudioSrc) {
            List<AudioSource> audios = new List<AudioSource>();
            GetComponents<AudioSource>(audios);
            playerHitAudioSrc = audios[0];
            cannonFiredAudioSrc = audios[1];
            cannonFiredAudioSrc.Play();
        }
        if (aimAtPlayer) {
            player = GameObject.Find("Player");
            if (player != null) {
                targetPlayer = player.transform.position;
            }
            direction = (targetPlayer - transform.position).normalized * speed;
            Destroy(gameObject, 10f);
        }
    }

    // Update is called once per frame
    void Update() {
        if (aimAtPlayer) {
            transform.Translate(direction * Time.deltaTime);
        }

    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (!playerController.dodging) {
                playerStats.RemoveCoconuts(CoconutsToRemoveOnHit);
                playerHitAudioSrc.Play();
                Destroy(gameObject);
            }
        }
    }
}
