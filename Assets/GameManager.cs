using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static bool playerInMenu = true;
    public static bool playerIsDead = false;
    public static bool playerInDialogue = false;
    public static bool hasDroppedItem = false; // used with dropItemEveryOtherEnemy

    public Transform playerCheckpoint;

    public static GameManager myGameManager;
    public static Transform playerTrans;
    public static Transform playerInv;
    public static Rigidbody playerRigidbody;
    public static GameObject mainMenu;
    public static GameObject creditsMenu;
    public static Transform weaponMenu;
    public static DialogueManager dialogueManager;
    public static GameObject deathMessage;
    public static GameObject useMessage;
    public static DisplayPlayerHealth displayPlayerHealth;
    public static TextMeshProUGUI debugMessage;

    private static Pool pool_LoudAudioSource;
    public static Pool pool_bulletsRevolver;
    public static Pool pool_bulletsMissiles;
    public static Pool pool_enemyBulletsEyeball;
    public static Pool pool_explosions;
    public static ParticleSystem particles_BloodKill;
    public static ParticleSystem particles_BloodDamage;

    public static UnityEvent playerReviveEvent = new UnityEvent();

    void Awake() {
        Time.timeScale = 0f;
        playerInMenu = true;
        playerIsDead = false;
        playerInDialogue = false;
        hasDroppedItem = false;
        playerReviveEvent.RemoveAllListeners();

        myGameManager = GetComponent<GameManager>();

        playerTrans = GameObject.Find("Player").transform;
        playerInv = playerTrans.Find("PlayerInv");
        playerRigidbody = playerTrans.GetComponent<Rigidbody>();
        mainMenu = GameObject.Find("MainMenu").gameObject;
        creditsMenu = GameObject.Find("CreditsMenu").gameObject;
        creditsMenu.SetActive(false);
        weaponMenu = GameObject.Find("Canvas/WeaponMenu").transform;
        dialogueManager = GameObject.Find("Canvas/Dialogue").GetComponent<DialogueManager>();
        deathMessage = GameObject.Find("Canvas/DeathMessage");
        deathMessage.SetActive(false);
        useMessage = GameObject.Find("Canvas/UseMessage");
        useMessage.SetActive(false);
        displayPlayerHealth = GameObject.Find("Canvas/PlayerHealth").GetComponent<DisplayPlayerHealth>();
        debugMessage = GameObject.Find("Canvas/DebugMessage").GetComponent<TextMeshProUGUI>();
        debugMessage.text = "";

        pool_LoudAudioSource = transform.Find("pool_LoudAudioSource").GetComponent<Pool>();
        pool_bulletsRevolver = transform.Find("pool_BulletsRevolver").GetComponent<Pool>();
        pool_bulletsMissiles = transform.Find("pool_BulletsMissiles").GetComponent<Pool>();
        pool_enemyBulletsEyeball = transform.Find("pool_EnemyBulletsEyeball").GetComponent<Pool>();
        pool_explosions = transform.Find("pool_Explosions").GetComponent<Pool>();
        particles_BloodKill = transform.Find("Particles_BloodKill").GetComponent<ParticleSystem>();
        particles_BloodDamage = transform.Find("Particles_BloodDamage").GetComponent<ParticleSystem>();

        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

    }
    public static void ChangeCheckpoint(Transform newCheckpoint) {
        myGameManager.playerCheckpoint = newCheckpoint;
    }
    public static void KillPlayer() {
        playerIsDead = true;
        deathMessage.SetActive(true);

        foreach(Transform child in playerInv) {
            Destroy(child.gameObject);
        }
    }
    public void WriteMessage(string newMessage) {
        debugMessage.text = newMessage;

        StartCoroutine("DeleteMessage");
    }
    private IEnumerator DeleteMessage() {
        yield return new WaitForSeconds(5f);
        debugMessage.text = "";
    }

    public void NewGame() {
        Time.timeScale = 1f;
        playerInMenu = false;
        playerIsDead = false;
        playerInDialogue = false;
        hasDroppedItem = false;

        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
        deathMessage.SetActive(false);

        playerCheckpoint = GameObject.Find("checkpoint_silentPath").transform;

        playerTrans.GetComponent<Rigidbody>().position = playerCheckpoint.position;
        playerTrans.position = playerCheckpoint.position;
        playerTrans.GetComponent<PlayerHealth>().ResetHealth();

        particles_BloodDamage.Clear();
        particles_BloodKill.Clear();
    }
    public void RevivePlayer() {
        playerIsDead = false;
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
        deathMessage.SetActive(false);

        playerTrans.GetComponent<Rigidbody>().position = playerCheckpoint.position;
        playerTrans.position = playerCheckpoint.position;
        playerTrans.GetComponent<PlayerHealth>().ResetHealth();

        particles_BloodDamage.Clear();
        particles_BloodKill.Clear();

        foreach (Transform child in playerInv) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in weaponMenu) {
            Destroy(child.gameObject);
        }

        playerReviveEvent.Invoke();
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
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.W)) {
            KillPlayer();
        }

        if (Input.GetMouseButtonDown(0) && Cursor.visible) {
            Cursor.visible = false;
        }

        if(playerIsDead == true && playerInMenu == false && Input.GetButtonDown("Revive")) {
            RevivePlayer();
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
