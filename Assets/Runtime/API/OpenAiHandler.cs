using com.studios.taprobana;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class OpenAiHandler : MonoBehaviour
{
    public Text label;
    public ChatCompletionsApi chatCompletionsApi;
    [Multiline]
    public string systemMessage;
    public string apiKey;
    [Multiline]
    public string messageForRequest;
    public bool sendMessage;
    private void Start()
    {
        chatCompletionsApi = new ChatCompletionsApi(apiKey);
        chatCompletionsApi.SetSystemMessage(systemMessage);
    }
    TaskAwaiter<ChatCompletionsResponse> resp;
    private void Update()
    {
        if (sendMessage)
        {
            resp= chatCompletionsApi.CreateChatCompletionsRequest(new ChatCompletionsRequest(new Message(Roles.USER, messageForRequest)))
                .GetAwaiter();
            resp.OnCompleted(() =>
            {
                ChatCompletionsResponse response = resp.GetResult();
                Debug.Log(response.ToString());
                label.text = response.Choices[0].Message.Content;
            });
            
            sendMessage = false;
        }
    }


}
