using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ShootAtPlayer : MonoBehaviour {

    public AudioClip clip_charge;
    public AudioClip clip_shoot;
    public Animator myAnimator;
    public float defaultShootCountdown = 1f;
    public float defaultShootRandom = 1f;
    private float currentShootCountdown = 1f;
    public float sightRange = 30f;

    void OnEnable()
    {
        currentShootCountdown = defaultShootCountdown + Random.Range(0f, defaultShootRandom);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (currentShootCountdown > 0f) {
            currentShootCountdown -= Time.deltaTime;
        } else if (currentShootCountdown <= 0f) {
            currentShootCountdown = defaultShootCountdown + Random.Range(0f, defaultShootRandom);

            if (Vector3.Distance(transform.position, GameManager.playerTrans.position) <= sightRange) {
                StartCoroutine("SpawnBullet");
            }
            /*
            RaycastHit hit;
            Physics.Linecast(transform.position, GameManager.playerTrans.position, out hit);
            if(hit.collider && hit.collider.CompareTag("Player") == false) { // no line of sight to player
                return;
            } else {
                StartCoroutine("SpawnBullet");
            }
            */
        }
    }
    
    IEnumerator SpawnBullet() {
        List<float> angles = new List<float> { -90f, -75f, -60f, -45f, -30f, -15f, 0f, 15f, 30f, 45f, 60f, 75f, 90f };

        GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
        GetComponent<AudioSource>().clip = clip_charge;
        GetComponent<AudioSource>().PlayWebGL();

        myAnimator.SetTrigger("Shoot");
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < angles.Count; i++) {
            yield return new WaitForSeconds(0.1f);
            GameObject obj = GameManager.pool_enemyBulletsEyeball.Spawn(transform.position);

            Vector3 direction = (GameManager.playerTrans.position - transform.position).normalized; // recalculate the direction after ant movement
            Vector3 rotatedDirection = Quaternion.Euler(0f, angles[i], 0f) * direction;

            obj.GetComponent<EntityMove>().SetDirection(rotatedDirection, GetComponent<Collider>());

            GameManager.SpawnLoudAudio(clip_shoot, new Vector2(0.8f, 1.2f), 0.3f);
            // GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
            // GetComponent<AudioSource>().clip = clip_shoot;
            // GetComponent<AudioSource>().PlayWebGL();
        }
    }
}
