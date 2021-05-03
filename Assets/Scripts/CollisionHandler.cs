
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float sceneDelaySecs = 2f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    // if currently transitioning, don't do things (don't allow to move, play audio, etc)
    bool isTransitioning = false;
    bool collisionDisabled = false;
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    
    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update() {
        // Disable by default
        // RespondToDebugKeys();
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly Target hiit");
                break;
            case "Finish":
                Debug.Log("Finished!");
                startSuccessSequence();
                break;
            default:
                Debug.Log("Pew Pew Blew Up");
                // Reload only firstscene
                startCrashSequence();
                break;
        }
    }

    void startSuccessSequence()
    {
        isTransitioning = true;
        // TODO add particle effect upon success
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("loadNextLevel", sceneDelaySecs);

    }
    void startCrashSequence()
    {
        isTransitioning = true;
        // TODO add particle effect upon crash
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("reloadLevel", sceneDelaySecs);
    }
    void RespondToDebugKeys()
    {
        // Once L is pressed (GetKeyDown)
        if (Input.GetKeyDown(KeyCode.L))
        {
            loadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // Toggle Collision


        }
    }
    void reloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void loadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
