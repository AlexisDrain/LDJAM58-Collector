using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveForce;
    public float drag = 0.98f;
    public float maxVelocity = 100f;
    private Rigidbody myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (x != 0) {
            myRigidbody.AddForce(x * Vector3.right * moveForce);
        }
        if (y != 0) {
            myRigidbody.AddForce(y * Vector3.forward * moveForce);
        }
        myRigidbody.velocity = myRigidbody.velocity * drag;
        myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, maxVelocity);
    }
}
