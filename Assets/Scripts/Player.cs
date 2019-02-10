using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
        // making frame rate independent for different devices
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime; 
        var newXPos = transform.position.x + deltaX;
        transform.position = new Vector2(newXPos, transform.position.y);
    }
}
