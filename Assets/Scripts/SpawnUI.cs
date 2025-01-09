using UnityEngine;
using UnityEngine.Assertions;

public class SpawnUI : MonoBehaviour
{
    [SerializeField] private Transform uiSpawnPoint;
    [SerializeField] private GameObject uiPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Assert.IsNotNull(uiSpawnPoint, "spawnPoint is not assigned");
        Assert.IsNotNull(uiPrefab, "mapPrefab is not assigned");

        Instantiate(uiPrefab, uiSpawnPoint.position, uiSpawnPoint.rotation);
    }
}