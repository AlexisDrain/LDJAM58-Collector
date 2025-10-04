using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform target;
    public AudioClip teleportAudioClip;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void InvokeTeleport()
    {
        GameManager.playerTrans.GetComponent<Rigidbody>().position = target.position;
        GameManager.playerTrans.position = target.position;
        GameManager.SpawnLoudAudio(teleportAudioClip);
    }
}
