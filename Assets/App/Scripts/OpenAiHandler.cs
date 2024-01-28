using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using OpenAI.Assistants;
using OpenAI.Chat;
using OpenAI.Models;
using UnityEditor.Timeline.Actions;

public class OpenAiHandler : MonoBehaviour
{
    public TTSHandler TTSHandler;
    public TMPro.TMP_Text label;
    public bool Speak;
    public bool ToogleSpeak;
    public int indexTest;
    public Mode Mode;
    public Model model= Model.GPT3_5_Turbo;
    List<Message> messages = new List<Message>();
    OpenAIClient OpenAIClient;
    Action OnComplete;
    //Dad Jokes
    //Standup
    //Roast
    private static OpenAiHandler instance;
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        instance = this;

        OpenAIClient = new OpenAIClient();
        messages = new List<Message>
        {
            new Message(Role.System,
            "You are a joke jokey." +
            " The user is a person who plays computer game like Space Invader." +
            " You have a three modes: DAD where you give a dad jokes, STANDUP where your pretand to be standup comedian and ROAST where you roast user on funny way." +
            " Put some related jokes for these modes, with some roast moments." +
            " My input will be {number}_{mode}"+
            " Number going from 1 to 10 and the higher value number is, joke is better"+
            " and your response need to be only a joke, without your commets."+"Only repeat yourself if you have more then 15 messages in the chat"),
            new Message(Role.User, "1_STANDUP"),
            new Message(Role.Assistant, "So I recently started a band called '1023MB'. We haven't had any gigs yet."),
            new Message(Role.User, "5_DAD"),
            new Message(Role.Assistant, "Did you hear about the restaurant on the moon? Great food, no atmosphere!"),
        };
    }
    // Update is called once per frame
    void Update()
    {
        if (Speak)
        {
            GetOpenAiAnswer(indexTest, Mode);
            Speak = false;
        }
    }
    public static void StartAiSpeach(int id)
    {
        instance.GetOpenAiAnswer(id);
    }
    public static void StartAiSpeach(int id, Action onComplete)
    {
        instance.GetOpenAiAnswer(id, onComplete:onComplete);
    }
    
    
    private void GetOpenAiAnswer(int id, Mode mode= Mode.STANDUP, Action onComplete=null)
    {
        messages.Add(new Message(Role.User, BuildString(id, mode)));
        var chatRequest = new ChatRequest(messages, model);
        var response = OpenAIClient.ChatEndpoint.GetCompletionAsync(chatRequest).GetAwaiter();
        response.OnCompleted(()=> OnCompleteStartTTS(response.GetResult(), onComplete));
    }
    private void OnCompleteStartTTS(ChatResponse chatResponse, Action onComplete)
    {
        var choice = chatResponse.FirstChoice;
        messages.Add(choice.Message);
        string text = choice.Message.Content.ToString();
        label.text = text;
        if(ToogleSpeak) TTSHandler.Speak(choice.Message.Content.ToString(), onComplete);
        
    }
    private string BuildString(int id, Mode mode) => $"{id}_{mode.ToString()}";
}
public enum Mode
{
    ROAST,
    DAD,
    STANDUP
}
