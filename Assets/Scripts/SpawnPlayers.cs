using System;
using System.Reflection;
using Meta.WitAi.CallbackHandlers;
using Meta.XR.MRUtilityKit;
using Oculus.Voice;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class SpawnPlayers : MonoBehaviour
{
    [Header("Wit Configuration")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;
    [SerializeField] private WitResponseMatcher witResponseMatcher;
    
    [Header("Voice Event")]
    [SerializeField] private UnityEvent onSpawnPlayerCommand;

    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI commandText;
    
    private Transform _spawnPoint = null;
    private bool _isPlayerSpawned = false;

    private void Awake()
    {
        appVoiceExperience.VoiceEvents.OnRequestCompleted.AddListener(ReactivateVoice);
        
        var eventField = typeof(WitResponseMatcher).GetField("onMultiValueEvent", BindingFlags.NonPublic | BindingFlags.Instance);
        if (eventField != null && eventField.GetValue(witResponseMatcher) is MultiValueEvent onMultiValueEvent)
        {
            onMultiValueEvent.AddListener(OnSpawnPlayerCommand);
        }
        
        ReactivateVoice();
    }

    public void InitialSetup()
    {
        Invoke(nameof(AssignGameObjects), 3);
    }

    public void AssignGameObjects()
    {
        Debug.Log("Assigning GameObjects");
        
        commandText = GameObject.FindWithTag("CommandText").GetComponent<TextMeshProUGUI>();
        _spawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
        
        Debug.Log("Assigned GameObjects");
    }

    private void OnDestroy()
    {
        appVoiceExperience.VoiceEvents.OnRequestCompleted.RemoveListener(ReactivateVoice);
        
        var eventField = typeof(WitResponseMatcher).GetField("onMultiValueEvent", BindingFlags.NonPublic | BindingFlags.Instance);
        if (eventField != null && eventField.GetValue(witResponseMatcher) is MultiValueEvent onMultiValueEvent)
        {
            onMultiValueEvent.RemoveListener(OnSpawnPlayerCommand);
        }
    }

    private void OnSpawnPlayerCommand(string[] arg0)
    {
        if (_isPlayerSpawned) return;
        if (_spawnPoint == null)
        {
            Debug.LogWarning("No Spawn Point");
            AssignGameObjects();
        }
        
        Instantiate(player, _spawnPoint.position, _spawnPoint.rotation);
        
        commandText.text = "Spawned Players";
        _isPlayerSpawned = true;
        
        onSpawnPlayerCommand?.Invoke();
    }

    private void ReactivateVoice()
    {
        appVoiceExperience.Activate();
    }
}
