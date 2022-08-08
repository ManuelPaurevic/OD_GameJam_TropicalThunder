using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonStats : MonoBehaviour {

    private GameObject player;
    private Vector3 targetPlayer;
    [SerializeField] private float speed = 2f;
    private Vector3 direction;

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
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(other.gameObject);
            playerHitAudioSrc.Play();
        }
    }
}
