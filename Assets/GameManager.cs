using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public static bool playerIsDead = false;

    public static Transform playerTrans;
    public static Rigidbody playerRigidbody;

    private static Pool pool_LoudAudioSource;
    public static Pool pool_bulletsRevolver;

    public static UnityEvent playerReviveEvent = new UnityEvent();

    void Awake()
    {
        playerTrans = GameObject.Find("Player").transform;
        playerRigidbody = playerTrans.GetComponent<Rigidbody>();

        pool_LoudAudioSource = transform.Find("pool_LoudAudioSource").GetComponent<Pool>();
        pool_bulletsRevolver = transform.Find("pool_BulletsRevolver").GetComponent<Pool>();

        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public static void KillPlayer() {
        playerIsDead = true;
    }
    void Update()
    {
        Cursor.visible = false;
        if (Input.GetMouseButtonDown(0) && Cursor.visible) {
            Cursor.visible = false;
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
