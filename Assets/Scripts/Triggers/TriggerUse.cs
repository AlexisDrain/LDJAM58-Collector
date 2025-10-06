using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerUse : MonoBehaviour
{

    public UnityEvent onTriggerUse;
    public UnityEvent onTriggerReset;
    private bool playerInUseRange = false;
    public bool canBeReTriggered = false;
    public bool onEnableResetHasBeenTriggered = true;

    private bool hasBeenTriggered = false;
    /*
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
    */

    private void Update() {
        if (playerInUseRange && GameManager.playerIsDead == false && GameManager.playerInMenu == false && Input.GetButtonDown("Use")) {
            onTriggerUse.Invoke();
            GameManager.useMessage.SetActive(false);
            playerInUseRange = false;
        }
    }

    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.CompareTag("Player")) {
            GameManager.useMessage.SetActive(true);
            playerInUseRange = true;
        }
    }
    void OnTriggerExit(Collider otherCollider) {
        if (otherCollider.CompareTag("Player")) {
            GameManager.useMessage.SetActive(false);
            playerInUseRange = false;
        }
    }
}
