
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float sceneDelaySecs = 1f;
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    void OnCollisionEnter(Collision other)
    {
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
        // TODO add SFX upon success
        // TODO add particle effect upon success
        GetComponent<Movement>().enabled = false;
        Invoke("loadNextLevel", sceneDelaySecs);

    }
    void startCrashSequence()
    {
        // TODO add SFX upon crash
        // TODO add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("reloadLevel", sceneDelaySecs);
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
