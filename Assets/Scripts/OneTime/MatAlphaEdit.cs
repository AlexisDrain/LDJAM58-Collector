using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatAlphaEdit : MonoBehaviour {
    public Color myColor;

    private Material myMaterial;
    // Start is called before the first frame update
    void Start() {
        myMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void LateUpdate() {
        myMaterial.color = myColor;
    }
}
