using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyWeapon {
    eyeball,
    zombie,
    labinnacGun
}

public class ShootAtPlayer : MonoBehaviour {
    public EnemyWeapon myEnemyWeapon = EnemyWeapon.eyeball;
    public AudioClip clip_charge;
    public AudioClip clip_shoot;
    public Animator myAnimator;
    public float defaultShootCountdown = 1f;
    public float defaultShootRandom = 1f;
    private float currentShootCountdown = 1f;
    public float sightRange = 30f;
    public bool _hasSeenPlayer = false;

    void OnEnable()
    {
        _hasSeenPlayer = false;
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
                _hasSeenPlayer = true;
            }
            if(_hasSeenPlayer) {
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
        if(myEnemyWeapon == EnemyWeapon.eyeball) {
            List<float> angles = new List<float> { -90f, -75f, -60f, -45f, -30f, -15f, 0f, 15f, 30f, 45f, 60f, 75f, 90f };

            GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
            GetComponent<AudioSource>().clip = clip_charge;
            GetComponent<AudioSource>().PlayWebGL();

            myAnimator.SetTrigger("Shoot");
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < angles.Count; i++) {
                yield return new WaitForSeconds(0.1f);
                GameObject obj = GameManager.pool_enemyBulletsEyeball.Spawn(transform.position);

                Vector3 direction = (GameManager.playerTrans.position - transform.position).normalized; // recalculate the direction after movement
                Vector3 rotatedDirection = Quaternion.Euler(0f, angles[i], 0f) * direction;

                obj.GetComponent<EntityMove>().SetDirection(rotatedDirection, GetComponent<Collider>());

                GameManager.SpawnLoudAudio(clip_shoot, new Vector2(0.8f, 1.2f), 0.3f);
                // GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
                // GetComponent<AudioSource>().clip = clip_shoot;
                // GetComponent<AudioSource>().PlayWebGL();
            }
        } else if (myEnemyWeapon == EnemyWeapon.zombie) {

            GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
            GetComponent<AudioSource>().clip = clip_charge;
            GetComponent<AudioSource>().PlayWebGL();

            myAnimator.SetTrigger("Shoot");
            yield return new WaitForSeconds(1f);
            Vector3 direction = (GameManager.playerTrans.position - transform.position).normalized; // recalculate the direction after movement
            GameObject bullet1 = GameManager.pool_enemyBulletsEyeball.Spawn(transform.position);
            bullet1.GetComponent<EntityMove>().SetDirection(direction, GetComponent<Collider>());
            GameManager.SpawnLoudAudio(clip_shoot, new Vector2(0.8f, 1.2f), 0.3f);

            yield return new WaitForSeconds(0.1f);
            direction = (GameManager.playerTrans.position - transform.position).normalized; // recalculate the direction after movement
            GameObject bullet2 = GameManager.pool_enemyBulletsEyeball.Spawn(transform.position);
            bullet2.GetComponent<EntityMove>().SetDirection(direction, GetComponent<Collider>());
            GameManager.SpawnLoudAudio(clip_shoot, new Vector2(0.8f, 1.2f), 0.3f);
            yield return new WaitForSeconds(0.1f);
            direction = (GameManager.playerTrans.position - transform.position).normalized; // recalculate the direction after movement
            GameObject bullet3 = GameManager.pool_enemyBulletsEyeball.Spawn(transform.position);
            bullet3.GetComponent<EntityMove>().SetDirection(direction, GetComponent<Collider>());
            GameManager.SpawnLoudAudio(clip_shoot, new Vector2(0.8f, 1.2f), 0.3f);
        } else if (myEnemyWeapon == EnemyWeapon.labinnacGun) {

            // GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
            // GetComponent<AudioSource>().clip = clip_charge;
            // GetComponent<AudioSource>().PlayWebGL();

            Vector3 direction = (GameManager.playerTrans.position - transform.position).normalized; // recalculate the direction after movement
            GameObject bullet1 = GameManager.pool_enemyBulletsEyeball.Spawn(transform.position);
            bullet1.GetComponent<EntityMove>().SetDirection(direction, transform.parent.GetComponent<Collider>());
            GameManager.SpawnLoudAudio(clip_shoot, Vector2.one, 0.5f);
            yield return new WaitForSeconds(0.2f);
            direction = (GameManager.playerTrans.position - transform.position).normalized; // recalculate the direction after movement
            GameObject bullet2 = GameManager.pool_enemyBulletsEyeball.Spawn(transform.position);
            bullet2.GetComponent<EntityMove>().SetDirection(direction, transform.parent.GetComponent<Collider>());
            GameManager.SpawnLoudAudio(clip_shoot, Vector2.one, 0.5f);
            yield return new WaitForSeconds(0.2f);
            direction = (GameManager.playerTrans.position - transform.position).normalized; // recalculate the direction after movement
            GameObject bullet3 = GameManager.pool_enemyBulletsEyeball.Spawn(transform.position);
            bullet3.GetComponent<EntityMove>().SetDirection(direction, transform.parent.GetComponent<Collider>());
            GameManager.SpawnLoudAudio(clip_shoot, Vector2.one, 0.5f);
            yield return new WaitForSeconds(0.2f);
            direction = (GameManager.playerTrans.position - transform.position).normalized; // recalculate the direction after movement
            GameObject bullet4 = GameManager.pool_enemyBulletsEyeball.Spawn(transform.position);
            bullet4.GetComponent<EntityMove>().SetDirection(direction, transform.parent.GetComponent<Collider>());
            GameManager.SpawnLoudAudio(clip_shoot, Vector2.one, 0.5f);
            yield return new WaitForSeconds(0.2f);
            direction = (GameManager.playerTrans.position - transform.position).normalized; // recalculate the direction after movement
            GameObject bullet5 = GameManager.pool_enemyBulletsEyeball.Spawn(transform.position);
            bullet5.GetComponent<EntityMove>().SetDirection(direction, transform.parent.GetComponent<Collider>());
            GameManager.SpawnLoudAudio(clip_shoot, Vector2.one, 0.5f);
        }


    }
}
