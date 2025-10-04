using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosion : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    public void InvokeSpawnExplosion()
    {
        GameObject explosion = GameManager.pool_explosions.Spawn(transform.position);
        explosion.GetComponent<AudioSource>().PlayWebGL();
    }
}
