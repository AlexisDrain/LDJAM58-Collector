using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleGunesis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Vector3 startPos;
    private Vector3 endPos;
    void FixedUpdate()
    {
        startPos = GameManager.playerTrans.position;

        endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(transform.position, Camera.main.transform.position)));

        transform.position = GetPointBetween(startPos, endPos, 0.2f);
    }

    private Vector3 GetPointBetween(Vector3 v1, Vector3 v2, float percentage) {
        return (v2 - v1) * percentage + v1;
    }
}
