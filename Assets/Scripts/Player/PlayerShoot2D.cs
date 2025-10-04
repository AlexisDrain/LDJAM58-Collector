using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Weapon {
    machinegun,
    missilelauncher,
    pistol,
}
public class PlayerShoot2D : MonoBehaviour {
    public Weapon myWeapon;
    public int currentAmmo;
    // public LineRenderer shootCrosshairLine;

    // public Transform shootCrosshairTransform;
    public Transform unicornHornTrans;
    public List<AudioClip> shootClips;

    public float gunPositionOffset = 0.1f;
    public float gunShootDelayOffset = 0f;
    public float playerPushbackForce = 300f;
    public float nextShotCountdownDefault;
    private float nextShotCountdown;

    [Header("read only")]
    public Vector3 _mousePosition;
    public GameObject weaponUI;
    public TextMeshProUGUI weaponUIAmmoCount;

    void Start() {

    }
    public IEnumerator ShootCountdown() {
        yield return new WaitForSeconds(gunShootDelayOffset);


        Vector3 direction = (_mousePosition - transform.position).normalized;

        if (myWeapon == Weapon.machinegun) {
            GameObject bullet = GameManager.pool_bulletsRevolver.Spawn(transform.position);
            // bullet.GetComponent<EntityMove>().SetDirection(_mousePosition - transform.position, GameManager.playerTransform.GetComponent<Collider>());
        } else if (myWeapon == Weapon.missilelauncher) {
            GameObject missile = GameManager.pool_bulletsRevolver.Spawn(transform.position);
            missile.GetComponent<EntityMove>().SetDirection(direction, GameManager.playerTrans.GetComponent<Collider>());
        } else if (myWeapon == Weapon.pistol) {
            GameObject bullet = GameManager.pool_bulletsRevolver.Spawn(transform.position);
            bullet.GetComponent<EntityMove>().SetDirection(direction, GameManager.playerTrans.GetComponent<Collider>());
        }

        // sfx
        int randomSFX = Random.Range(0, shootClips.Count);
        GameManager.SpawnLoudAudio(shootClips[randomSFX]);

        // player pushback
        GameManager.playerTrans.GetComponent<Rigidbody>().AddForce(-1f * direction * playerPushbackForce, ForceMode.Force);

        currentAmmo -= 1;
        weaponUIAmmoCount.text = currentAmmo.ToString();
        if (currentAmmo <= 0) {
            Destroy(weaponUI);
            Destroy(gameObject);
        }
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
            StartCoroutine("ShootCountdown");
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
