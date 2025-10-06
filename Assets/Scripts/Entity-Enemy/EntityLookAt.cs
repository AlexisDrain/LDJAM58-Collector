using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLookAt : MonoBehaviour {

    public bool faceCamera = true;
    public bool targetIsMouse = false;
    public bool targetIsPlayer = true;
    public bool flipYWhenOnLeftSide = true;
    public Transform targetTransform;
    public SpriteRenderer spriteRenderer;


    void Start() {
        if (targetIsPlayer == true) {
            targetTransform = GameManager.playerTrans;
        }
        if (spriteRenderer == null) {
            print("sprite renderer not found");
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private Vector3 direction;
    void LateUpdate() {
        if(targetIsMouse == true) {
            Vector3 mousePos = GameManager.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(transform.position, Camera.main.transform.position)));
            direction = new Vector3(mousePos.x, mousePos.y, 0f) - new Vector3(transform.position.x, transform.position.y, 0f);
        } else {
            direction = new Vector3(targetTransform.position.x, targetTransform.position.z, 0f) - new Vector3(transform.position.x, transform.position.z, 0f);
        }
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(faceCamera) {
            transform.eulerAngles = new Vector3(60f, 0f, angle);

        } else {
            // won't need this probably.
            transform.eulerAngles = new Vector3(0f, 0f, angle);
        }
        // GetComponent<PixelRotate>().SetRotate(angle);

        if (flipYWhenOnLeftSide == false) {
            return;
        }

        if(angle < 90 && angle > -90) {
            spriteRenderer.flipY = false;
        } else {
            spriteRenderer.flipY = true;
        }

    }
}
