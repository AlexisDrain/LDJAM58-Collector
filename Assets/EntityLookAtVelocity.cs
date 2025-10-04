using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLookAtVelocity : MonoBehaviour {
    public bool faceCamera = true;
    public Rigidbody targetRigidbody;
    void Start()
    {
        
    }

    void LateUpdate() {
        Vector3 direction = targetRigidbody.velocity.normalized;

        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        if (faceCamera) {
            transform.eulerAngles = new Vector3(60f, 0f, angle);

        } else {
            // won't need this probably.
            transform.eulerAngles = new Vector3(0f, 0f, angle);
        }
    }
}
