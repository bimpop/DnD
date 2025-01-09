using System.Reflection;
using Meta.WitAi.CallbackHandlers;
using Oculus.Voice;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public class GanfaulController : MonoBehaviour
{
    [Header("Wit Configuration")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;
    [SerializeField] private WitResponseMatcher witResponseMatcher;
    
    [Header("Voice Event")]
    [SerializeField] private UnityEvent onAttackCommand;
    [SerializeField] private Animator animatorController;
    [SerializeField] private AudioSource audioSource;
    
    public string isAttacking = "isAttacking";
    
    private void Awake()
    {
        appVoiceExperience.VoiceEvents.OnRequestCompleted.AddListener(ReactivateVoice);
        
        var eventField = typeof(WitResponseMatcher).GetField("onMultiValueEvent", BindingFlags.NonPublic | BindingFlags.Instance);
        if (eventField != null && eventField.GetValue(witResponseMatcher) is MultiValueEvent onMultiValueEvent)
        {
            onMultiValueEvent.AddListener(OnAttackCommand);
        }
        
        ReactivateVoice();
    }

    private void OnDestroy()
    {
        appVoiceExperience.VoiceEvents.OnRequestCompleted.RemoveListener(ReactivateVoice);
        
        var eventField = typeof(WitResponseMatcher).GetField("onMultiValueEvent", BindingFlags.NonPublic | BindingFlags.Instance);
        if (eventField != null && eventField.GetValue(witResponseMatcher) is MultiValueEvent onMultiValueEvent)
        {
            onMultiValueEvent.RemoveListener(OnAttackCommand);
        }
    }

    private void OnAttackCommand(string[] arg0)
    {
        onAttackCommand?.Invoke();
        animatorController.SetBool(isAttacking, true);
        audioSource.PlayOneShot(audioSource.clip);
        
        Invoke(nameof(ResetState), 2);
    }

    private void ResetState()
    {
        animatorController.SetBool(isAttacking, false);
    }

    private void ReactivateVoice()
    {
        appVoiceExperience.Activate();
    }
}
