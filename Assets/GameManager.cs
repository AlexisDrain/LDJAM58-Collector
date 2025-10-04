using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static bool playerInMenu = true;
    public static bool playerIsDead = false;

    public static Transform playerTrans;
    public static Rigidbody playerRigidbody;
    public static GameObject mainMenu;
    public static GameObject creditsMenu;
    public static Transform weaponMenu;

    private static Pool pool_LoudAudioSource;
    public static Pool pool_bulletsRevolver;
    public static ParticleSystem particles_BloodKill;
    public static ParticleSystem Particles_BloodDamage;

    public static UnityEvent playerReviveEvent = new UnityEvent();

    void Awake() {
        Time.timeScale = 0f;
        playerInMenu = true;
        playerIsDead = false;

        playerTrans = GameObject.Find("Player").transform;
        playerRigidbody = playerTrans.GetComponent<Rigidbody>();
        mainMenu = GameObject.Find("MainMenu").gameObject;
        creditsMenu = GameObject.Find("CreditsMenu").gameObject;
        creditsMenu.SetActive(false);
        weaponMenu = GameObject.Find("Canvas/WeaponMenu").transform;

        pool_LoudAudioSource = transform.Find("pool_LoudAudioSource").GetComponent<Pool>();
        pool_bulletsRevolver = transform.Find("pool_BulletsRevolver").GetComponent<Pool>();
        particles_BloodKill = transform.Find("Particles_BloodKill").GetComponent<ParticleSystem>();
        Particles_BloodDamage = transform.Find("Particles_BloodDamage").GetComponent<ParticleSystem>();

        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public static void KillPlayer() {
        playerIsDead = true;
    }
    public void StartGame() {
        Time.timeScale = 1f;
        playerInMenu = false;
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }
    public void ResumeGame() {
        Time.timeScale = 1f;
        playerInMenu = false;
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }
    public void PauseGame() {
        Time.timeScale = 0f;
        playerInMenu = true;
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }
    void Update()
    {
        Cursor.visible = false;
        if (Input.GetMouseButtonDown(0) && Cursor.visible) {
            Cursor.visible = false;
        }

        if(Input.GetButtonDown("Pause")) {
            PauseGame();
        }
    }

    public static Vector3 GetMousePositionOnFloor() {
        // Create a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance = -ray.origin.y / ray.direction.y;
        Vector3 pointOnFloor = ray.origin + ray.direction * distance;

        return pointOnFloor;
    }

    public static AudioSource SpawnLoudAudio(AudioClip newAudioClip, Vector2 pitch = new Vector2(), float newVolume = 1f) {

        if (newAudioClip == null) {
            return null;
        }

        float sfxPitch;
        if (pitch.x <= 0.1f) {
            sfxPitch = 1;
        } else {
            sfxPitch = Random.Range(pitch.x, pitch.y);
        }

        AudioSource audioObject = pool_LoudAudioSource.Spawn(new Vector3(0f, 0f, 0f)).GetComponent<AudioSource>();
        audioObject.GetComponent<AudioSource>().pitch = sfxPitch;
        audioObject.GetComponent<AudioSource>().clip = newAudioClip;
        audioObject.GetComponent<AudioSource>().volume = newVolume;
        audioObject.Play();
        return audioObject;
        // audio object will set itself to inactive after done playing.
    }
}
