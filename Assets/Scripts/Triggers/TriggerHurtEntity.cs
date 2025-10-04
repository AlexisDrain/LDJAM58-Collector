using UnityEngine;
using UnityEngine.Events;

public class TriggerHurtEntity : MonoBehaviour
{
    public UnityEvent onTouchEvent;
    public bool destroyOnTouchAnything = true;
    public bool destroySelfOnInvoke = true;
    public int damageValue = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.gameObject.GetComponent<EntityHealth>()) {
            col.collider.gameObject.GetComponent<EntityHealth>().AddDamage(damageValue);
            onTouchEvent.Invoke();
            if (destroySelfOnInvoke) {
                gameObject.SetActive(false);
            }
        }
        if(destroyOnTouchAnything == true) {
            onTouchEvent.Invoke();
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.GetComponent<EntityHealth>()) {
            col.gameObject.GetComponent<EntityHealth>().AddDamage(damageValue);
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
