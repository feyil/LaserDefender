using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speedOfSpin = 1f;

    // Update is called once per frame
    void Update()
    {
        // make frame rate independent
        transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
    }
}
