using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Health health;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        health = GetComponent<Health>();
        playerMovement = GetComponent<PlayerMovement>();
        
    }
    private void Start()
    {
        health.OnZeroHealth.AddListener(EventManager.Instance.PlayerDiedEvent);
    }
}