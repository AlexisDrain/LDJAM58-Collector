using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerPlayerTouch : MonoBehaviour
{
    [TextArea(2, 30)]
    public string notes;

    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerReset;
    public bool canBeReTriggered = false;
    public bool onEnableResetHasBeenTriggered = true;

    private bool hasBeenTriggered = false;
    // Start is called before the first frame update
    void Start() {
        ResetTrigger();

        GameManager.playerReviveEvent.AddListener(ResetTrigger);
    }
    private void OnEnable() {
        if(onEnableResetHasBeenTriggered) {
            ResetTrigger();
        }
    }
    void ResetTrigger() {
        hasBeenTriggered = false;
        onTriggerReset.Invoke();
    }
    void OnTriggerEnter(Collider otherCollider) {
        if (hasBeenTriggered && canBeReTriggered == false) {
            return;
        }
        if (otherCollider.CompareTag("Player")) {
            onTriggerEnter.Invoke();
            hasBeenTriggered = true;
        }

    }
    private void OnCollisionEnter(Collision collision) {
        if (hasBeenTriggered && canBeReTriggered == false) {
            return;
        }
        if (collision.collider.CompareTag("Player")) {
            onTriggerEnter.Invoke();
            hasBeenTriggered = true;
        }

    }
}
