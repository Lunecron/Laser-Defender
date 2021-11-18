using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] int PointsForKill = 300;

    [Header("Weapon")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 1f;
    [SerializeField] GameObject activeLaser;

    [Header("Visual Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 0.3f;

    [Header("Sound Effects")]

    [SerializeField] AudioClip deathSFX;
    [SerializeField][Range(0,1)] float volumeDeathSFX = 1f;

    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float volumeShootSFX = 1f;



    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(activeLaser, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -(activeLaser.GetComponent<Player_Laser>().GetbulletSpeed()));
        PlaySound(shootSFX, volumeShootSFX);
    }

    private void OnTriggerEnter2D(Collider2D otherCollision)
    {

        // IF optional , because of layer collision
        if (otherCollision.gameObject.name.Contains("Enemy"))
        {

        }
        else
        {
            DamageDealer damageDealer = otherCollision.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }
        
        
        
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(PointsForKill);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        PlaySound(deathSFX, volumeDeathSFX);
    }

    private void PlaySound(AudioClip sound, float volume)
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, volume);
    }
}
