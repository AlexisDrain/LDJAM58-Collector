using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMoveTo : MonoBehaviour
{
    public float speed = 10f;
    public float maxVelocity = 100f;
    public bool targetIsPlayer = true;
    public Transform target;
    public float sightRange = 10f;
    public bool _hasSeenPlayer = false;

    public bool flipGraphicOnLeft = true;
    public SpriteRenderer mySpriteRenderer;
    private Rigidbody myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        if (targetIsPlayer) {
            target = GameManager.playerTrans;
        }
    }
    private void OnEnable() {
        _hasSeenPlayer = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if(flipGraphicOnLeft) {
            if (mySpriteRenderer.flipX == false && myRigidbody.velocity.x < -1f) {
                mySpriteRenderer.flipX = true;
            }
            if (mySpriteRenderer.flipX == true && myRigidbody.velocity.x > 1f) {
                mySpriteRenderer.flipX = false;
            }
        }

        if (Vector3.Distance(transform.position, target.position) <= sightRange) {
            _hasSeenPlayer = true;
        }
        if(_hasSeenPlayer) {
            Vector3 direction = (target.position - transform.position).normalized;
            myRigidbody.AddForce(direction * speed);
        }
        myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, maxVelocity);
    }
}
