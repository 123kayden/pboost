using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{


    [SerializeField] float ReloadTime = 1;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip finishAudio;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem finishParticle;

    AudioSource audioSource;


    bool isTransitioning = false;
    bool collisionsOn = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
            LoadNextLevel();
        else if (Input.GetKey(KeyCode.C))
            collisionsOn = !collisionsOn;
    }

    void OnCollisionEnter(Collision other)
    {

        if (!collisionsOn || isTransitioning ) { return; }


        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;

            case "Finish":
                StartFinishLevelSequence();
                break;

            default:
                StartCrashSequence();
                // Reload();
                break;
        }
    }


    private void StartCrashSequence()
    {
        // add particle and sound effect

        isTransitioning = true;
        audioSource.Stop();

        audioSource.PlayOneShot(crashAudio);
        crashParticle.Play();
        this.gameObject.GetComponent<Movement>().enabled = false;
        Invoke("Reload", ReloadTime);
    }

    private void StartFinishLevelSequence()
    {

        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(finishAudio);
        finishParticle.Play();

        this.gameObject.GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", ReloadTime);
    }

    private void LoadNextLevel()
    {
        var nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
            nextScene = 0;

        SceneManager.LoadScene(nextScene);
    }

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
