using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector = new Vector3(0, 3f, 0);
    [SerializeField] float period = 8f; // in seconds for ex.
    [SerializeField] [Range(0,1)] float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        
        startingPosition = transform.position;
        Debug.Log(startingPosition);
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (period <= Mathf.Epsilon) { return; } // solve for NaN.. Mathf.Epsilon = smallest float in Mathf.

        float cycles = Time.time / period; // continually growing over time.. (time elapsed)

        const float tau = Mathf.PI * 2; // constant val of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //  (cycle). * tau (a full cycle).. take sin of this component
        
        movementFactor = (rawSinWave + 1f) / 2f; // want to go from 0-1.. instead of -.5 -> .5 
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
