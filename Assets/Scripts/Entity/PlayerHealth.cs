using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public AudioClip clipPlayerHurt;
    public AudioClip clipPlayerKill;
    public GameObject playerGraphics;
    // public List<AudioClip> clipDeath;
    public int defaultHealth = 100;

    public float defaultTimeToEndInvunrability = 1f;
    private float currentInvunrability = 0f;

    [Header("read only")]
    public int _currentHealth = 100;

    void Awake()
    {
        _currentHealth = defaultHealth;
    }
    public void ResetHealth() {
        playerGraphics.SetActive(true);
        _currentHealth = defaultHealth;
        GameManager.displayPlayerHealth.UpdateHealthValue(_currentHealth);
    }
    private void FixedUpdate() {
        if (currentInvunrability >= 0f) {
            currentInvunrability-= Time.deltaTime;
        }
    }
    public void AddDamage(int damage=20) {
        if (currentInvunrability >= 0f
            || _currentHealth <= 0) { // why we check this variable twice? Because player hitbox is still active at death.
            return;
        }
        currentInvunrability = defaultTimeToEndInvunrability;
        _currentHealth -= damage;

        GameManager.displayPlayerHealth.UpdateHealthValue(_currentHealth);

        if(_currentHealth >= 1) {
            GameManager.SpawnLoudAudio(clipPlayerHurt);
            GameManager.particles_BloodDamage.transform.position = transform.position;
            GameManager.particles_BloodDamage.Play();
        }
        if(_currentHealth <= 0) {
            GameManager.SpawnLoudAudio(clipPlayerKill);
            GameManager.particles_BloodKill.transform.position = transform.position;
            GameManager.particles_BloodKill.Play();
            GameManager.KillPlayer();
            playerGraphics.SetActive(false);
            return;
        }
    }
}
