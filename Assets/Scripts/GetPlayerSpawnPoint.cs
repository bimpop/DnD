using UnityEngine;

public class GetPlayerSpawnPoint : MonoBehaviour
{
    public Transform GetPlayerSpawnPosition()
    {
        return GameObject.FindWithTag("SpawnPoint").transform;
    }
}
