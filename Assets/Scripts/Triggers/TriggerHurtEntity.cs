using UnityEngine;
using UnityEngine.Events;

public class TriggerHurtEntity : MonoBehaviour
{
    // public UnityEvent onTouchEvent;
    public int damageValue = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.gameObject.GetComponent<EntityHealth>()) {
            col.collider.gameObject.GetComponent<EntityHealth>().AddDamage(damageValue);
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.GetComponent<EntityHealth>()) {
            col.gameObject.GetComponent<EntityHealth>().AddDamage(damageValue);
            gameObject.SetActive(false);
        }
    }
}
