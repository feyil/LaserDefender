using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // configuration parameters
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;

    private float minX;
    private float maxX;

    private float minY;
    private float maxY;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
       
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
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        }
    }
}
