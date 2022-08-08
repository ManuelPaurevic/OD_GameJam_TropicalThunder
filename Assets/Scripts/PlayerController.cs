using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 3f;

    [SerializeField]
    private float sprintSpeed = 5f;

    [SerializeField]
    private float dodgeTime = 0.5f;

    [SerializeField]
    private MovementController movementController;

    public bool dodging = false;

    public Vector2 facingDirection;

    private void Awake()
    {
        if (!movementController) {
            movementController = GetComponent<MovementController>();
        }
    }

    private void Start() {
        movementController.ChangeMovement(Vector2.zero);
        movementController.ChangeSpeed(walkSpeed);
    }

    private void OnMovement(InputValue input) {
        facingDirection = input.Get<Vector2>();
        if (!dodging) {
            movementController.ChangeMovement(facingDirection);
        }
    }

    private void OnSprint(InputValue input) {
        float sprintInput = input.Get<float>();
        if (sprintInput > 0) {
            movementController.ChangeSpeed(sprintSpeed);
        } else {
            movementController.ChangeSpeed(walkSpeed);
        }
    }

    private void OnDodge() {
        StartCoroutine(PerformDodge(facingDirection));
    }

    private IEnumerator PerformDodge(Vector2 direction) {
        dodging = true;
        float activeDodgeTime = 0;
        while(activeDodgeTime <= dodgeTime) {
            activeDodgeTime += Time.deltaTime;
            yield return null;
        }
        dodging = false;
    }

    public Vector2 GetPosition() {
        return transform.position;
    }
}
