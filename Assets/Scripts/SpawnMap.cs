using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class SpawnMap : MonoBehaviour
{
    [SerializeField] private Transform mapSpawnPoint;
    [SerializeField] private GameObject mapPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Assert.IsNotNull(mapSpawnPoint, "spawnPoint is not assigned");
        Assert.IsNotNull(mapPrefab, "mapPrefab is not assigned");
        
        Instantiate(mapPrefab, mapSpawnPoint.position, mapSpawnPoint.rotation);
    }
}
