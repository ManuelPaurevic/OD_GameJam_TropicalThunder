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
    private Sprite leftSpriteLFoot;

    [SerializeField]
    private Sprite leftSpriteRFoot;
    [SerializeField]
    private Sprite upSprite;
    [SerializeField]
    private Sprite upSpriteLFoot;
    [SerializeField]
    private Sprite upSpriteRFoot;
    [SerializeField]
    private Sprite rightSprite;
    [SerializeField]
    private Sprite rightSpriteLFoot;
    [SerializeField]
    private Sprite rightSpriteRFoot;
    [SerializeField]
    private Sprite downSprite;
    [SerializeField]
    private Sprite downSpriteLFoot;
    [SerializeField]
    private Sprite downSpriteRFoot;

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

    private Vector2 lastFacingDirection;

    private Vector2 lastMovement;

    private float lastSpeed;

    private int walkingAnimationFrameIdx = 0;
    private int walkingAnimationFrameCounter = 0;

    private int hitBlinkCounter = 0;

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

    private void UpdateWalkingAnimation() {
        walkingAnimationFrameCounter = ++walkingAnimationFrameCounter % 8;
        if (walkingAnimationFrameCounter == 0) {
            walkingAnimationFrameIdx = ++walkingAnimationFrameIdx % 4;
            if (Vector2.up.Equals(lastFacingDirection)) {
                if (lastSpeed == sprintSpeed) {
                    spriteRenderer.sprite = walkingAnimationFrameIdx == 0 ? upSpriteLFoot : walkingAnimationFrameIdx == 1 ? upSpriteRFoot : walkingAnimationFrameIdx == 2 ? upSpriteLFoot : upSpriteRFoot;
                } else {
                    spriteRenderer.sprite = walkingAnimationFrameIdx == 0 ? upSprite : walkingAnimationFrameIdx == 1 ? upSpriteLFoot : walkingAnimationFrameIdx == 2 ? upSprite : upSpriteRFoot;
                }
            } else if (Vector2.right.Equals(lastFacingDirection)) {
                if (lastSpeed == sprintSpeed) {
                    spriteRenderer.sprite = walkingAnimationFrameIdx == 0 ? rightSpriteLFoot : walkingAnimationFrameIdx == 1 ? rightSpriteRFoot : walkingAnimationFrameIdx == 2 ? rightSpriteLFoot : rightSpriteRFoot;
                } else {
                    spriteRenderer.sprite = walkingAnimationFrameIdx == 0 ? rightSprite : walkingAnimationFrameIdx == 1 ? rightSpriteLFoot : walkingAnimationFrameIdx == 2 ? rightSprite : rightSpriteRFoot;
                }
            } else if (Vector2.down.Equals(lastFacingDirection)) {
                if (lastSpeed == sprintSpeed) {
                    spriteRenderer.sprite = walkingAnimationFrameIdx == 0 ? downSpriteLFoot : walkingAnimationFrameIdx == 1 ? downSpriteRFoot : walkingAnimationFrameIdx == 2 ? downSpriteLFoot : downSpriteRFoot;
                } else {
                    spriteRenderer.sprite = walkingAnimationFrameIdx == 0 ? downSprite : walkingAnimationFrameIdx == 1 ? downSpriteLFoot : walkingAnimationFrameIdx == 2 ? downSprite : downSpriteRFoot;
                }
            } else if (Vector2.left.Equals(lastFacingDirection)) {
                if (lastSpeed == sprintSpeed) {
                    spriteRenderer.sprite = walkingAnimationFrameIdx == 0 ? leftSpriteLFoot : walkingAnimationFrameIdx == 1 ? leftSpriteRFoot : walkingAnimationFrameIdx == 2 ? leftSpriteLFoot : leftSpriteRFoot;
                } else {
                    spriteRenderer.sprite = walkingAnimationFrameIdx == 0 ? leftSprite : walkingAnimationFrameIdx == 1 ? leftSpriteLFoot : walkingAnimationFrameIdx == 2 ? leftSprite : leftSpriteRFoot;
                }
            }
        }
    }

    private void UpdateHitAnimation() {
        if (hitBlinkCounter > 0) {
            hitBlinkCounter--;
            if (hitBlinkCounter % 6 < 3) {
                spriteRenderer.color = Color.white;
            } else {
                spriteRenderer.color = Color.red;
            }
        }
    }

    void FixedUpdate() {
        // timeScale == 1f stops animating during potential pausing of time:
        if (Time.timeScale == 1f) {
            UpdateHitAnimation();
            if (!dodging && !Vector2.zero.Equals(lastMovement)) {
                UpdateWalkingAnimation();
            }
        }
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
                    lastFacingDirection = facingDirection;
                } else if (Vector2.right.Equals(facingDirection)) {
                    spriteRenderer.sprite = rightSprite;
                    lastFacingDirection = facingDirection;
                } else if (Vector2.down.Equals(facingDirection)) {
                    spriteRenderer.sprite = downSprite;
                    lastFacingDirection = facingDirection;
                } else if (Vector2.left.Equals(facingDirection)) {
                    spriteRenderer.sprite = leftSprite;
                    lastFacingDirection = facingDirection;
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
        spriteRenderer.color = Color.red;
        hitBlinkCounter = 18;
    }
}
