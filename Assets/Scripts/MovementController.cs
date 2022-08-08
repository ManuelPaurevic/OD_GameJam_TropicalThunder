using UnityEngine;

public class MovementController : MonoBehaviour {
    private Vector2 movement;

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private AudioSource audioSrc;

    private void Awake() {
        if (!rb) {
            rb = GetComponent<Rigidbody2D>();
        }
        if (!audioSrc) {
            audioSrc = GetComponent<AudioSource>();
        }
    }

    void FixedUpdate() {
        // Stops movement during potential pausing of time
        if (Time.timeScale == 1f) {
            UpdateMovement();
        }
    }

    private void UpdateFootstepsAudio() {
        if (movement.SqrMagnitude() > 0) {
            if (!audioSrc.isPlaying) {
                audioSrc.Play();
            }
        } else {
            if (audioSrc.isPlaying) {
                audioSrc.Stop();
            }
        }
    }

    private void UpdateMovement() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        UpdateFootstepsAudio();
    }

    public void ChangeSpeed(float newSpeed) {
        speed = newSpeed;
    }

    public void ChangeMovement(Vector2 newMovement) {
        movement = newMovement;
    }
}
