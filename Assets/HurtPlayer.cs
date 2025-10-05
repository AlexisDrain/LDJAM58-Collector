using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HurtPlayer : MonoBehaviour
{
    public UnityEvent onTouchEvent;
    public bool destroyOnTouchAnything = true;
    public bool destroySelfOnInvoke = true;
    public int damageValue = 1;
    void Start() {

    }

    void OnCollisionEnter(Collision col) {
        if (col.collider.gameObject.GetComponent<PlayerHealth>()) {
            col.collider.gameObject.GetComponent<PlayerHealth>().AddDamage(damageValue);
            onTouchEvent.Invoke();
            if (destroySelfOnInvoke) {
                gameObject.SetActive(false);
            }
        }
        if (destroyOnTouchAnything == true) {
            onTouchEvent.Invoke();
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.GetComponent<PlayerHealth>()) {
            col.gameObject.GetComponent<PlayerHealth>().AddDamage(damageValue);
            onTouchEvent.Invoke();
            if (destroySelfOnInvoke) {
                gameObject.SetActive(false);
            }
        }
        if (destroyOnTouchAnything == true) {
            onTouchEvent.Invoke();
            gameObject.SetActive(false);
        }
    }
}
