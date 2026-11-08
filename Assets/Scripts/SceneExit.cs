using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string destinationSpawnId;

    public static string NextSpawnId { get; private set; } = "HouseStart";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() == null)
            return;

        NextSpawnId = destinationSpawnId;
        SceneManager.LoadScene(sceneToLoad);
    }
}