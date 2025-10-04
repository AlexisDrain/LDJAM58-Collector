using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMove : MonoBehaviour {

    [Header("entity move values")]
    public float addForce = 10f;
    public float maxSpeed = 30f;
    public bool lookTowardsVelocity = false;

    [Header("Read Only")]
    public Collider spawner;
    public Vector3 _direction;
    private Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

    }

    private void OnEnable() {
        Start();
    }
    private void OnDisable() {
        if(spawner) {
            Physics.IgnoreCollision(GetComponent<Collider>(), spawner, false);
        }
    }

    public void ResetEntity() {
        if (spawner) {
            Physics.IgnoreCollision(GetComponent<Collider>(), spawner, false);
        }
    }

    public void SetDirection(Vector3 newDirection, Collider spawner) {
        Physics.IgnoreCollision(GetComponent<Collider>(), spawner, true);
        myRigidbody.velocity = Vector3.zero;
        _direction = newDirection.normalized;
        if (lookTowardsVelocity) {
            float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0f, 0f, angle);
        }
    }


    // Update is called once per frame
    void FixedUpdate() {
        myRigidbody.AddForce(addForce * _direction, ForceMode.Force);
        myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, maxSpeed);
        /*
        if(lookTowardsVelocity) {
            float angle = Mathf.Atan2(myRigidbody.velocity.y, myRigidbody.velocity.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0f, 0f, angle);

        }
        */
        // transform.rotation = Quaternion.LookRotation(_direction);
    }
}
