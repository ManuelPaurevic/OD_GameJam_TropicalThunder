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
    private float dodgeAmount = 10f;

    [SerializeField]
    private MovementController movementController;

    public bool dodging = false;

    public Vector2 facingDirection;

    private Vector2 lastMovement;

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
        Vector2 movement = input.Get<Vector2>();
        lastMovement = movement;
        if (!dodging) {
            if (!Vector2.zero.Equals(movement)) {
                facingDirection = movement;
            }
            movementController.ChangeMovement(movement);
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
        if (!Vector2.zero.Equals(facingDirection)) {
            StartCoroutine(PerformDodge(facingDirection));
        }
    }

    private IEnumerator PerformDodge(Vector2 direction) {
        dodging = true;
        float activeDodgeTime = 0;
        movementController.ChangeMovement(direction);
        while(activeDodgeTime <= dodgeTime) {
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
}
