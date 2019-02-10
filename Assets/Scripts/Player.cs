using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
       
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

        transform.position = new Vector2(newXPos, newYPos);

    }
}
