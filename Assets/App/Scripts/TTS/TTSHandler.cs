using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSHandler : MonoBehaviour
{
    public SpeechManager SpeechManag;
    public string text;
    public bool speak;
    private void Update()
    {
        if (speak)
        {
            speak = false;
            SpeechManag.SpeakWithRESTAPI(text);
        }

    }


   
}


