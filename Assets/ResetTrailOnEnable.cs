using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrailOnEnable : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<TrailRenderer>().Clear();
    }

}
