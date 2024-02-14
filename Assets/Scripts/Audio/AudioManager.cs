using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager : MonoBehaviour
{
    [BankRef] public List<string> startupSoundbanks;

    public static Action OnAllSoundbanksLoaded;

    private int _startupBanksTotal;
    private int _startupBanksLoaded;

    private void Awake()
    {
        AudioStatics.OnGlobalPlayEventRaised += PlayGlobalEventWithValidation;
        AudioStatics.OnGlobalStopEventRaised += StopGlobalEventWithValidation;
        AudioStatics.OnPositionalPlayEventRaised += PlayPositionalEventWithValidation;
        AudioStatics.OnPositionalStopEventRaised += StopPositionalEventWithValidation;
        AudioStatics.OnParameterChangeEventRaised += SetParameter;
        LoadStartupSoundbanks();
    }

    private void OnDestroy()
    {
        UnloadBanks();
        AudioStatics.OnGlobalPlayEventRaised -= PlayGlobalEventWithValidation;
        AudioStatics.OnGlobalStopEventRaised -= StopGlobalEventWithValidation;
        AudioStatics.OnPositionalPlayEventRaised -= PlayPositionalEventWithValidation;
        AudioStatics.OnPositionalStopEventRaised -= StopPositionalEventWithValidation;
        AudioStatics.OnParameterChangeEventRaised -= SetParameter;
    }

     #region Load/Unload Soundbanks
    private void LoadStartupSoundbanks()
    {
        _startupBanksLoaded = 0;
        _startupBanksTotal = startupSoundbanks.Count;

        foreach (var bank in startupSoundbanks)
        {
            RuntimeManager.LoadBank(bank);
            if(RuntimeManager.HasBankLoaded(bank))
                OnStartupBankLoaded(bank);
        }
    }
    
    private void UnloadBanks()
    {
        foreach (var bank in startupSoundbanks)
        {
            RuntimeManager.UnloadBank(bank);
            _startupBanksLoaded--;
        }
    }
    
    private void OnStartupBankLoaded(string bankName)
    {
        _startupBanksLoaded++;
        if (_startupBanksLoaded < _startupBanksTotal) 
            return;
        OnAllSoundbanksLoaded?.Invoke();
    }

    #endregion

    #region Core

    private void SetParameter(EventInstance eventInstance, string parameterName, float parameterValue)
    {
        if(!eventInstance.isValid())
            return;
        
        var setParamResult = eventInstance.setParameterByName(parameterName, parameterValue);
        if (setParamResult != RESULT.OK)
            Debug.LogError(setParamResult);
    }
    
    private void PlayGlobalEventWithValidation(EventInstance eventInstance)
    {
        if(!eventInstance.isValid())
            return;
        
        var playbackResult = eventInstance.getPlaybackState(out var state);
        if (playbackResult != RESULT.OK)
            Debug.LogError(playbackResult);
        
        if(state is PLAYBACK_STATE.PLAYING or PLAYBACK_STATE.STARTING)
            return;
        
        var startResult = eventInstance.start();
        if (startResult != RESULT.OK)
            Debug.LogError(startResult);
        
        var releaseResult = eventInstance.release();
        if (releaseResult != RESULT.OK)
            Debug.LogError(releaseResult);
    }
    
    private void StopGlobalEventWithValidation(EventInstance eventInstance, STOP_MODE stopMode = STOP_MODE.IMMEDIATE)
    {
        if(!eventInstance.isValid())
            return;

        var playbackResult = eventInstance.getPlaybackState(out var state);
        if (playbackResult != RESULT.OK)
            Debug.LogError(playbackResult);
        
        if(state != PLAYBACK_STATE.PLAYING)
            return;

        var stopResult = eventInstance.stop(stopMode);
        if (stopResult != RESULT.OK)
            Debug.LogError(stopResult);
    }
    
    private void PlayPositionalEventWithValidation(EventInstance eventInstance, Transform target)
    {
        if(!eventInstance.isValid())
            return;
        
        RuntimeManager.AttachInstanceToGameObject(eventInstance, target.transform);
        
        var playbackResult = eventInstance.getPlaybackState(out var state);
        if (playbackResult != RESULT.OK)
            Debug.LogError(playbackResult);
        
        if(state is PLAYBACK_STATE.PLAYING or PLAYBACK_STATE.STARTING)
            return;
        
        var startResult = eventInstance.start();
        if (startResult != RESULT.OK)
            Debug.LogError(startResult);
    }
    
    private void StopPositionalEventWithValidation(EventInstance eventInstance, Transform target, STOP_MODE stopMode = STOP_MODE.IMMEDIATE)
    {
        if(!eventInstance.isValid())
            return;
        
        RuntimeManager.DetachInstanceFromGameObject(eventInstance);
        
        var playbackResult = eventInstance.getPlaybackState(out var state);
        if (playbackResult != RESULT.OK)
            Debug.LogError(playbackResult);
        
        if(state != PLAYBACK_STATE.PLAYING)
            return;

        var releaseResult = eventInstance.release();
        if (releaseResult != RESULT.OK)
            Debug.LogError(releaseResult);
        
        var stopResult = eventInstance.stop(stopMode);
        if (stopResult != RESULT.OK)
            Debug.LogError(stopResult);
    }
    #endregion
}
