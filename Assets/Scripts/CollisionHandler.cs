using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{


    [SerializeField] float ReloadTime = 1;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip finishAudio;

    AudioSource audioSource;


    bool crashed = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collide");

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


    private void StartCrashSequence(){
        // add particle and sound effect
        if(!crashed)
        {
            audioSource.PlayOneShot(crashAudio);
        }
        crashed = false;
        this.gameObject.GetComponent<Movement>().enabled = false;
        Invoke("Reload", ReloadTime);
    }

    private void StartFinishLevelSequence(){

        if(!crashed)
        {
            audioSource.PlayOneShot(finishAudio);
        }
        crashed = false;


        this.gameObject.GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", ReloadTime);
    }

    private  void LoadNextLevel()
    {
        var nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextScene == SceneManager.sceneCountInBuildSettings)
            nextScene = 0;

        SceneManager.LoadScene(nextScene);
        crashed = false;
    }

    private  void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        crashed = false;
    }
}
