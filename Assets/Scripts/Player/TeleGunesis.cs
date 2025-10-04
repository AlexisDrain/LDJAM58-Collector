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

        if (GameManager.playerInMenu == true || GameManager.playerIsDead == true) {
            return;
        }

        startPos = GameManager.playerTrans.position;

        endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(transform.position, Camera.main.transform.position)));
        // endPos = new Vector3(endPos.x, 1f, endPos.z);
        transform.position = GetPointBetween(startPos, endPos, 0.2f);
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }

    private Vector3 GetPointBetween(Vector3 v1, Vector3 v2, float percentage) {
        return (v2 - v1) * percentage + v1;
    }
}
