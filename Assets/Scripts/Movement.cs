using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - tuning, typically set in editory
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables

    // PARAMETERS
    [SerializeField] float mainThrust = 1000;
    [SerializeField] float sideThrust = 200;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    // CACHE
    Rigidbody rb ;
    AudioSource audioSource;
    
    // STATE

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    
    void StartThrusting()
    {
    if (!audioSource.isPlaying)
    {
        audioSource.PlayOneShot(mainEngine);
    }
    if (!mainBooster.isPlaying)
    {
        mainBooster.Play();
    }
    rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }
    void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void RotateRight()
    {
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
        ApplyRotation(-sideThrust);
    }

    void RotateLeft()
    {
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
        ApplyRotation(sideThrust);
    }

    void StopRotating()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }
    
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freeze rotation to allow for manual rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze rotation after manual rotation to give control back to physics system
    }
}
