using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 60f;
    float rotationSpeed;
    [SerializeField] float rcsThrust = 80f;
    public bool flightEnabled = true;

    public float horizontalValue;

    float explodeTime = 2f;
    public bool hasExploded = false;
    public GameObject explosionEffect;

    public bool levelComplete = false;
    public GameObject levelCompleteMusic;
    public Quaternion rocketCurrentRotation;
    public GameObject landingRotation;
    public Quaternion rocketEndRotation;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketThrust();
        RocketRotation();
    }

    private void RocketThrust()
    {
        float thrustPerFrame = mainThrust * Time.deltaTime;

        if (flightEnabled && Input.GetKey(KeyCode.Space) || flightEnabled && Input.GetButton("Fire1"))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustPerFrame);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1"))
        {
            audioSource.Stop();
        }
    }

    private void RocketRotation()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        rotationSpeed = rcsThrust * Time.deltaTime;

        rigidBody.freezeRotation = true; // take manual control of rotation

        if (flightEnabled && Input.GetKey(KeyCode.A) || flightEnabled && horizontalValue < 0f)
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (flightEnabled && Input.GetKey(KeyCode.D) || flightEnabled && horizontalValue > 0f)
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidBody.freezeRotation = false; //resume physics control of rotation
    }

    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Safe":
                print("Safe!");
                break;

            case "Goal":
                CompletedLevel();
                flightEnabled = false;
                break;

            default:
                Explode();
                break;
        }
    }

    void CompletedLevel()
    {
        rocketCurrentRotation = transform.rotation;
        rocketEndRotation = landingRotation.transform.rotation;

        if (!levelComplete)
        {
            levelCompleteMusic = Instantiate(levelCompleteMusic, transform.position, transform.rotation);
            levelComplete = true;
            transform.rotation = Quaternion.Lerp(rocketCurrentRotation, rocketEndRotation, 0.8f);
        }
    }

    void Explode()
    {
        //Show effect
        explosionEffect = Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
        Destroy(explosionEffect, explodeTime);
    }
}