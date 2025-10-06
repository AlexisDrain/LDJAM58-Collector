using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveForce;
    public float drag = 0.98f;
    public float maxVelocity = 100f;
    public Transform graphicalObject;
    private Rigidbody myRigidbody;
    public Animator myAnimator;

    public bool _canMove = true;
    private bool directionRight = true;
    private float targetScaleX = 1f;
    private float currentScaleX = 1f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator.keepAnimatorStateOnDisable = true;
    }

    private void Update() {

        if (GameManager.playerInMenu == true || GameManager.playerIsDead == true || GameManager.playerInDialogue == true) {
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");

        //if (directionRight == true && myRigidbody.velocity.x < -1f) { because of weapon pushback, shooting a gun would turn the player around
        if (directionRight == true && x < -0.5f) {
            directionRight = false;
            targetScaleX = -1f;
        }
        if (directionRight == false && x > 0.5f) {
            directionRight = true;
            targetScaleX = 1f;
        }

        currentScaleX = Mathf.Lerp(currentScaleX, targetScaleX, 8f * Time.deltaTime);
        graphicalObject.localScale = new Vector3(currentScaleX, 1f, 1f);
    }
    void FixedUpdate() {

        if (GameManager.playerInMenu == true || GameManager.playerIsDead == true || GameManager.playerInDialogue == true) {
            myRigidbody.velocity = Vector3.zero;
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (x != 0) {
            myRigidbody.AddForce(x * Vector3.right * moveForce);
        }
        if (y != 0) {
            myRigidbody.AddForce(y * Vector3.forward * moveForce);
        }

        if (myRigidbody.velocity.magnitude > 1f) {
            myAnimator.SetBool("isMoving", true);
        } else {
            myAnimator.SetBool("isMoving", false);
        }

        myRigidbody.velocity = myRigidbody.velocity * drag;
        myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, maxVelocity);
    }
}
