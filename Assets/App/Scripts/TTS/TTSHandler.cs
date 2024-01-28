using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSHandler : MonoBehaviour
{
    public SpeechManager SpeechManag;
    private static TTSHandler instance;
    private void Awake()
    {
        instance = this;
    }
    public static void Speak(string text,Action onComplete)
    {
        instance.SpeechManag.SpeakWithRESTAPI(text, onComplete);
    }


   
}


