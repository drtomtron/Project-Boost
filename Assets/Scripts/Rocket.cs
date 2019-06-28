using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    AudioSource explosionSFX;
    public int rocketThrust = 50;

    public float delay = 3f;
    float explodeTime = 2f;
    float countdown;
    public bool hasExploded = false;
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        explosionSFX = GetComponent<AudioSource>();
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        RocketThrust();
        RocketRotation();
        Countdown();
    }

    private void RocketThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
        }
    }

    private void RocketRotation()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rocketThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * rocketThrust);
        }

        rigidBody.freezeRotation = false; //resume physics control of rotation
    }

    void Countdown()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        //Show effect
        explosionEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
        //Play SFX
        explosionSFX.Play();
        //Get nearby objects
        //Add force
        //Damage

        Destroy(gameObject);
        Destroy(explosionEffect, explodeTime);
    }
}