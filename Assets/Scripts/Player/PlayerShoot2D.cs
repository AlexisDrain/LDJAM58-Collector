using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon {
    machinegun,
    missilelauncher,
}
public class PlayerShoot2D : MonoBehaviour {
    public Weapon myWeapon;
    // public LineRenderer shootCrosshairLine;

    // public Transform shootCrosshairTransform;
    public Transform unicornHornTrans;
    public float playerPushForce = 1f;
    public List<AudioClip> shootClips;

    public float gunOffset = 0.1f;
    public float playerPushbackForce = 300f;
    public float nextShotCountdownDefault;
    private float nextShotCountdown;

    [Header("read only")]
    public Vector3 _mousePosition;

    void Start() {

    }
    public void NewWeapon() {

    }
    private void FixedUpdate() {
        if (nextShotCountdown > 0.0f) {
            nextShotCountdown -= Time.deltaTime;
        }


        if (GameManager.playerInMenu == true || GameManager.playerIsDead == true) {
            return;
        }

        _mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(transform.position, Camera.main.transform.position)));
        if (Input.GetButton("Shoot") && nextShotCountdown <= 0f) {

            nextShotCountdown = nextShotCountdownDefault;

            Vector3 direction = (_mousePosition - unicornHornTrans.position).normalized;

            if (Input.GetButton("Shoot")) {
                GameManager.playerRigidbody.AddForce(-direction * playerPushForce, ForceMode.Force);
            }

            int randomSFX = Random.Range(0, shootClips.Count);
            GameManager.SpawnLoudAudio(shootClips[randomSFX]);
            if(myWeapon == Weapon.machinegun) {
                GameObject bullet = GameManager.pool_bulletsRevolver.Spawn(transform.position);
                // bullet.GetComponent<EntityMove>().SetDirection(_mousePosition - transform.position, GameManager.playerTransform.GetComponent<Collider>());
            } else if (myWeapon == Weapon.missilelauncher) {
                GameObject missile = GameManager.pool_bulletsRevolver.Spawn(transform.position);
                // missile.GetComponent<EntityMove>().SetDirection(_mousePosition - transform.position, GameManager.playerTransform.GetComponent<Collider>());
            }

            GameManager.playerTrans.GetComponent<Rigidbody>().AddForce((transform.position - _mousePosition).normalized * playerPushbackForce, ForceMode.Force);
        }
    }
    /*
    private void LateUpdate() {
        //shootCrosshairLine.SetPosition(0, GameManager.playerTransform.position);
        //shootCrosshairLine.SetPosition(0, transform.position);
        //shootCrosshairLine.SetPosition(1, _mousePosition);
        /*
        RaycastHit hit;
        Physics.Linecast(transform.position, mousePositionOnFloor, out hit, (1 << GameManager.layerMask_World) + (1 << GameManager.layerMask_Entity));
        if(hit.collider != null) {
            shootCrosshairLine.SetPosition(1, hit.point);
        } else {
            shootCrosshairLine.SetPosition(1, mousePositionOnFloor);
        }
        
    }
*/
}
