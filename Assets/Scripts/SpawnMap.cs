using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class SpawnMap : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject mapPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Assert.IsNotNull(spawnPoint, "spawnPoint is not assigned");
        Assert.IsNotNull(mapPrefab, "mapPrefab is not assigned");
        
        Instantiate(mapPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
