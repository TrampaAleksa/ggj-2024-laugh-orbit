using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSHandler : MonoBehaviour
{
    public SpeechManager SpeechManag;
    public void Speak(string text,Action onComplete)
    {
        SpeechManag.SpeakWithRESTAPI(text, onComplete);
    }


   
}


