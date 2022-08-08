using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public AudioClip footstepsWalk;
    public AudioClip footstepsSprint;

    [SerializeField]
    private float walkSpeed = 3f;

    [SerializeField]
    private float sprintSpeed = 5f;

    [SerializeField]
    private float dodgeTime = 0.5f;

    [SerializeField]
    private float dodgeAmount = 10f;

    [SerializeField]
    private MovementController movementController;

    public GameObject playerStats;
    private PlayerStats playerStatsScript;

    [SerializeField]
    private AudioSource audioSrc;

    public bool dodging = false;

    public Vector2 facingDirection;

    private Vector2 lastMovement;

    private bool isSprinting = false;

    private void Awake() {
        if (!movementController) {
            movementController = GetComponent<MovementController>();
        }
        if (!audioSrc) {
            audioSrc = GetComponent<AudioSource>();
        }
    }

    private void Start() {
        playerStatsScript = playerStats.GetComponent<PlayerStats>();
        movementController.ChangeMovement(Vector2.zero);
        movementController.ChangeSpeed(walkSpeed);
    }

    private void UpdateFootstepsAudio(Vector2 movement) {
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

    private void OnMovement(InputValue input) {
        Vector2 movement = input.Get<Vector2>();
        lastMovement = movement;
        if (!dodging) {
            if (!Vector2.zero.Equals(movement)) {
                facingDirection = movement;
            }
            movementController.ChangeMovement(movement);
        }
        UpdateFootstepsAudio(movement);
    }

    private void OnSprint(InputValue input) {
        float sprintInput = input.Get<float>();
        if (sprintInput > 0) {
            movementController.ChangeSpeed(sprintSpeed);
            if (!isSprinting) {
                audioSrc.Stop();
                audioSrc.clip = footstepsSprint;
                audioSrc.Play();
                isSprinting = true;
            }
        } else {
            movementController.ChangeSpeed(walkSpeed);
            if (isSprinting) {
                audioSrc.Stop();
                audioSrc.clip = footstepsWalk;
                audioSrc.Play();
                isSprinting = false;
            }
        }
    }

    private void OnDodge() {
        if (!Vector2.zero.Equals(facingDirection)) {
            StartCoroutine(PerformDodge(facingDirection));
        }
    }

    private IEnumerator PerformDodge(Vector2 direction) {
        dodging = true;
        float activeDodgeTime = 0;
        movementController.ChangeMovement(direction);
        while (activeDodgeTime <= dodgeTime) {
            float dodgePercent;
            if (activeDodgeTime == 0) {
                dodgePercent = 0;
            } else if (dodgeTime == activeDodgeTime) {
                dodgePercent = 1f;
            } else {
                dodgePercent = activeDodgeTime / dodgeTime;
            }
            float dodgeSpeed = Mathf.Lerp(dodgeAmount, walkSpeed, dodgePercent);
            movementController.ChangeSpeed(dodgeSpeed);
            activeDodgeTime += Time.deltaTime;
            yield return null;
        }
        movementController.ChangeSpeed(walkSpeed);
        movementController.ChangeMovement(lastMovement);
        dodging = false;
    }

    public Vector2 GetPosition() {
        return transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Collectibles")){
            Destroy(other.gameObject);
            playerStatsScript.itemsValue++;
        }
    }
}
