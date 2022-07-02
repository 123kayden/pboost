using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float mainThrustFactor = 1000f;
    [SerializeField] float rotateThrustFactor = 300f;
    [SerializeField] AudioClip mainEngine;


    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;



    [SerializeField]
    ForceMode fmode;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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



    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            StartRotatingRight();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            StartRotatingLeft();
        }
        else
        {
            StopRotating();
        }
    }

    void StopThrusting()
    {
        audioSource.Pause();
        mainBoosterParticle.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Time.deltaTime * mainThrustFactor * Vector3.up);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);

        if (!mainBoosterParticle.isPlaying)
            mainBoosterParticle.Play();
    }

    void StopRotating()
    {
        rightBoosterParticle.Stop();
        leftBoosterParticle.Stop();
    }

    void StartRotatingLeft()
    {
        ApplyRotation(-rotateThrustFactor);

        if (!leftBoosterParticle.isPlaying)
            leftBoosterParticle.Play();
    }

    void StartRotatingRight()
    {
        ApplyRotation(rotateThrustFactor);

        if (!rightBoosterParticle.isPlaying)
            rightBoosterParticle.Play();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
