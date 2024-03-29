﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // configuration parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;

    private float minX;
    private float maxX;

    private float minY;
    private float maxY;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
       // StartCoroutine(PrintAndWait());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Movement();
       
    }

    private void Move()
    {

        // Horizontal Movement
        // making frame rate independent for different devices
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; 
        var newXPos = transform.position.x + deltaX;

        // Vertical Movement
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;

        // clamp the new values for player
        newXPos = Mathf.Clamp(newXPos, minX, maxX);
        newYPos = Mathf.Clamp(newYPos, minY, maxY);

        transform.position = new Vector2(newXPos, newYPos);

    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Fire()
    {
        // Mobile device control
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            foreach(var touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    firingCoroutine = StartCoroutine(FireContinuously());
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    StopCoroutine(firingCoroutine);
                }
            }
        }

        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    // Coroutine
    private IEnumerator PrintAndWait()
    {
        Debug.Log("First message sent, boss!");
        yield return new WaitForSeconds(3);
        Debug.Log("The second message, yo!");
    }

    private IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser = Instantiate(
            laserPrefab,
            transform.position,
            Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            // shoot sound
            AudioSource.PlayClipAtPoint(shootSound,
                Camera.main.transform.position,
                shootSoundVolume);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
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
        FindObjectOfType<Level>().LoadGameOver();

        Destroy(gameObject);

        AudioSource.PlayClipAtPoint(deathSound,
            Camera.main.transform.position,
            deathSoundVolume);
    }

    public int GetHealth()
    {
        return health;
    }

    private void Movement()
    {
        var dir = Vector3.zero;

       // dir.y = -Input.acceleration.y;
        dir.x = Input.acceleration.x;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= 40 * Time.deltaTime;

        transform.Translate(dir);
    }
}
