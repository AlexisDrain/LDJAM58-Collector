using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    public int defaultAmmo = 30;
    public GameObject weaponPrefab;
    public GameObject weaponUIPrefab;
    public AudioClip pickupAudio;
    void Start()
    {
        
    }

    public void Pickup()
    {

        GameObject newGunMenu = GameObject.Instantiate(weaponUIPrefab, GameManager.weaponMenu);

        GameObject newGun = GameObject.Instantiate(weaponPrefab, GameManager.playerTrans);
        newGun.GetComponent<PlayerShoot2D>().gunShootDelayOffset = Random.Range(0f, 0.7f);
        newGun.GetComponent<PlayerShoot2D>().currentAmmo = defaultAmmo;
        newGun.GetComponent<TeleGunesis>().telegunPosOffset = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        newGun.GetComponent<PlayerShoot2D>().weaponUI = newGunMenu;
        newGun.GetComponent<PlayerShoot2D>().weaponUIAmmoCount = newGunMenu.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        newGun.GetComponent<PlayerShoot2D>().weaponUIAmmoCount.text = defaultAmmo.ToString();

        GameManager.SpawnLoudAudio(pickupAudio, new Vector2(0.9f, 1.2f));
        Destroy(gameObject);
    }
}
