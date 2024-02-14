using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

     public class Fmodaudio : MonoBehaviour
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

                var instance = RuntimeManager.CreateInstance(@event.Guid);
                instance.set3DAttributes(gameObject.To3DAttributes());
                instance.start();
                instance.release();

        }

    }


