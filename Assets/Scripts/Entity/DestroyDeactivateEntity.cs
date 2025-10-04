using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDeactivateEntity : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }
    public void DeactivateObject() {
        gameObject.SetActive(false);
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
