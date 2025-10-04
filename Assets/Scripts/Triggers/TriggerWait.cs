using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerWait : MonoBehaviour {

    [TextArea(2, 30)]
    public string notes = "your notes";
    public float timeToWait = 3f;
    public UnityEvent onTimeEnd;
    public bool waitForSecondsRealtime = false;
    public bool startOnEnable = true;
    public bool startOnStart = false;
    public bool restartTimerOnReset = true;

    void Start() {
        if (startOnStart) {
            ResetTrigger();
        }
    }
    public void OnTimeChange() {
        ResetTrigger();
    }
    public void StartCoroutine() {
        ResetTrigger();
    }
    private void OnEnable() {
        if (startOnEnable && gameObject.activeSelf) {
            ResetTrigger();
        }
    }

    public IEnumerator Countdown() {
        if(waitForSecondsRealtime) {
            yield return new WaitForSecondsRealtime(timeToWait);
        } else {
            yield return new WaitForSeconds(timeToWait);
        }

        onTimeEnd.Invoke();
    }

    void ResetTrigger() {

        if (restartTimerOnReset) {
            StopCoroutine("Countdown");
        }

        if (gameObject.activeSelf) {
            StartCoroutine("Countdown");
        }
    }
}
