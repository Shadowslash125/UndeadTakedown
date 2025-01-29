using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject ObjPrefab;

    // Serialize fields to expose in Inspector, but keep private for encapsulation
    [SerializeField] private float spawnRate = 10f;
    [SerializeField] private Vector2 spawnRangeX = new Vector2(-10, 10);
    [SerializeField] private Vector2 spawnRangeZ = new Vector2(-10, 10);
    [SerializeField] private float spawnHeight = 3f;

    private Coroutine spawnCoroutine;

    void Start()
    {
        if (ObjPrefab == null)
        {
            Debug.LogError("No ObjPrefab assigned for spawning!");
            return;
        }

        // Start the spawn coroutine
        spawnCoroutine = StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        // Delay before starting the spawn
        yield return new WaitForSeconds(2f);

        while (true)
        {
            SpawnObj();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnObj()
    {
        // Generate a random position based on the specified range
        Vector3 randomSpawnPos = new Vector3(
            Random.Range(spawnRangeX.x, spawnRangeX.y),
            spawnHeight,
            Random.Range(spawnRangeZ.x, spawnRangeZ.y)
        );

        // Instantiate the object
        Instantiate(ObjPrefab, randomSpawnPos, Quaternion.identity);
    }

    // Public method to stop spawning if needed
    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    // Draw gizmos to visualize spawn area in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        // Calculate the center and size of the spawn area
        Vector3 center = new Vector3(
            (spawnRangeX.x + spawnRangeX.y) / 2f, 
            spawnHeight, 
            (spawnRangeZ.x + spawnRangeZ.y) / 2f
        );

        Vector3 size = new Vector3(
            spawnRangeX.y - spawnRangeX.x, 
            0.1f, // Height is minimal since the spawn area is flat
            spawnRangeZ.y - spawnRangeZ.x
        );

        // Draw a wireframe box to visualize the spawn area
        Gizmos.DrawWireCube(center, size);
    }
}
