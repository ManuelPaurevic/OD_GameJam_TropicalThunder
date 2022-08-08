using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public AudioClip footstepsWalk;
    public AudioClip footstepsSprint;

    [SerializeField]
    private float walkSpeed = 3f;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite leftSprite;
    [SerializeField]
    private Sprite upSprite;
    [SerializeField]
    private Sprite rightSprite;
    [SerializeField]
    private Sprite downSprite;

    [SerializeField]
    private float sprintSpeed = 5f;

    [SerializeField]
    private float dodgeTime = 0.5f;

    [SerializeField]
    private float dodgeAmount = 10f;

    [SerializeField]
    private MovementController movementController;

    [SerializeField]
    private AudioSource playerHitAudioSrc;

    [SerializeField]
    private AudioSource footstepsAudioSrc;

    [SerializeField]
    PlayerStats playerStats;

    public bool dodging = false;

    public Vector2 facingDirection;

    private Vector2 lastMovement;

    private float lastSpeed;

    private void Awake() {
        if (!movementController) {
            movementController = GetComponent<MovementController>();
        }
        if (!footstepsAudioSrc) {
            List<AudioSource> audios = new List<AudioSource>();
            GetComponents<AudioSource>(audios);
            footstepsAudioSrc = audios[0];
        }
        if (!playerHitAudioSrc) {
            List<AudioSource> audios = new List<AudioSource>();
            GetComponents<AudioSource>(audios);
            footstepsAudioSrc = audios[1];
        }
    }

    private void Start() {
        movementController.ChangeMovement(Vector2.zero);
        movementController.ChangeSpeed(walkSpeed);
    }

    private void UpdateFootstepsAudio(Vector2 movement) {
        if (movement.SqrMagnitude() > 0) {
            if (!footstepsAudioSrc.isPlaying) {
                footstepsAudioSrc.Play();
            }
        } else {
            if (footstepsAudioSrc.isPlaying) {
                footstepsAudioSrc.Stop();
            }
        }
    }

    private void changeFootstepsAudioClip(AudioClip clip) {
        if (footstepsAudioSrc.isPlaying) {
            footstepsAudioSrc.Stop(); // Unity doesn't change the clip immediately while playing
            footstepsAudioSrc.clip = clip;
            footstepsAudioSrc.Play();
        } else {
            footstepsAudioSrc.clip = clip;
        }
    }

    private void UpdateFootstepsAudioSpeed(float sprintInput) {
        if (sprintInput > 0 && footstepsAudioSrc.clip == footstepsWalk) {
            changeFootstepsAudioClip(footstepsSprint);
        } else if (sprintInput <= 0 && footstepsAudioSrc.clip == footstepsSprint) {
            changeFootstepsAudioClip(footstepsWalk);
        }
    }

    private void OnMovement(InputValue input) {
        Vector2 movement = input.Get<Vector2>();
        lastMovement = movement;
        if (!dodging) {
            if (!Vector2.zero.Equals(movement)) {
                facingDirection = movement;
                if (Vector2.up.Equals(facingDirection)) {
                    spriteRenderer.sprite = upSprite;
                } else if (Vector2.right.Equals(facingDirection)) {
                    spriteRenderer.sprite = rightSprite;
                } else if (Vector2.down.Equals(facingDirection)) {
                    spriteRenderer.sprite = downSprite;
                } else if (Vector2.left.Equals(facingDirection)) {
                    spriteRenderer.sprite = leftSprite;
                }
            }
            movementController.ChangeMovement(movement);
        }
        UpdateFootstepsAudio(movement);
    }

    private void OnSprint(InputValue input) {
        float sprintInput = input.Get<float>();
        if (sprintInput > 0) {
            movementController.ChangeSpeed(sprintSpeed);
            lastSpeed = sprintSpeed;
        } else {
            movementController.ChangeSpeed(walkSpeed);
            lastSpeed = walkSpeed;
        }
        UpdateFootstepsAudioSpeed(sprintInput);
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
        movementController.ChangeSpeed(lastSpeed);
        movementController.ChangeMovement(lastMovement);
        dodging = false;
    }

    public Vector2 GetPosition() {
        return transform.position;
    }

    public void Hit(int hitAmount) {
        playerHitAudioSrc.Play();
        playerStats.RemoveScore(hitAmount);
    }
}
