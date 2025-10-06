using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalManager : MonoBehaviour
{
    public bool collectedUranium = false;
    public bool collectedBiscuit = false;

    public UnityEvent collectedBothItems;
    public void CollectedUranium()
    {
        collectedUranium = true;
        if(collectedBiscuit) {
            collectedBothItems.Invoke();
        }
    }
    public void CollectedBiscuit() {
        collectedBiscuit = true;
        if (collectedUranium) {
            collectedBothItems.Invoke();
        }
    }

}
