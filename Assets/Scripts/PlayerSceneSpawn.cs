using UnityEngine;

public class PlayerSceneSpawn : MonoBehaviour
{
    private void Start()
    {
        SceneSpawnPoint[] spawnPoints =
            FindObjectsByType<SceneSpawnPoint>(FindObjectsInactive.Exclude);

        foreach (SceneSpawnPoint point in spawnPoints)
        {
            if (point.SpawnId == SceneExit.NextSpawnId)
            {
                transform.position = point.transform.position;
                return;
            }
        }
    }
}