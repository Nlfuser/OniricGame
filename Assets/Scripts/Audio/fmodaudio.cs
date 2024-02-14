using UnityEngine;
using FMODUnity;

public class FmodAudio : MonoBehaviour
{
    public static bool Enabled = true;

    public static void Play(EventReference @event)
    {
        if (Enabled)
            RuntimeManager.PlayOneShot(@event);
    }

    public static void PlayAtLocation(EventReference @event, GameObject gameObject)
    {
        if (!Enabled)
            return;

        var instance = RuntimeManager.CreateInstance(@event);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        instance.start();
        instance.release();
    }
}
