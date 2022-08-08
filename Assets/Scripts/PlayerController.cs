using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 3f;

    [SerializeField]
    private float sprintSpeed = 5f;

    [SerializeField]
    private MovementController movementController;

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
        movementController.ChangeMovement(movement);
    }

    private void OnSprint(InputValue input) {
        float sprintInput = input.Get<float>();
        if (sprintInput > 0) {
            movementController.ChangeSpeed(sprintSpeed);
        } else {
            movementController.ChangeSpeed(walkSpeed);
        }
    }

    public Vector2 GetPosition() {
        return transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Collectibles")){
            Destroy(other.gameObject);
        }
    }
}
