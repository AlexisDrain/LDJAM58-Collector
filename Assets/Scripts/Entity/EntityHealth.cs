using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour {
    public bool dropItemEveryOtherEnemy = true;
    public float defaultHealth = 100;
    public GameObject healthBar;
    public SpriteRenderer healthBarBar;
    public List<AudioClip> clipDeath;
    public List<AudioClip> clipHurt;
    public GameObject dropItem;
    // public bool doNotCountKill = false;

    // private Vector3 originPosition;
    // private Vector2 originCamCoords;
    [Header("read only")]
    public float _currentHealth = 100;

    void Awake()
    {

        _currentHealth = defaultHealth;
        healthBar.SetActive(false);
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
    public void AddDamage(int damage=1) {
        _currentHealth -= damage;

        healthBar.SetActive(true); // starts false
        healthBarBar.size = new Vector2(_currentHealth / defaultHealth * 3.6875f, healthBarBar.size.y);

        if (GetComponent<EntityMoveTo>()) {
            GetComponent<EntityMoveTo>()._hasSeenPlayer = true;
        }
        if (GetComponent<ShootAtPlayer>()) {
            GetComponent<ShootAtPlayer>()._hasSeenPlayer = true;
        }

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

            // item drop
            if (dropItem) {
                if(dropItemEveryOtherEnemy == true) {
                    if(GameManager.hasDroppedItem == false) {
                        GameObject loot = GameObject.Instantiate(dropItem);
                        loot.transform.position = transform.position;
                        GameManager.hasDroppedItem = true;
                    } else if (GameManager.hasDroppedItem == true) {
                        GameManager.hasDroppedItem = false;
                    }
                } else if (dropItemEveryOtherEnemy == false) {
                    GameObject loot = GameObject.Instantiate(dropItem);
                    loot.transform.position = transform.position;
                }

            }

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
