using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration param
    [Header("Player")]

    [SerializeField] float health = 200f;
    [Header("Player Movement")]
    [SerializeField] BackgroundScroller Playground;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float paddingSides = 1f;
    [SerializeField] float paddingTopBottom = 1f;
    
    [Header("Projectile")]
    [SerializeField] GameObject activeLaser;
    [SerializeField] List<GameObject> Lasers;
    //[SerializeField] float projectileSpeed = 10f;

    int activeLaserIndex = 0;

    [Header("Visual Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 0.3f;

    [Header("Sound Effects")]

    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float volumeDeathSFX = 1f;

    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float volumeShootSFX = 1f;

    Coroutine fireingCoroutine;

    // Space on Sides of Screen
    float xMin;
    float xMax;

    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBounderies();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        ChangeLaser();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireingCoroutine = StartCoroutine(FireContinuosly());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireingCoroutine);
        }
    }

    private void ChangeLaser()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if(activeLaserIndex >= Lasers.Count-1)
            {
                activeLaserIndex = 0;
            }
            else
            {
                activeLaserIndex++;
            }
            
            UpdateLaser(Lasers[activeLaserIndex]);
        }
    }
    
    IEnumerator FireContinuosly()
    {
        while (true)
        {
        GameObject laser = Instantiate(activeLaser, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, activeLaser.GetComponent<Player_Laser>().GetbulletSpeed());
        PlaySound(shootSFX,volumeShootSFX);
        yield return new WaitForSeconds(activeLaser.GetComponent<Player_Laser>().GetBulletFireSpeed());
        }

    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX , xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBounderies()
    {
        /*
        Camera gameCam = Camera.main;
        xMin = gameCam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + padding;
        xMax = gameCam.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - padding;

        yMin = gameCam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + padding;
        yMax = gameCam.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - padding;
        */
        Camera gameCam = Camera.main;
        xMin = Playground.GetComponent<MeshRenderer>().bounds.min.x + paddingSides;
        xMax = Playground.GetComponent<MeshRenderer>().bounds.max.x - paddingSides;

        yMin = gameCam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + paddingTopBottom;
        yMax = gameCam.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - paddingTopBottom;

    }

    private void UpdateLaser(GameObject laser)
    {
        activeLaser = laser;
    }

    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        // IF optional , because of layer collision
        if (otherCollision.gameObject.name.Contains("Player"))
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

    public void Die()
    {
        Destroy(gameObject);
        PlaySound(deathSFX,volumeDeathSFX);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        FindObjectOfType<Level>().LoadGameOver();
    }

    private void PlaySound(AudioClip sound,float volume)
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, volume);
    }

    public float GetHealth()
    {
        return health;
    }
}
