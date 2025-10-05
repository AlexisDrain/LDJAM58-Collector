using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public AudioClip clipPlayerHurt;
    public AudioClip clipPlayerKill;
    public GameObject playerGraphics;
    // public List<AudioClip> clipDeath;
    public int defaultHealth = 100;

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
    public void AddDamage(int damage=20) {
        if (_currentHealth <= 0) { // why we check this variable twice? Because player hitbox is still active at death.
            return;
        }
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
