using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsController : MonoBehaviour
{
    public Transform unicornHornTrans;
    public float playerPushForce = 10f;
    public float defaultShootCountdown = 0.2f;

    public LineRenderer lineRenderer;

    [Header("Read only")]
    public bool _isFiring = false;
    private float currentShootCountdown;
    private AudioSource myAudioSource;
    private PlayerController playerController;
    private Rigidbody playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        playerController = GameManager.playerTrans.GetComponent<PlayerController>();
        playerRigidbody = GameManager.playerTrans.GetComponent<Rigidbody>();
    }

    /*
    private void Update() {
        if (GameManager.playerIsDead == true // || GameManager.menus.activeSelf
            // || GameManager.endGame == true
            || playerController._canMove == false) {
            _isFiring = false;
            return;
        }
        if (Input.GetButtonDown("Shoot")) {
            _isFiring = true;
        }
        if (Input.GetButtonUp("Shoot")) {
            _isFiring = false;
        }
    }
    */
    /*
    void SpawnBullets() {
        Vector3 mousePosition = GameManager.GetMousePositionOnFloor(); // + new Vector3(0f, 1f, 0f);
        Vector3 direction = (mousePosition - unicornHornTrans.position).normalized;
        List<float> angles = new List<float> { -30f, -15f, 0f, 15f, 30f };

        for(int i = 0; i < angles.Count; i++) {
            GameObject obj = GameManager.pool_bulletsRevolver.Spawn(transform.position);
            obj.GetComponent<Rigidbody>().position = transform.position;
            obj.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            Vector3 rotatedDirection = Quaternion.Euler(0f, angles[i], 0f) * direction;
            obj.GetComponent<BulletController>().SetDirection(rotatedDirection);
        }
    }
    */
    // Update is called once per frame
    void FixedUpdate() {
        Vector3 mousePosition = GameManager.GetMousePositionOnFloor();

        if (GameManager.playerInMenu == true || GameManager.playerIsDead == true) {
            return;
        }

        // line to crosshair
        lineRenderer.SetPosition(0, unicornHornTrans.position);
        lineRenderer.SetPosition(1, mousePosition);
    }
}
