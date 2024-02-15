using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public static class AudioStatics
{
    public static Action<EventInstance> OnGlobalPlayEventRaised;
    public static Action<EventInstance, STOP_MODE> OnGlobalStopEventRaised;
    public static Action<EventInstance, Transform> OnPositionalPlayEventRaised;
    public static Action<EventInstance, Transform, STOP_MODE> OnPositionalStopEventRaised;
    public static Action<EventInstance, string, float> OnParameterChangeEventRaised;

    public static EventInstance CreateEventInstance(EventReference eventReference)
        => RuntimeManager.CreateInstance(eventReference);

    public static void SetParameter(EventInstance eventInstance, string parameterName, float parameterValue)
        => OnParameterChangeEventRaised?.Invoke(eventInstance, parameterName, parameterValue);
    
    public static void PlayGlobalEvent(EventInstance eventInstance) 
        => OnGlobalPlayEventRaised?.Invoke(eventInstance);

    public static void StopGlobalEvent(EventInstance eventInstance, STOP_MODE stopMode)
        => OnGlobalStopEventRaised?.Invoke(eventInstance, stopMode);

    public static void PlayPositionalEvent(EventInstance eventInstance, GameObject target) 
        => OnPositionalPlayEventRaised?.Invoke(eventInstance, target.transform);
    
    public static void StopPositionalEvent(EventInstance eventInstance, GameObject target, STOP_MODE stopMode) 
        => OnPositionalStopEventRaised?.Invoke(eventInstance, target.transform, stopMode);
}