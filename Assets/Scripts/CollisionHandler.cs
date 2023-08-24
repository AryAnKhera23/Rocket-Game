using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    Movement move;
    AudioSource audioSource;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashparticles;
    [SerializeField] ParticleSystem successparticles;  
    [SerializeField] float delay = 1f;
    bool isTransitioning = false;
    bool collisionDisable = false;
    void Start()
    {
        move = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        CheatKeys();
    }
    void CheatKeys()
    {
        if(Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
        if(Input.GetKey(KeyCode.C))
        {
            collisionDisable = !collisionDisable; //this toggles collision
        }
    }
    
    
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisable) {return;}

        switch(other.gameObject.tag)
        {
            
            case "Friendly": Debug.Log("You have collided with launchpad nothing happens");
                             break;
            case "Finish": SuccessSequence();
                           break;
            default: CrashSequence();
                break;
        }    
    }
    void CrashSequence()
    {
        isTransitioning = true;
        crashparticles.Play();
        audioSource.Stop();
        move.enabled = false;
        audioSource.PlayOneShot(crash);
        Invoke("ReloadLevel", delay);

    }
    void SuccessSequence()
    {
        isTransitioning = true;
        successparticles.Play();
        audioSource.Stop();
        move.enabled = false;
        audioSource.PlayOneShot(success);
        Invoke("NextLevel",delay);
    }
    void NextLevel()
    {
        int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        int nextsceneindex = currentsceneindex + 1;
        if(nextsceneindex == SceneManager.sceneCountInBuildSettings) //scenecountinbuildsettings is the total no of scenes
        {
            nextsceneindex = 0;
        }
        SceneManager.LoadScene(nextsceneindex);

    }
    void ReloadLevel()
    {
        int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneindex);
        gameObject.SetActive(true); 
    }
    
}
