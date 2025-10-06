using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour {
    public bool dropItemEveryOtherEnemy = true;
    public float defaultHealth = 100;
    public GameObject healthBar;
    public SpriteRenderer healthBarBar;
    public List<AudioClip> clipHurt;
    public List<AudioClip> clipDeath;
    public GameObject dropItem;

    public UnityEvent onDeathEvent;
    public Image labinnacHealthbar;
    private Vector3 resetPosition;
    // public bool doNotCountKill = false;

    // private Vector3 originPosition;
    // private Vector2 originCamCoords;
    [Header("read only")]
    public float _currentHealth = 100;
    public bool spawnedByWave = false;

    void Awake()
    {
        resetPosition = transform.position;
        _currentHealth = defaultHealth;
        healthBar.SetActive(false);

        GameManager.playerReviveEvent.AddListener(ResetEnemy);
        /*
        // save entity map coords
        originCamCoords = new Vector2(transform.position.x / GameManager.cameraBounds.x,
                                           transform.position.z / GameManager.cameraBounds.y);
        originCamCoords = new Vector2(Mathf.FloorToInt(originCamCoords.x), Mathf.FloorToInt(originCamCoords.y));

        originPosition = transform.position;
        GameManager.playerChangeRoomEvent.AddListener(ChangeRoom);
        GameManager.playerChangeRoomEvent.AddListener(EnableIfInCameraCoords);
        */
    }
    private void ResetEnemy() {

        if (spawnedByWave == true) {
            Destroy(gameObject);  // when player is revived, this enemy which was spawned by Labinnac waves will destroy self
            return;
        }
        _currentHealth = defaultHealth;

        if(labinnacHealthbar == null) {
            healthBarBar.size = new Vector2(_currentHealth / defaultHealth * 3.6875f, healthBarBar.size.y);
            healthBar.SetActive(false); // starts false
        }

        transform.position = resetPosition;
        if(GetComponent<Rigidbody>()) {
            GetComponent<Rigidbody>().position = resetPosition;
        }

        if (GetComponent<EntityMoveTo>()) {
            GetComponent<EntityMoveTo>()._hasSeenPlayer = false;
        }
        if (GetComponent<ShootAtPlayer>()) {
            GetComponent<ShootAtPlayer>()._hasSeenPlayer = false;
        }

        gameObject.SetActive(true);
    }

    public void AddDamage(int damage=1) {
        _currentHealth -= damage;

        if (labinnacHealthbar == null) {
            healthBar.SetActive(true); // starts false
            healthBarBar.size = new Vector2(_currentHealth / defaultHealth * 3.6875f, healthBarBar.size.y);
        } else {
            labinnacHealthbar.fillAmount = _currentHealth / defaultHealth;
        }

        if (GetComponent<EntityMoveTo>()) {
            GetComponent<EntityMoveTo>()._hasSeenPlayer = true;
        }
        // _hasSeenPlayer on shoot at player is a bit confusing. EntityMoveTo makes sense though.
        //if (GetComponent<ShootAtPlayer>()) {
        // GetComponent<ShootAtPlayer>()._hasSeenPlayer = true;
        //}

        if (_currentHealth > 0) {
            if(clipHurt.Count > 0) {
                GetComponent<AudioSource>().clip = clipHurt[Random.Range(0, clipHurt.Count)];
                GetComponent<AudioSource>().PlayWebGL();
            }
            GameManager.particles_BloodDamage.transform.position = transform.position;
            GameManager.particles_BloodDamage.Play();
        }
        if(_currentHealth <= 0) {

            GameManager.particles_BloodKill.transform.position = transform.position;
            GameManager.particles_BloodKill.Play();

            if (clipDeath.Count > 0) {
                GameManager.SpawnLoudAudio(clipDeath[Random.Range(0, clipDeath.Count)]);
            }

            // item drop
            if (dropItem) {
                if(dropItemEveryOtherEnemy == true) {
                    if(GameManager.hasDroppedItem == false) {
                        GameObject loot = GameObject.Instantiate(dropItem);
                        loot.GetComponent<WeaponPickup>().droppedByEnemy = true; // when player is revived, this dropped item will destroy self
                        loot.transform.position = transform.position;
                        GameManager.hasDroppedItem = true;
                    } else if (GameManager.hasDroppedItem == true) {
                        GameManager.hasDroppedItem = false;
                    }
                } else if (dropItemEveryOtherEnemy == false) {
                    GameObject loot = GameObject.Instantiate(dropItem);
                    loot.GetComponent<WeaponPickup>().droppedByEnemy = true;
                    loot.transform.position = transform.position;
                }

            }

            onDeathEvent.Invoke();

            gameObject.SetActive(false);
        }
    }
    /*
    private void ChangeRoom() {
        if (_currentHealth <= 0 && GameManager.currentDay != Day.day3) { // on day 3, enemies revive
            return;
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().position = originPosition;
        transform.position = originPosition;
        _currentHealth = defaultHealth;
    }
    public void EnableIfInCameraCoords() {
        if (originCamCoords == GameManager.cameraCoords && _currentHealth >= 1) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
    */
}
