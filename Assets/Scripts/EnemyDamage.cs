using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour {

    [SerializeField] float hitPoints = 1;
    private float health;
    [SerializeField] ParticleSystem hitParticlePrefab; 
    [SerializeField] ParticleSystem deathParticlePrefab; 
    [SerializeField] AudioClip enemyDamageSFX;
    [SerializeField] AudioClip enemyDeathSFX; 

    [Header("HealthBar")]
    [SerializeField] Image healthBar;

    AudioSource myAudioSource;

    // Use this for initialization
    void Start ()
    {
        myAudioSource = GetComponent<AudioSource>();
        health = hitPoints;
	}

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if(health <= 0)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        health -= 0.05f;

        healthBar.fillAmount = health / hitPoints;

        hitParticlePrefab.Play();
        myAudioSource.PlayOneShot(enemyDamageSFX, 0.6f);
    }

    private void KillEnemy()
    {
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, vfx.main.duration);

        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);

        Destroy(gameObject);
    }
}
