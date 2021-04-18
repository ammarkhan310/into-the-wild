using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour {

    [SerializeField] private Transform groundPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public CharacterController controller;
    [SerializeField] public bool isGrounded = false;
    [SerializeField] Animator animator;
    [SerializeField] public float speed = 6f;
    [SerializeField] public Transform cam;
    [SerializeField] public float vSpeed;
    [SerializeField] public float jumpSpeed = 3f;
    [SerializeField] float playerGravity = 1f;
    [SerializeField] Transition transition;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        RaycastHit collision;
        if (Physics.Raycast(groundPosition.position, Vector3.down, out collision, 0.2f, groundLayer)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }

        if (!isGrounded) {
            vSpeed += playerGravity * -9.81f * Time.deltaTime;
        } else {
            vSpeed = 0f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
        // Jump
        if (isGrounded && Input.GetButtonDown("Jump")) {
            animator.SetTrigger("Jump");
            direction.y += Mathf.Sqrt(jumpSpeed * -3.0f * playerGravity);
            isGrounded = false;
        } else if (!isGrounded) {
        }

        if (direction.magnitude >= 0.1f) {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            direction = moveDir;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.LeftShift)) {
            animator.SetBool("Running", true);
            speed = speed*2;
        }

        if (isGrounded && Input.GetKeyUp(KeyCode.LeftShift)) {
            animator.SetBool("Running", false);
            speed = 9f;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Mouse1)) {
            animator.SetBool("Shielding", true);
            controller.enabled = false;
        }

        if (isGrounded && Input.GetKeyUp(KeyCode.Mouse1)) {
            animator.SetBool("Shielding", false);
            controller.enabled = true;
        }

        animator.SetFloat("Speed", direction.magnitude);
        vSpeed -= playerGravity * Time.deltaTime;
        direction.y = vSpeed;
        if (controller.enabled) {
            controller.Move(direction * speed * Time.deltaTime); 
        }

        if (gameObject.GetComponent<Health>().health <= 0) {
            controller.enabled = false;
            StartCoroutine(GameOver());
        }
    }

    // A couritine to play the player dying animation and call the game over scene.
    IEnumerator GameOver(){
        animator.SetTrigger("Dying");
        yield return new WaitForSeconds(3);
        transition.GameOver();
    }
}
